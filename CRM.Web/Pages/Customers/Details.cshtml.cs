using CRM.Web.Models;
using CRM.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRM.Web.Pages.Customers;

public class DetailsModel : AuthenticatedPageModel
{
    private readonly ApiClient _apiClient;

    public DetailsModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public CustomerModel Customer { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var customer = await _apiClient.GetAsync<CustomerModel>($"api/customer/{id}");
        if (customer == null)
            return RedirectToPage("Index");

        Customer = customer;
        return Page();
    }
}
