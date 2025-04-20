namespace api_for_kursach.ViewModels
{
    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public string name {  get; set; }
        public int Id {  get; set; }
        public Dictionary<string, string[]> messages { get; set; }
       
    }
}
