using ES.ElasticSearch.Web.Models;

namespace ES.ElasticSearch.Web.Dtos;

public record BlogCreateDto(
    string Title,
    string Content,
    string[] Tags,
    Guid UserId,
    DateTime Created)
{
    public Blog CreateBlog()
    {
        return new Blog
        {
            Title = Title,
            Content = Content,
            Tags = Tags,
            UserId = UserId,
            Created = DateTime.UtcNow
        };
    }
}