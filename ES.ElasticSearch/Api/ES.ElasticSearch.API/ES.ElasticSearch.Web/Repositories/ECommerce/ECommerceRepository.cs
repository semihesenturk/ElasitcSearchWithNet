using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ES.ElasticSearch.Web.ViewModel.ECommerce;

namespace ES.ElasticSearch.Web.Repositories.ECommerce;

public class ECommerceRepository(ElasticsearchClient client)
{
    private const string IndexName = "kibana_sample_data_ecommerce";

    public async Task<(List<Models.ECommerce.ECommerce> list, long count)> SearchAsync(
        ECommerceSearchViewModel searchViewModel, int page, int pageSize)
    {
        List<Action<QueryDescriptor<Models.ECommerce.ECommerce>>> listQuery = [];

        if (searchViewModel is null)
        {
            listQuery.Add(q => q.MatchAll(_ => { }));
            return await CalculateResultSet(page, pageSize, listQuery);
        }

        if (!string.IsNullOrEmpty(searchViewModel.Category))
        {
            Action<QueryDescriptor<Models.ECommerce.ECommerce>> query = q => q
                .Match(m => m
                    .Field(f => f.Category)
                    .Query(searchViewModel.Category));

            listQuery.Add(query);
        }

        if (!string.IsNullOrEmpty(searchViewModel.CustomerFullName))
        {
            Action<QueryDescriptor<Models.ECommerce.ECommerce>> query = q => q
                .Match(m => m
                    .Field(f => f.CustomerFullName)
                    .Query(searchViewModel.CustomerFullName));

            listQuery.Add(query);
        }

        if (searchViewModel.OrderDateStart.HasValue)
        {
            Action<QueryDescriptor<Models.ECommerce.ECommerce>> query = q => q
                .Range(r => r
                    .DateRange(dr => dr
                        .Field(f => f.OrderDate)
                        .Gte(searchViewModel.OrderDateStart.Value)));

            listQuery.Add(query);
        }

        if (searchViewModel.OrderDateEnd.HasValue)
        {
            Action<QueryDescriptor<Models.ECommerce.ECommerce>> query = q => q
                .Range(r => r
                    .DateRange(dr => dr
                        .Field(f => f.OrderDate)
                        .Lte(searchViewModel.OrderDateEnd.Value)));

            listQuery.Add(query);
        }

        if (!string.IsNullOrEmpty(searchViewModel.Gender))
        {
            Action<QueryDescriptor<Models.ECommerce.ECommerce>> query = q => q
                .Term(t => t
                    .Field(f => f.CustomerGender)
                    .Value(searchViewModel.Gender).CaseInsensitive());

            listQuery.Add(query);
        }

        if (listQuery.Count == 0)
        {
            listQuery.Add(q=>q.MatchAll(_ => {}));
        }

        return await CalculateResultSet(page, pageSize, listQuery);
    }

    private async Task<(List<Models.ECommerce.ECommerce> list, long count)> CalculateResultSet(int page, int pageSize, List<Action<QueryDescriptor<Models.ECommerce.ECommerce>>> listQuery)
    {
        var pageFrom = (page - 1) * pageSize;
        var result = await client.SearchAsync<Models.ECommerce.ECommerce>(
            s => s.Index(IndexName)
                .Size(pageSize)
                .From(pageFrom)
                .Query(q => q
                    .Bool(b => b
                        .Must(listQuery.ToArray())))
        );

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return (list: result.Documents.ToList(), count: result.Total);
    }
}