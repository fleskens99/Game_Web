using Game_Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Game_Web.Pages;

public class AddModel : PageModel
{
    [BindProperty]
    public Game_Web.Data.Entities.GameModel Game { get; set; } = new();

    [BindProperty]
    public IFormFile? Picture { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Picture != null && Picture.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(Picture.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Picture.CopyToAsync(stream);
            }
            Game.Picture = "/uploads/" + fileName;
        }

        var repo = new Game_Web.Data.Models.GameModel();
        repo.AddGame(Game);

        TempData["Success"] = "Game saved.";
        return RedirectToPage();
    }
}