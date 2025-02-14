namespace ES.ElasticSearch.Web.Dtos;

public record BlogDto(
    string Id,
    string Title,
    string Content,
    string[] Tags,
    Guid UserId,
    DateTime Created);