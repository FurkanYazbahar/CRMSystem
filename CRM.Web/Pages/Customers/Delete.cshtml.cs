using CRM.Web.Models;
using CRM.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRM.Web.Pages.Customers;

public class DeleteModel : AuthenticatedPageModel
{
    private readonly ApiClient _apiClient;

    public DeleteModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [BindProperty]
    public CustomerModel Customer { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var customer = await _apiClient.GetAsync<CustomerModel>($"api/customer/{id}");
        if (customer == null)
            return RedirectToPage("Index");

        Customer = customer;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _apiClient.DeleteAsync($"api/customer/{Customer.Id}");
        if (result)
            return RedirectToPage("Index");

        ModelState.AddModelError("", "An error occurred while deleting the customer.");
        return Page();
    }
}
