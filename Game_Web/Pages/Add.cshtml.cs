using Entities;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repos;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Game_Web.Pages;

public class AddModel : PageModel
{
    private readonly IGameRepo _gameRepo;

    [BindProperty]
    //create a Game
    public Game Game { get; set; } = new();
    public IFormFile? Picture { get; set; }
    public string? PreviewImagePath { get; set; }
    public string? SelectedFileName { get; set; }
    public bool ShowPreview { get; set; } = false;

    public void OnGet()
    {
    }

    public AddModel(IGameRepo gameRepo)
    {
        _gameRepo = gameRepo;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Picture != null && Picture.Length > 0)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);

            string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(Picture.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);

            await using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await Picture.CopyToAsync(stream);
            }
            Game.Picture = "/uploads/" + fileName;
        }
        try
        {
            _gameRepo.AddGame(Game);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Unable to save game: " + ex.Message);
            return Page();
        }
        return RedirectToPage("/Index");
    }

    // Preview handler: saves the uploaded file to a temp preview location and returns the page with preview info
    public async Task<IActionResult> OnPostPreviewAsync()
    {
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
            string previewFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "preview");
            Directory.CreateDirectory(previewFolder);

            string fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(Picture.FileName);
            string filePath = Path.Combine(previewFolder, fileName);

            await using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await Picture.CopyToAsync(stream);
            }
            PreviewImagePath = "/uploads/preview/" + fileName;
        }
        else
        {
            PreviewImagePath = null;
        }
        ShowPreview = true;
        return Page();
    }
}