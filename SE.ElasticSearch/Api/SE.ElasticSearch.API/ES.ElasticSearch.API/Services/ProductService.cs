using System.Collections.Immutable;
using System.Net;
using ES.ElasticSearch.API.Dtos;
using ES.ElasticSearch.API.Repositories;

namespace ES.ElasticSearch.API.Services;

public class ProductService(ProductRepository productRepository)
{
    public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
    {
        var responseProduct = await productRepository.SaveAsync(request.CreateProduct());

        if (responseProduct == null)
            return ResponseDto<ProductDto>.Fail(["Failed to Save Product"], HttpStatusCode.InternalServerError);

        return ResponseDto<ProductDto>.Success(responseProduct.CreateDto(), HttpStatusCode.Created);
    }

    public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
    {
        var products = await productRepository.GetAllAsync();
        var productListDto = products.Select(x => x.CreateDto()).ToList();

        return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
    }
}