using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using MobileMekaniko.Models.Dto;
using MobileMekaniko.Repository.IRepository;

namespace MobileMekaniko.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        // GET: Customer List
        public async Task<IActionResult> GetCustomers()
        {
            return View();
        }

        // GET: Populate Customer List Table
        public async Task<IActionResult> PopulateCustomerTable()
        {
            var customer = await _unitOfWork.Customer.GetCustomers();
            return Json(customer);
        }

        // POST: Add Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(AddCustomerDto model)
        {
            if(ModelState.IsValid)
            {
                await _unitOfWork.Customer.AddCustomerAsync(model);
                return Json("Customer added successfully");
            }
            return Json("Unable to add customer.");
        }

        // GET : Update Delete Modal
        public async Task<IActionResult> UpdateDeleteCustomer(int id)
        {
            var customer = await _unitOfWork.Customer.GetCustomerForUpdateDeleteAsync(id);
            if (customer != null)
            {
                return Json(customer);
            }
            return Json("Invalid Id");
        }

        // POST : Delete Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                // Delete Customer
                await _unitOfWork.Customer.DeleteCustomerByIdAsync(id);
                return Json(new { success = true, message = "Customer deleted successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting customer." });
            }

        }

        // POST : Update Customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer(UpdateDeleteCustomerDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the customer
                    await _unitOfWork.Customer.UpdateCustomerByIdAsync(model);

                    return Json(new { success = true, message = "Customer updated successfully." });
                }
                catch (KeyNotFoundException ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "An error occurred while trying to delete customer." });
                }
            }
            return Json(new { success = false, message = "Model validation failed." });
        }
    }
}
