using System.ComponentModel.DataAnnotations;

namespace ES.ElasticSearch.Web.ViewModel;

public class BlogCreateViewModel
{
    [Display(Name = "Blog Title")]
    [Required]
    public string Title { get; set; } = null!;
    
    [Display(Name = "Blog Content")]
    [Required]
    public string Content { get; set; } = null!;
    
    [Display(Name = "Tags")]
    public string Tags { get; set; } 
}