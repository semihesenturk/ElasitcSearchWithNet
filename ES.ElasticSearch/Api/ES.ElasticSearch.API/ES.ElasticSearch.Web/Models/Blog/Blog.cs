using System.Text.Json.Serialization;
using ES.ElasticSearch.Web.Dtos.Blog;

namespace ES.ElasticSearch.Web.Models.Blog;

public class Blog
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = null!;
    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;
    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = null!;
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }
    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    public BlogDto CreateDto()
    {
        return new BlogDto(Id, Title, Content, Tags, UserId, Created);
    }
}