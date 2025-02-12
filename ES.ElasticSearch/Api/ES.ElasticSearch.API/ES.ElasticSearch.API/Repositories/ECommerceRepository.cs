using System.Collections.Immutable;
using Elastic.Clients.Elasticsearch;
using ES.ElasticSearch.API.Models.ECommerceModel;

namespace ES.ElasticSearch.API.Repositories;

public class ECommerceRepository(ElasticsearchClient client)
{
    private const string IndexName = "kibana_sample_data_ecommerce";

    public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
    {
        var result = await client.SearchAsync<ECommerce>(
            s => s.Index(IndexName).Query(
                q => q.Term(
                    t => t.Field("customer_first_name.keyword"!).Value(customerFirstName)
                )));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }
}