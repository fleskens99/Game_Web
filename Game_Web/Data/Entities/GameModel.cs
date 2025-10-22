using System.Reflection.Metadata;

namespace Game_Web.Data.Entities
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
