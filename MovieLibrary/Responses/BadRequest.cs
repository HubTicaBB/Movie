namespace MovieLibrary.Responses
{
    public class BadRequest : Response
    {
        public BadRequest()
        {
            StatusCode = 500;
            Message = "Bad Request";
        }
    }
}
