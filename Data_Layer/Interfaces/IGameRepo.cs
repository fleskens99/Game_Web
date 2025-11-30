using Repos;
using DTOs;

namespace Interfaces
{
    public interface IGameRepo
    {
        List<GameDTO> GetGames();
        void AddGame(GameDTO game);

    }
}
