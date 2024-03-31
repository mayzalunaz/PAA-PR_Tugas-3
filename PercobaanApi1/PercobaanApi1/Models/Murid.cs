namespace PercobaanApi1.Models
{
    public class Murid
    {
        public int id_murid { get; set; } //ex : 1, 2, ...
        public string nama { get; set; }
        public string kelas { get; set; } //ex : 10 MIPA 2, 12 IPS 4, ...
        public string alamat { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
