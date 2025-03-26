using Microsoft.AspNetCore.Http;
using VNPAY.NET.Models;

namespace FAKE.VNPAY;

public interface IVnpay
{
    void Initialize(string tmnCode, string hashSecret, string baseUrl, string callbackUrl, string version = "2.1.0", string orderType = "other");

    string GetPaymentUrl(PaymentRequest request);

    PaymentResult GetPaymentResult(IQueryCollection parameters);
}