using ES.ElasticSearch.API.Models;

namespace ES.ElasticSearch.API.Dtos;

public record ProductCreateDto(string Name, decimal Price, int Stock, ProductFeatureDto Feature)
{
    public Product CreateProduct()
    {
        return new Product
        {
            Name = Name,
            Price = Price,
            Stock = Stock,
            Feature = new ProductFeature
            {
                Width = Feature.Width,
                Height = Feature.Height,
                Color = (EColor)int.Parse(Feature.Color)
            }
        };
    }
}