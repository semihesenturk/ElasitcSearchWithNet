using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ES.ElasticSearch.Web.Models;

namespace ES.ElasticSearch.Web.Extension;

public static class ElasticSearchExt
{
    private const string BlogIndexName = "blog";
    public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
    {
        var userName = configuration.GetSection("Elastic")["Username"]!;
        var password = configuration.GetSection("Elastic")["Password"]!;
        var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!))
            .Authentication(new BasicAuthentication(userName, password));

        var client = new ElasticsearchClient(settings);
        AddDefaultMappings(client);
        services.AddSingleton(client);
    }

    private static void AddDefaultMappings(ElasticsearchClient client)
    {
        var indexExistsResponse = client.Indices.Exists(BlogIndexName);
        if (!indexExistsResponse.Exists)
        {
            var createIndexResponse = client.Indices
                .CreateAsync<Blog>(BlogIndexName, c => c
                    .Mappings(map => map
                        .Properties(props => props
                            .Text(t => t.Title, f => f.Fields(x => x.Keyword(k => k.Title)))
                            .Text(t => t.Content)
                            .Keyword(k => k.UserId)
                            .Keyword(k => k.Tags)
                            .Date(d => d.Created))));
        }
    }
}