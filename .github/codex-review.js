import { createAppAuth } from "@octokit/auth-app";
import fetch from "node-fetch";
import OpenAI from "openai";

const openai = new OpenAI({ apiKey: process.env.OPENAI_API_KEY });

async function getGitHubToken() {
  const auth = createAppAuth({
    appId: process.env.APP_ID,
    privateKey: process.env.PRIVATE_KEY,
    installationId: process.env.INSTALLATION_ID
  });
  const token = await auth({ type: "installation" });
  return token.token;
}

async function listChangedFiles(token) {
  const [owner, repo] = process.env.GITHUB_REPOSITORY.split("/");
  const pr = process.env.PR_NUMBER;
  const r = await fetch(
    `https://api.github.com/repos/${owner}/${repo}/pulls/${pr}/files`,
    { headers: { Authorization: `Bearer ${token}`, Accept: "application/vnd.github+json" } }
  );
  return await r.json();
}

async function getFileContent(token, owner, repo, sha) {
  const r = await fetch(
    `https://api.github.com/repos/${owner}/${repo}/git/blobs/${sha}`,
    { headers: { Authorization: `Bearer ${token}` } }
  );
  const j = await r.json();
  return Buffer.from(j.content, "base64").toString("utf8");
}

function buildPrompt(files) {
  let txt = `Aşağıdaki PR değişikliklerini analiz et:
- Güvenlik, performans, mimari uyum, okunabilirlik
- Kritik maddeleri önce yaz
- Gerekirse örnek düzeltme kodu ver

`;
  for (const f of files) {
    if (!/\.(js|ts|tsx|jsx|py|cs|java|go|rb|php|md|json|yml|yaml)$/i.test(f.filename)) continue;
    txt += `--- FILE: ${f.filename} ---\n${f.content.slice(0, 15000)}\n\n`;
  }
  return txt;
}

async function postComment(token, body) {
  const [owner, repo] = process.env.GITHUB_REPOSITORY.split("/");
  const pr = process.env.PR_NUMBER;
  await fetch(`https://api.github.com/repos/${owner}/${repo}/issues/${pr}/comments`, {
    method: "POST",
    headers: { Authorization: `Bearer ${token}`, Accept: "application/vnd.github+json" },
    body: JSON.stringify({ body })
  });
}

async function run() {
  const token = await getGitHubToken();
  const [owner, repo] = process.env.GITHUB_REPOSITORY.split("/");

  const changed = await listChangedFiles(token);
  if (!Array.isArray(changed) || changed.length === 0) {
    await postComment(token, "🤖 Kod dosyası değişikliği bulunamadı.");
    return;
  }

  const files = [];
  for (const f of changed) {
    const content = await getFileContent(token, owner, repo, f.sha);
    files.push({ filename: f.filename, content });
  }

  const prompt = buildPrompt(files);
  const resp = await openai.responses.create({
    model: "gpt-5",
    input: [{ role: "user", content: prompt }],
    max_output_tokens: 1800
  });

  await postComment(token, `🤖 **Codex Bot Analizi**\n\n${resp.output_text || "Çıktı yok."}`);
}

run().catch(async (e) => {
  try {
    const token = await getGitHubToken();
    await postComment(token, `Hata: ${e.message}`);
  } catch {}
  process.exit(1);
});
