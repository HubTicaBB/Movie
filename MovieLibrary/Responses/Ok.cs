namespace MovieLibrary.Responses
{
    public class Ok : Response
    {
        public Ok()
        {
            StatusCode = 200;
            Message = "Ok";
        }
    }
}
