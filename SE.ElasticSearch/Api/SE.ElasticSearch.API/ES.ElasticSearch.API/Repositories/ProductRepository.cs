using ES.ElasticSearch.API.Models;
using Nest;

namespace ES.ElasticSearch.API.Repositories;

public class ProductRepository(ElasticClient client)
{
    public async Task<Product?> SaveAsync(Product product)
    {
        product.Created = DateTime.Now;
        var response = await client.IndexAsync(product, i => i.Index("products"));
        
        //Fast fail
        if(!response.IsValid) return null;
        
        product.Id = response.Id;
        return product;
    }
}