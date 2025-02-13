using System.Net;
using ES.ElasticSearch.API.Dtos;
using ES.ElasticSearch.API.Dtos.ECommerce;
using ES.ElasticSearch.API.Repositories;

namespace ES.ElasticSearch.API.Services;

public class ECommerceService(ECommerceRepository eCommerceRepository, ILogger<ECommerceService> logger)
{
    private readonly ILogger<ECommerceService> _logger = logger;

    public async Task<ResponseDto<List<ECommerceDto>>> GetWithCustomerFirstNameTerm(string customerFirstName)
    {
        var getResponse = await eCommerceRepository.TermQueryAsync(customerFirstName);
        var responseListDto = getResponse.Select(x=>x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> TermsQuery(List<string> customerFirstNameList)
    {
        var getResponse = await eCommerceRepository.TermsQueryAsync(customerFirstNameList);
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> PrefixQuery(string customerFullName)
    {
        var getResponse = await eCommerceRepository.PrefixQueryAsync(customerFullName);
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> RangeQuery(double fromPrice, double toPrice)
    {
        var getResponse = await eCommerceRepository.RangeQueryAsync(fromPrice, toPrice);
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> MatchAllQuery()
    {
        var getResponse = await eCommerceRepository.MatchAllQueryAsync();
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> PaginationQueryAsync(int page, int pageSize)
    {
        var getResponse = await eCommerceRepository.PaginationQueryAsync(page, pageSize);
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> WildCardQueryAsync(string customerFullName)
    {
        var getResponse = await eCommerceRepository.WildCardQueryAsync(customerFullName);
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> FuzzyQueryAsync(string customerName)
    {
        var getResponse = await eCommerceRepository.FuzzyQueryAsync(customerName);
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> MatchQueryFullTextAsync(string categoryName)
    {
        var getResponse = await eCommerceRepository.MatchQueryFullTextAsync(categoryName);
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> MatchBooPrefixFullTextQueryAsync(string customerFullName)
    {
        var getResponse = await eCommerceRepository.MatchBooPrefixFullTextQueryAsync(customerFullName);
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<List<ECommerceDto>>> MatchPhraseFullTextQueryAsync(string customerFullName)
    {
        var getResponse = await eCommerceRepository.MatchPhraseFullTextQueryAsync(customerFullName);
        var responseListDto = getResponse.Select(x => x.CreateDto()).ToList();
        
        return ResponseDto<List<ECommerceDto>>.Success(responseListDto, HttpStatusCode.OK);
    }
}