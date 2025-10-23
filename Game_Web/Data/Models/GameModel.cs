using System.Reflection.Metadata;

namespace Game_Web.Data.Entities
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Categorie { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
    }
}
