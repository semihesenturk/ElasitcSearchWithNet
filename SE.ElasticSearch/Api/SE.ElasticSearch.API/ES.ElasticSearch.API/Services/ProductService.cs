using System.Collections.Immutable;
using System.Net;
using ES.ElasticSearch.API.Dtos;
using ES.ElasticSearch.API.Repositories;
using Nest;

namespace ES.ElasticSearch.API.Services;

public class ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
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

    public async Task<ResponseDto<ProductDto?>> GetByIdAsync(string id)
    {
        var hasProduct = await productRepository.GetByIdAsync(id);
        if (hasProduct == null)
            return ResponseDto<ProductDto>.Fail("Product not found", HttpStatusCode.NotFound);
        
        return ResponseDto<ProductDto>.Success(hasProduct.CreateDto(), HttpStatusCode.OK);
    }

    public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto updateProduct)
    {
        var isSuccess = await productRepository.UpdateAsync(updateProduct);
        
        if (!isSuccess)
            return ResponseDto<bool>.Fail("Failed to Update Product", HttpStatusCode.InternalServerError);
        
        return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        
    }

    public async Task<ResponseDto<bool>> DeleteAsync(string id)
    {
        var deleteResponse = await productRepository.DeleteAsync(id);

        if (!deleteResponse.IsValid && deleteResponse.Result == Result.NotFound)
        {
            return ResponseDto<bool>.Fail("Product not found for delete", HttpStatusCode.NotFound);
        }

        if (!deleteResponse.IsValid)
        {
            logger.LogError(
                $"Failed to delete product: {deleteResponse.OriginalException}," +
                $" Error: {deleteResponse.ServerError.Error}");
           
            return ResponseDto<bool>.Fail("Failed to Delete Product", HttpStatusCode.InternalServerError);
        }
        
        return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
    }
}