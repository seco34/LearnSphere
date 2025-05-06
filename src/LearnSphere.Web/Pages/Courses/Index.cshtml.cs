using Microsoft.AspNetCore.Mvc;  
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnSphere.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace LearnSphere.Web.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _factory;
        public List<Course>? Courses { get; set; }

        public IndexModel(IHttpClientFactory factory)
            => _factory = factory;

        public async Task OnGetAsync()
        {
            var client = _factory.CreateClient("ApiClient");
            Courses = await client.GetFromJsonAsync<List<Course>>("/api/course");
        }
        public async Task<IActionResult> OnPostAsync(string title, string description)
{
    var client = _factory.CreateClient("ApiClient");
    var course = new Course { Title = title, Description = description };
    var response = await client.PostAsJsonAsync("/api/course", course);

    if (response.IsSuccessStatusCode)
        return RedirectToPage();   // Başarıyla eklediysen sayfayı yenile

    // Başarısızsa hata mesajı göster
    ModelState.AddModelError("", "Kurs eklenirken bir hata oluştu.");
    await OnGetAsync();            // Yine de listeyi getir
    return Page();
}

    }
}
