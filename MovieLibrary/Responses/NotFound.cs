namespace MovieLibrary.Responses
{
    public class NotFound : Response
    {
        public NotFound()
        {
            StatusCode = 404;
            Message = "Not Found";
        }
    }
}
