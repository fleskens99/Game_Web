using DTOs;


namespace Logic.Mapper
{
    public class GameDTOToEntity
    {
        public static Entities.Game MapToEntity(DTOs.GameDTO gameDto)
        {
            if (gameDto == null) throw new ArgumentNullException(nameof(gameDto));
            return new Entities.Game
            {
                Id = gameDto.Id,
                Name = gameDto.Name,
                Categorie = gameDto.Categorie,
                Description = gameDto.Description,
                Picture = gameDto.Picture
            };
        }
    }
}
