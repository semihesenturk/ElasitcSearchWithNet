using System.Collections.Immutable;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ES.ElasticSearch.API.Models.ECommerceModel;

namespace ES.ElasticSearch.API.Repositories;

public class ECommerceRepository(ElasticsearchClient client)
{
    private const string IndexName = "kibana_sample_data_ecommerce";

    public async Task<ImmutableList<ECommerce>> TermQueryAsync(string customerFirstName)
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

    public async Task<ImmutableList<ECommerce>> TermsQueryAsync(List<string> customerFirstNameList)
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

    public async Task<ImmutableList<ECommerce>> PrefixQueryAsync(string customerFullName)
    {
        var result = await client.SearchAsync<ECommerce>(
            s => s.Index(IndexName)
                .Size(100)
                .Query(q =>
                    q.Prefix(p => p
                        .Field(
                            f => f.CustomerFullName
                                .Suffix("keyword"))
                        .Value(customerFullName))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> RangeQueryAsync(double fromPrice, double toPrice)
    {
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Size(100)
            .Query(q => q
                .Range(r => r
                    .NumberRange(nr => nr
                        .Field(f => f.TaxFullTotalPrice)
                        .Gte(fromPrice).Lte(toPrice))
                )
            )
        );

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> MatchAllQueryAsync()
    {
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Size(100)
            .Query(q => q.MatchAll(_ => { })));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> PaginationQueryAsync(int page, int pageSize)
    {
        var pageFrom = (page - 1) * pageSize;
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Size(pageSize).From(pageFrom)
            .Query(q => q.MatchAll(_ => { })));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> WildCardQueryAsync(string customerFullName)
    {
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Query(q => q
                .Wildcard(w => w
                    .Field(f => f.CustomerFullName
                        .Suffix("keyword"))
                    .Wildcard(customerFullName))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> FuzzyQueryAsync(string customerName)
    {
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Query(q => q
                .Fuzzy(fz => fz
                    .Field(f => f.CustomerFirstName.Suffix("keyword"))
                    .Value(customerName)
                    .Fuzziness(new Fuzziness(2))))
            .Sort(sort => sort
                .Field(f => f.TaxFullTotalPrice,
                    new FieldSort { Order = SortOrder.Desc }))
        );

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> MatchQueryFullTextAsync(string categoryName)
    {
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Category)
                    .Query(categoryName)
                    .Operator(Operator.Or))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> MatchBooPrefixFullTextQueryAsync(string customerFullName)
    {
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Size(100)
            .Query(q => q
                .MatchBoolPrefix(m => m
                    .Field(f => f.CustomerFullName)
                    .Query(customerFullName)
                    .Operator(Operator.Or))));
        
        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> MatchPhraseFullTextQueryAsync(string customerFullName)
    {
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Query(q => q
                .MatchPhrase(m => m
                    .Field(f => f.CustomerFullName)
                    .Query(customerFullName))));
        
        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> CompoundQueryExampleOne(string cityName, double taxfulTotalPrice, string categoryName,string manufacturerName)
    {
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Size(100)
            .Query(q=>q
                .Bool(b=>b
                    .Must(m=>m
                        .Term(t=>t
                            .Field("geoip.city_name")
                            .Value(cityName)))
                    .MustNot(mn=>mn
                        .Range(r=>r
                            .NumberRange(nr=>nr
                                .Field(f=>f.TaxFullTotalPrice)
                                .Lte(taxfulTotalPrice))))
                    .Should(sh=>sh
                        .Term(st=>st
                            .Field(f=>f.Category.Suffix("keyword"))
                            .Value(categoryName))
                    )
                    .Filter(f=>f
                        .Term(ft=>ft
                            .Field("manufacturer.keyword")
                            .Value(manufacturerName)))
                ))
        );
        
        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }

    public async Task<ImmutableList<ECommerce>> CompoundQueryExampleTwo(string customerFullName)
    {
        var result = await client.SearchAsync<ECommerce>(s => s.Index(IndexName)
            .Query(q => q
                .Bool(b => b
                    .Should(m => m
                        .Match(mt => mt
                            .Field(f => f.CustomerFullName)
                            .Query(customerFullName))
                        .Prefix(p=>p
                            .Field(f=>f.CustomerFullName.Suffix("keyword"))
                            .Value(customerFullName))))
            )
        );
        
        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }
}