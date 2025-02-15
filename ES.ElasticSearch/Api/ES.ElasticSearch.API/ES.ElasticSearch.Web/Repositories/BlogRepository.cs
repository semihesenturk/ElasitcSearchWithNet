using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ES.ElasticSearch.Web.Models;

namespace ES.ElasticSearch.Web.Repositories;

public class BlogRepository(ElasticsearchClient client)
{
    private const string IndexName = "blog";

    public async Task<Blog?> SaveAsync(Blog blog)
    {
        blog.Created = DateTime.Now;

        var response = await client.IndexAsync(blog, i => i.Index(IndexName));
        if (!response.IsValidResponse) return null;
        blog.Id = response.Id;

        return blog;
    }

    public async Task<List<Blog>> SearchAsync(string searchText)
    {
        List<Action<QueryDescriptor<Blog>>> listQuery = [];
        
        Action<QueryDescriptor<Blog>> matchAllQuery = (q) => q.MatchAll(_ => { });
        
        Action<QueryDescriptor<Blog>> matchContent = (q) => q
            .Match(m => m
                .Field(f => f.Content)
                .Query(searchText));
        
        Action<QueryDescriptor<Blog>> titleMatchBoolPrefix = (q) => q
            .MatchBoolPrefix(m => m
                .Field(f => f.Content)
                .Query(searchText));

        Action<QueryDescriptor<Blog>> tagTerm = (q) => q
            .Term(t => t
                .Field(f => f.Tags.Suffix("keyword"))
                .Value(searchText));
        
        if (string.IsNullOrEmpty(searchText))
        {
            listQuery.Add(matchAllQuery);
        }
        else
        {
            listQuery.Add(matchContent);
            listQuery.Add(titleMatchBoolPrefix);
            listQuery.Add(tagTerm);
        }
        
        var result = await client.SearchAsync<Blog>(s => s.Index(IndexName)
            .Size(100)
            .Query(q => q
                .Bool(b => b
                    .Should(listQuery.ToArray())
                )));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToList();
    }
}