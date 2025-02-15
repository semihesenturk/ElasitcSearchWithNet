using ES.ElasticSearch.Web.Repositories.Blog;
using ES.ElasticSearch.Web.ViewModel;

namespace ES.ElasticSearch.Web.Services.Blog;

public class BlogService(BlogRepository blogRepository)
{
    public async Task<bool> SaveAsync(BlogCreateViewModel request)
    {
        var blog = new Models.Blog.Blog
        {
            Title = request.Title,
            UserId = Guid.NewGuid(),
            Content = request.Content,
            Tags = request.Tags.Split(",")
        };

        var result = await blogRepository.SaveAsync(blog);
        return result != null;
    }

    public async Task<List<BlogViewModel>> SearchAsync(string searchText)
    {
        var blogList = await blogRepository.SearchAsync(searchText);

        return blogList.Select(s => new BlogViewModel()
        {
            Id = s.Id,
            Title = s.Title,
            Content = s.Content,
            Tags = string.Join(",", s.Tags),
            Created = s.Created.ToShortDateString(),
            UserId = s.UserId.ToString(),
        }).ToList();
    }
}