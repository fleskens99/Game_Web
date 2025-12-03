using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Game_Web.Pages
{
    public class DetailsModel : PageModel
    {
        public List<GameVM> Game { get; set; } = new List<Game>();
        private readonly IGameRepo _gameRepo;

        public DetailsModel(IGameRepo gameRepo)
        {
            _gameRepo = gameRepo;
        }
        public void OnGet()
        {
        }


        public IActionResult Details(int id)
        {
            var game = _gameRepo.GetGameById(id); 
            if (game == null) return NotFound();
            return Page();
        }


    }
}
