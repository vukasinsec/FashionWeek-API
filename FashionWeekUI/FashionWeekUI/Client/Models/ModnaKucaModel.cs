namespace KulinariumUI.Client.Models
{
    public class ModnaKucaModel
    {
        public string Naziv { get; set; }
        public string? ImeOsnivaca { get; set; }
        public string? PrezimeOsnivaca { get; set; }
        public string? Drzava { get; set; }
        public string? Grad { get; set; }
        public Organizator? OrganizatorID { get; set; }
        public List<string> ModniKreatori { get; set; } = new();
        public List<string> Vlasnici { get; set; } = new();
    }

    public class Organizator
    {
        public int OrganizatorID { get; set; }
        public string? Kolekcija { get; set; }
        public char PrvaModnaRevija { get; set; }
    }

}
