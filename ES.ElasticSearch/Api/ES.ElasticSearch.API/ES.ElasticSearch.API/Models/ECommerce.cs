using System.Text.Json.Serialization;
using ES.ElasticSearch.API.Dtos;
using ES.ElasticSearch.API.Dtos.ECommerce;

namespace ES.ElasticSearch.API.Models.ECommerceModel;

public class ECommerce
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = null!;
    [JsonPropertyName("customer_first_name")]
    public string CustomerFirstName { get; set; } = null!;
    [JsonPropertyName("customer_last_name")]
    public string CustomerLastName { get; set; } = null!;
    [JsonPropertyName("customer_full_name")]
    public string CustomerFullName { get; set; } = null!;
    [JsonPropertyName("taxful_total_price")]
    public double TaxFullTotalPrice { get; set; }

    [JsonPropertyName("category")]
    public string[] Category { get; set; } = null!;
    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }
    [JsonPropertyName("order_date")]
    public DateTime OrderDate { get; set; }

    [JsonPropertyName("products")]
    public Product[] Products { get; set; }

    public ECommerceDto CreateDto()
    {
        return new ECommerceDto(Id,
            CustomerFirstName,
            CustomerLastName,
            CustomerFullName,
            TaxFullTotalPrice,
            Category,
            OrderId,
            OrderDate,
            Products.Select(p => new ECommerceProductDto(p.Id, p.ProductName)).ToArray());
    }
}

public class Product
{
    [JsonPropertyName("product_id")]
    public long Id { get; set; }
    [JsonPropertyName("product_name")]
    public string ProductName { get; set; } = null!;
    
}