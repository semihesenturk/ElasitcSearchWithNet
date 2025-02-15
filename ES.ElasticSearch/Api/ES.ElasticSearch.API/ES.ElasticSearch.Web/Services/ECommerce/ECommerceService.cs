using ES.ElasticSearch.Web.Repositories.ECommerce;
using ES.ElasticSearch.Web.ViewModel.ECommerce;

namespace ES.ElasticSearch.Web.Services.ECommerce;

public class ECommerceService(ECommerceRepository repository)
{
    public async Task<(List<ECommerceViewModel>, long totalCount, long pageLinkCount)> SearchAsync(ECommerceSearchViewModel searchModel, int page, int pageSize)
    {
        var (eCommerceList, totalCount) = await repository.SearchAsync(searchModel, page, pageSize);

        var pageLinkCountCalculate = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
        long pageLinkCount = 0;

        if (pageLinkCountCalculate == 0)
        {
            pageLinkCount = totalCount / pageSize;
        }
        else
        {
            pageLinkCount = (totalCount / pageSize) + 1;
        }

        var eCommerceListViewModel = eCommerceList.Select(x => new ECommerceViewModel
        {
            Category = string.Join(",", x.Category),
            CustomerFullName = x.CustomerFullName,
            CustomerFirstName = x.CustomerFirstName,
            CustomerLastName = x.CustomerLastName,
            OrderDate = x.OrderDate.ToShortDateString(),
            CustomerGender = x.CustomerGender.ToLower(),
            Id = x.Id,
            OrderId = x.OrderId,
            TaxFullTotalPrice = x.TaxFullTotalPrice
        }).ToList();
        
        return (eCommerceListViewModel, totalCount, pageLinkCount);
    }
}