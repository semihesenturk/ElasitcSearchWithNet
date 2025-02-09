using System.Collections.Immutable;
using ES.ElasticSearch.API.Models;
using Nest;

namespace ES.ElasticSearch.API.Repositories;

public class ProductRepository(ElasticClient client)
{
    private const string IndexName = "products";

    public async Task<Product?> SaveAsync(Product product)
    {
        product.Created = DateTime.Now;
        var response = await client.IndexAsync(product, i => i.Index(IndexName).Id(Guid.NewGuid().ToString()));

        //Fast fail
        if (!response.IsValid) return null;

        product.Id = response.Id;
        return product;
    }

    public async Task<ImmutableList<Product>> GetAllAsync()
    {
        var result = await client.SearchAsync<Product>(
            s => s.Index(IndexName)
                .Query(q => q.MatchAll())
        );

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToImmutableList();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        var response = await client.GetAsync<Product>(id, i => i.Index(IndexName));
        if(!response.IsValid)
            return null;
        
        response.Source.Id = response.Id;
        return response.Source;
    }
}