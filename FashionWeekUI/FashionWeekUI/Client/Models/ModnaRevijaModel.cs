namespace KulinariumUI.Client.Models
{
    public class ModnaRevijaModel
    {
        public int IdModneRevije { get; set; }
        public int RedniBroj { get; set; }
        public string Naziv { get; set; }
        public string? Grad { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public int VremeOdrzavanja { get; set; }
        public string ImeJavneLicnosti { get; set; }
        public string PrezimeJavneLicnosti { get; set; }
        public string ZanimanjeJL { get; set; }
    }
}
