using System.Collections.Immutable;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ES.ElasticSearch.API.Models.ECommerceModel;

namespace ES.ElasticSearch.API.Repositories;

public class ECommerceRepository(ElasticsearchClient client)
{
    private const string IndexName = "kibana_sample_data_ecommerce";

    public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
    {
        //First way to search
        // var result = await client.SearchAsync<ECommerce>(
        //     s => s.Index(IndexName).Query(
        //         q => q.Term(
        //             t => t.Field("customer_first_name.keyword"!).Value(customerFirstName)
        //         )));

        // Second way to search
        var result = await client.SearchAsync<ECommerce>(
            s => s.Index(IndexName)
                .Query(
                    q => q.Term(
                        t => t.Field(f => f.CustomerFirstName.Suffix("keyword"))
                            .Value(customerFirstName))
                ));

        //Another way to search
        // var termQuery = new TermQuery("customer_first_name.keyword"!)
        // {
        //     Value = customerFirstName,
        //     CaseInsensitive = true
        // };
        //
        // var result = await client.SearchAsync<ECommerce>(
        //     s => s.Index(IndexName)
        //         .Query(termQuery)
        // );

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNameList)
    {
        var terms = new List<FieldValue>();
        customerFirstNameList.ForEach(x =>
            terms.Add(x)
        );

        //First way to search
        // var termsQuery = new TermsQuery
        // {
        //     Field = "customer_first_name.keyword",
        //     Terms = new TermsQueryField(terms.AsReadOnly())
        // };
        //
        // var result =await client.SearchAsync<ECommerce>(s =>
        //     s.Index(IndexName).Query(termsQuery));
        
        //Second way to search
        var result = await client.SearchAsync<ECommerce>(
            s => s.Index(IndexName)
                .Size(100)
                .Query(q => q
                    .Terms(t => t
                        .Field(f => f.CustomerFirstName
                            .Suffix("keyword"))
                        .Terms(new TermsQueryField(terms.AsReadOnly())))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }
}