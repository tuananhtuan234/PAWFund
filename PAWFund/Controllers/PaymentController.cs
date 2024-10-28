using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data.Entity;
using Services.Interface;
using Services.Models.Request;
using Services.Services;

namespace PAWFund.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;
        private readonly IUserServices _userServices;
        private readonly IDonationServices _donationServices;
        public PaymentController(IPaymentServices paymentServices, IUserServices userServices, IDonationServices donationServices)
        {
            _paymentServices = paymentServices;
            _userServices = userServices;
            _donationServices = donationServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentById(string id)
        {
            var result = await _paymentServices.GetPaymentById(id);
            return Ok(result);
        }

        [HttpPost("payment/vnpay")]
        public async Task<IActionResult> AddPayment(string donationId, string userId)
        {
            var user = await _userServices.GetUserById(userId);
            var donation = await _donationServices.GetDonationById(donationId);
            try
            {
                var vnPayModel = new VnPaymentRequestModel()
                {
                    Amount = donation.Amount,
                    CreatedDate = DateTime.Now,
                    Description = "thanh toán VnPay",
                    OrderId = donation.DonationId,
                    FullName = user.FullName,
                };
                if (vnPayModel.Amount < 0)
                {
                    return BadRequest("The amount entered cannot be less than 0. Please try again");
                }
                var paymentUrl = _paymentServices.CreatePaymentUrl(HttpContext, vnPayModel, userId);
                return Ok(new { url = paymentUrl });
                //return Redirect(_vpnPayServices.CreatePaymentUrl(HttpContext, vnPayModel, userId));
                //return new JsonResult(_vpnPayServices.CreatePaymentUrl(HttpContext, vnPayModel, userId));               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PaymentBack")]
        public async Task<IActionResult> PaymenCalltBack()
        {
            var queryParameters = HttpContext.Request.Query;
            // Kiểm tra và lấy giá trị 'vnp_OrderInfo' từ Query
            string orderInfo = queryParameters["vnp_OrderInfo"];
            string userId = _paymentServices.GetUserId(orderInfo);
            string orderId = _paymentServices.GetOrderId(orderInfo);
            double amount = double.Parse(queryParameters["vnp_Amount"]);
            if (string.IsNullOrEmpty(orderInfo))
            {
                return BadRequest("Thông tin đơn hàng không tồn tại.");
            }
            // Phân tích chuỗi 'orderInfo' để lấy các thông tin cần thiết
            var orderInfoDict = new Dictionary<string, string>();
            string[] pairs = orderInfo.Split(',');
            foreach (var pair in pairs)
            {
                string[] keyValue = pair.Split(':');
                if (keyValue.Length == 2)
                {
                    orderInfoDict[keyValue[0].Trim()] = keyValue[1].Trim();
                }
            }

            //Tạo và lưu trữ thông tin giao dịch
            var paymentDto = new PaymentDtos()
            {
                PaymentId = Guid.NewGuid().ToString(),
                DonationId = orderId,
                Status = Repository.Data.Enum.PaymentStatus.Completed,
                /*Amount = (float)amount / 100, */ // Chia cho 100 nếu giá trị 'amount' là theo đơn vị nhỏ nhất của tiền tệ
                Method = Repository.Data.Enum.Method.Banking,             
            };
            await _paymentServices.AddPayment(paymentDto);

            var donationUpdateRequest = new DonationUpdateRequest()
            {
                Amount = (float)amount / 100,
            };
            var result = await _donationServices.UpdateDonation(orderId, donationUpdateRequest);

            if (result == "Update Successfully")
            {
                return Redirect("http://localhost:5000/" /*+ userId*/); // thay đổi đường link
            }
            return BadRequest("Invalid transaction data.");
        }
    }
}
