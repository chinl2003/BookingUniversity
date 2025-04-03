using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Entities;
using Services.IService;

namespace BookingManagementRazorPages.Pages.Manager.CampusManagement
{
    public class CreateCampusPageModel : PageModel
    {
        private readonly ICampusService _campusService;

        public CreateCampusPageModel(ICampusService campusService)
        {
            _campusService = campusService;
        }

        [BindProperty]
        public Campus Campus { get; set; }

        public void OnGet()
        {
        }

        public  IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Campus newCampus = new Campus
            {
                Name = Campus.Name,
                Code = Campus.Code,
                Location = Campus.Location
            };

            bool result = _campusService.CreateCampus(newCampus);

            if (result)
            {
                return RedirectToPage("/Manager/CampusManagement/CampusManagementPage");
            }

            ModelState.AddModelError("", "Có lỗi xảy ra khi tạo campus. Vui lòng thử lại.");
            return Page();
        }
    }

}
