using CRM.Web.Models;
using CRM.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRM.Web.Pages.Customers;

public class IndexModel : AuthenticatedPageModel
{
    private readonly ApiClient _apiClient;

    public IndexModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public List<CustomerModel> Customers { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? SearchName { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchRegion { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? SearchDate { get; set; }

    public async Task OnGetAsync()
    {
        var query = $"?name={SearchName}&region={SearchRegion}&date={SearchDate?.ToString("yyyy-MM-dd")}";
        Customers = (await _apiClient.GetAsync<List<CustomerModel>>($"api/customer/filter{query}")) ?? new List<CustomerModel>();
    }
}
