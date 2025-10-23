using Game_Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Game_Web.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public Game_Web.Data.Entities.GameModel Game { get; set; } = new();

    [BindProperty]
    public IFormFile? Picture { get; set; }

    public void OnGet()
    {
    }
}
