namespace ES.ElasticSearch.Web.Dtos.Blog;

public record BlogDto(
    string Id,
    string Title,
    string Content,
    string[] Tags,
    Guid UserId,
    DateTime Created);