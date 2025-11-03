using Game_Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Game_Web.Data.Models;

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
        var gameHelper = new GameHelper();
        Games = gameHelper.GetGames();
    }
}
