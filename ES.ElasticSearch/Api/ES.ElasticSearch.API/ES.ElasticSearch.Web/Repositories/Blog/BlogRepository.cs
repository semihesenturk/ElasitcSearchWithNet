using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace ES.ElasticSearch.Web.Repositories.Blog;

public class BlogRepository(ElasticsearchClient client)
{
    private const string IndexName = "blog";

    public async Task<Models.Blog.Blog?> SaveAsync(Models.Blog.Blog blog)
    {
        blog.Created = DateTime.Now;

        var response = await client.IndexAsync(blog, i => i.Index(IndexName));
        if (!response.IsValidResponse) return null;
        blog.Id = response.Id;

        return blog;
    }

    public async Task<List<Models.Blog.Blog>> SearchAsync(string searchText)
    {
        List<Action<QueryDescriptor<Models.Blog.Blog>>> listQuery = [];
        
        Action<QueryDescriptor<Models.Blog.Blog>> matchAllQuery = (q) => q.MatchAll(_ => { });
        
        Action<QueryDescriptor<Models.Blog.Blog>> matchContent = (q) => q
            .Match(m => m
                .Field(f => f.Content)
                .Query(searchText));
        
        Action<QueryDescriptor<Models.Blog.Blog>> titleMatchBoolPrefix = (q) => q
            .MatchBoolPrefix(m => m
                .Field(f => f.Content)
                .Query(searchText));

        Action<QueryDescriptor<Models.Blog.Blog>> tagTerm = (q) => q
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
        
        var result = await client.SearchAsync<Models.Blog.Blog>(s => s.Index(IndexName)
            .Size(100)
            .Query(q => q
                .Bool(b => b
                    .Should(listQuery.ToArray())
                )));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToList();
    }
}