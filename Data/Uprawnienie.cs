namespace JRG.WebApplication.Data
{
    public class Uprawnienie
    {
        public int Id { get; set; }
        public Pracownik? pracownik { get; set; }
        public int PracownikId { get; set; }
        public Prawko? prawko { get; set; }
        public int PrawkoId { get; set; }
        
    }
}
