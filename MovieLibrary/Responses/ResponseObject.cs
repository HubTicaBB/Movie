namespace MovieLibrary.Responses
{
    public class ResponseObject<T>
    {
        public Response Response { get; set; }
        public T Content { get; set; }
    }
}
