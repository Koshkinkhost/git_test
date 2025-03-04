namespace api_for_kursach.Exceptions
{
    public class LoginIsBusyException:Exception
    {
        public LoginIsBusyException(string message):base(message) { }
    }
}
