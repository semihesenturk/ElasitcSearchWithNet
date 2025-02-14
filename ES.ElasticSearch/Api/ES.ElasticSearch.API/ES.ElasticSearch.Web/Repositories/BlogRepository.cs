using Elastic.Clients.Elasticsearch;
using ES.ElasticSearch.Web.Models;

namespace ES.ElasticSearch.Web.Repositories;

public class BlogRepository(ElasticsearchClient client)
{
    private const string IndexName = "blog";

    public async Task<Blog?> SaveAsync(Blog blog)
    {
        blog.Created = DateTime.Now;

        var response = await client.IndexAsync(blog, i => i.Index(IndexName));
        if(!response.IsValidResponse) return null;
        blog.Id = response.Id;
        
        return blog;
    }
}