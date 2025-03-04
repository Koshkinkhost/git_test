namespace api_for_kursach.Exceptions
{
    public class UserNotExistException:Exception
    {
        public UserNotExistException(string message):base(message) { }
    }
}
