namespace ES.ElasticSearch.API.Dtos.ECommerce;

public record ECommerceDto(
    string Id,
    string CustomerFirstName,
    string CustomerLastName,
    string CustomerFullName,
    string[] Category,
    int OrderId,
    DateTime OrderDate,
    ECommerceProductDto[] Products
)
{
}
    