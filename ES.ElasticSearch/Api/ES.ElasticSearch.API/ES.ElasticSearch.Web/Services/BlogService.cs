using ES.ElasticSearch.Web.Models;
using ES.ElasticSearch.Web.Repositories;
using ES.ElasticSearch.Web.ViewModel;

namespace ES.ElasticSearch.Web.Services;

public class BlogService(BlogRepository blogRepository)
{
    public async Task<bool> SaveAsync(BlogCreateViewModel request)
    {
        var blog = new Blog
        {
            Title = request.Title,
            UserId = Guid.NewGuid(),
            Content = request.Content,
            Tags = request.Tags.Split(",")
        };

        var result = await blogRepository.SaveAsync(blog);
        return result != null;
    }
}