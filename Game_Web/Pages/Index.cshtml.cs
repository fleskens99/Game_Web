using Game_Web.Data.Entities;
using Game_Web.Data.Models;
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
    [BindProperty]
    public Game_Web.Data.Entities.Game Game { get; set; } = new();

    [BindProperty]
    public IFormFile? Picture { get; set; }
    public List<Game_Web.Data.Entities.Game> Games { get; set; } = new();

    public void OnGet() 
    {
        var gameHelper = new GameRepo();
        Games = gameHelper.GetGames();
    }
    public IActionResult OnPost()
    {
        return RedirectToPage("/Details");
    }
}
