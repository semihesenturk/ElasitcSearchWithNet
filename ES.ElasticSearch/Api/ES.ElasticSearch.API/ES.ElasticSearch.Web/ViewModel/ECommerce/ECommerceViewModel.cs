namespace ES.ElasticSearch.Web.ViewModel.ECommerce;

public class ECommerceViewModel
{
    public string Id { get; set; } = null!;
    public string CustomerFirstName { get; set; } = null!;
    public string CustomerLastName { get; set; } = null!;
    public string CustomerFullName { get; set; } = null!;
    public string CustomerGender { get; set; } = null!;
    public double TaxFullTotalPrice { get; set; }

    public string Category { get; set; } = null!;
    public int OrderId { get; set; }
    public string OrderDate { get; set; }
}