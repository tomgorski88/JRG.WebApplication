using Microsoft.Build.Framework;

namespace JRG.WebApplication.Data
{
    public class Stopien
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}