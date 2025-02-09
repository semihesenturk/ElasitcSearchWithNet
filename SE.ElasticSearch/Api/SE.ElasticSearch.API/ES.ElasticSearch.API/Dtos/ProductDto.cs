namespace ES.ElasticSearch.API.Dtos;

public record ProductDto(
    string Id,
    string Name,
    decimal Price,
    int Stock,
    DateTime Created,
    DateTime? Updated,
    ProductFeatureDto? Feature)
{
   
}