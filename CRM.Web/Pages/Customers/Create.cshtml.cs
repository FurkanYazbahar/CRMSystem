using CRM.Web.Models;
using CRM.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRM.Web.Pages.Customers;

public class CreateModel : AuthenticatedPageModel
{
    private readonly ApiClient _apiClient;

    public CreateModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [BindProperty]
    public CustomerModel Customer { get; set; } = new();

    public void OnGet()
    {
        Customer.RegistrationDate = DateTime.Today;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _apiClient.PostAsync("api/customer", Customer);
        if (result)
            return RedirectToPage("Index");

        ModelState.AddModelError("", "An error occurred while creating the customer.");
        return Page();
    }
}
