namespace PostService.Application.DTO_s.Post
{
    public class GetAllPostDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Postname { get; set; }
    }
}
