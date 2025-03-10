using ES.ElasticSearch.API.Dtos;
using Nest;

namespace ES.ElasticSearch.API.Models;

public class Product
{
    [PropertyName("_id")]
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public ProductFeature? Feature { get; set; }

    public ProductDto CreateDto()
    {
        if (Feature ==null)
            return new ProductDto(Id, Name, Price, Stock, Created, Updated, null);
        
        return new ProductDto(
            Id,
            Name,
            Price,
            Stock,
            Created,
            Updated, 
            new ProductFeatureDto(
                Feature.Width,
                Feature.Height,
                Feature.Color.ToString())
            );
    }
}  