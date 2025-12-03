using Entities;

namespace Interfaces
{
    public interface IGameRepo
    {
        List<Game> GetGames();
        void AddGame(Game game);

        Game GetGameById(int id);

    }
}
