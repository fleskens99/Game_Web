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
    //create a Game
    public Game_Web.Data.Entities.GameModel Game { get; set; } = new();

    [BindProperty]
    public IFormFile? Picture { get; set; }
    public string? PreviewImagePath { get; set; }
    public string? SelectedFileName { get; set; }
    public bool ShowPreview { get; set; } = false;

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

        try
        {
            var repo = new Game_Web.Data.Models.GameModel(); 
            repo.AddGame(Game);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Unable to save game: " + ex.Message);
            return Page();
        }
        return RedirectToPage("/Index");
    }


    public async Task<IActionResult> OnPostPreviewAsync()
    {
        // Validate required preview fields
        if (string.IsNullOrWhiteSpace(Game?.Name) ||
            string.IsNullOrWhiteSpace(Game?.Description) ||
            string.IsNullOrWhiteSpace(Game?.Categorie))
        {
            ModelState.AddModelError(string.Empty, "Please fill Name, Description and Category before previewing.");
            return Page();
        }

        SelectedFileName = Picture?.FileName;

        if (Picture != null && Picture.Length > 0)
        {
            // Save to a preview folder under wwwroot/uploads/preview
            var previewFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "preview");
            Directory.CreateDirectory(previewFolder);

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(Picture.FileName);
            var filePath = Path.Combine(previewFolder, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Picture.CopyToAsync(stream);
            }

            // set PreviewImagePath relative to web root so img src can use it
            PreviewImagePath = "/uploads/preview/" + fileName;
        }
        else
        {
            // no picture selected; keep null (placeholder will be used)
            PreviewImagePath = null;
        }

        // Keep the Game property filled so the page can render name/desc/category
        ShowPreview = true;

        // Return Page to render preview (no redirect)
        return Page();
    }
}