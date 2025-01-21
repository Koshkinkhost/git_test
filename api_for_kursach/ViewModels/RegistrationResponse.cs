namespace api_for_kursach.ViewModels
{
    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public Dictionary<string, string[]> messages { get; set; }
        public string cookies {  get; set; }
    }
}
