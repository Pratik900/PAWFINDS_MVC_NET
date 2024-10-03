using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razorpaycore8.Models;
using Razorpaycore8.Service;
using System.Diagnostics;
using WebApplication1.Data;

namespace Razorpaycore8.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _service;
        private IHttpContextAccessor _httpContextAccessor;
        public PaymentController(AppDbContext context, ILogger<PaymentController> logger, IPaymentService service, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProcessRequestOrder(PaymentRequest _paymentRequest)
        {
            MerchantOrder _marchantOrder = await _service.ProcessMerchantOrder(_paymentRequest);
            return View("Payment", _marchantOrder);
        }
        [HttpPost]
        public async Task<IActionResult> CompleteOrderProcess(string orderId, string razorpayKey, int amount, string currency, string name, string email, string phoneNumber, string address, string description, string uniqueId)
        {
            string PaymentMessage = await _service.CompleteOrderProcess(_httpContextAccessor);
            if (PaymentMessage == "captured")
            {
                var merchantOrder = new MerchantOrder
                {
                    OrderId = orderId,
                    RazorpayKey = razorpayKey,
                    Amount = amount,
                    Currency = currency,
                    Name = name,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Description = description,
                    UniqueID = uniqueId
                };

                Guid id = Guid.Parse(uniqueId);
                var petDetail = await _context.PetDetails.FindAsync(id);
                petDetail.IsAdopted = true;
                await _context.SaveChangesAsync();

                _context.MerchantOrders.Add(merchantOrder);
                await _context.SaveChangesAsync();

                return RedirectToAction("UserPetDetailIndex", "PetDetails");
                //return View("Success");
            }
            else
            {
                return View("Failed");
            }
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Failed()
        {
            return View();
        }
    }
}
