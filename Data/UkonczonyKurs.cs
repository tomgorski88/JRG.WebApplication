using System.ComponentModel;

namespace JRG.WebApplication.Data
{
    public class UkonczonyKurs
    {
        public int Id { get; set; }
        public Pracownik? Pracownik { get; set; }
        [DisplayName("Pracownik")]
        public int PracownikId { get; set; }
        public Kurs? Kurs { get; set; }
        [DisplayName("Nazwa kursu")]
        public int KursId { get; set; }
    }
}