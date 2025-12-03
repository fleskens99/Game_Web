using ViewModels;


namespace Logic.VmMapper
{
    public class GameEntityToVm
    {
        public static GameViewModel Map(Entities.Game game)
        {
            return new Presentation.ViewModels.GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Categorie = game.Categorie,
                Description = game.Description,
                Picture = game.Picture
            };
        }
    }
}
