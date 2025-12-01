using Entities;
using Repos;
using Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Game_Web.Pages;

public class IndexModel : PageModel
{
    private readonly IGameRepo _gameRepo;
    [BindProperty]
    public Game Game { get; set; } = new();
    public IFormFile? Picture { get; set; }
    public List<Game> Games { get; set; } = new();

    public IndexModel(IGameRepo gameRepo)
    {
        _gameRepo = gameRepo;
    }


    public void OnGet() 
    {
        Games = _gameRepo.GetGames();
    }
    public IActionResult OnPost()
    {
        return RedirectToPage("/Details");
    }
}
