namespace Hackathon.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
        public List<double> info_vector { get; set; }
        public List<int> inverse_vector { get; set; }
    }
}
