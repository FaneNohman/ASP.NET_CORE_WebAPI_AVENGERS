using System.Data;

namespace Avengers.Models
{
    
    public class Avenger
    {
        public Guid AvengerId { get; set; }
        public string RealName { get; set; }
        public string HeroName { get; set; }
    }
}
