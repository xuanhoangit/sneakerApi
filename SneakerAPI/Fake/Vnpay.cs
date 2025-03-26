
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using VNPAY.Enums;
using VNPAY.Utilities;

namespace FAKE.VNPAY;

public class Vnpay : IVnpay
{
    private string _tmnCode;

    private string _hashSecret;

    private string _callbackUrl;

    private string _baseUrl;

    private string _version;

    private string _orderType;

    public void Initialize(string tmnCode, string hashSecret, string baseUrl, string callbackUrl, string version = "2.1.0", string orderType = "other")
    {
        _tmnCode = tmnCode;
        _hashSecret = hashSecret;
        _callbackUrl = callbackUrl;
        _baseUrl = baseUrl;
        _version = version;
        _orderType = orderType;
        EnsureParametersBeforePayment();
    }

    public string GetPaymentUrl(PaymentRequest request)
    {
        EnsureParametersBeforePayment();
        if (request.Money < 5000.0 || request.Money > 1000000000.0)
        {
            throw new ArgumentException("Số tiền thanh toán phải nằm trong khoảng 5.000 (VND) đến 1.000.000.000 (VND).");
        }

        if (string.IsNullOrEmpty(request.Description))
        {
            throw new ArgumentException("Không được để trống mô tả giao dịch.");
        }

        if (string.IsNullOrEmpty(request.IpAddress))
        {
            throw new ArgumentException("Không được để trống địa chỉ IP.");
        }

        PaymentHelper paymentHelper = new PaymentHelper();
        paymentHelper.AddRequestData("vnp_Version", _version);
        paymentHelper.AddRequestData("vnp_Command", "pay");
        paymentHelper.AddRequestData("vnp_TmnCode", _tmnCode);
        paymentHelper.AddRequestData("vnp_Amount", (request.Money * 100.0).ToString());
        paymentHelper.AddRequestData("vnp_CreateDate", request.CreatedDate.ToString("yyyyMMddHHmmss"));
        paymentHelper.AddRequestData("vnp_CurrCode", request.Currency.ToString().ToUpper());
        paymentHelper.AddRequestData("vnp_IpAddr", request.IpAddress);
        paymentHelper.AddRequestData("vnp_Locale", EnumHelper.GetDescription(request.Language));
        paymentHelper.AddRequestData("vnp_BankCode", (request.BankCode == BankCode.ANY) ? string.Empty : request.BankCode.ToString());
        paymentHelper.AddRequestData("vnp_OrderInfo", request.Description.Trim());
        paymentHelper.AddRequestData("vnp_OrderType", _orderType);
        paymentHelper.AddRequestData("vnp_ReturnUrl", _callbackUrl);
        paymentHelper.AddRequestData("vnp_TxnRef", request.PaymentId.ToString());
        return paymentHelper.GetPaymentUrl(_baseUrl, _hashSecret);
    }

    public PaymentResult GetPaymentResult(IQueryCollection parameters)
    {
        Dictionary<string, string> dictionary = parameters.Where<KeyValuePair<string, StringValues>>((KeyValuePair<string, StringValues> kv) => !string.IsNullOrEmpty(kv.Key) && kv.Key.StartsWith("vnp_")).ToDictionary((KeyValuePair<string, StringValues> kv) => kv.Key, (KeyValuePair<string, StringValues> kv) => kv.Value.ToString());
        string valueOrDefault = dictionary.GetValueOrDefault("vnp_BankCode");
        string valueOrDefault2 = dictionary.GetValueOrDefault("vnp_BankTranNo");
        string valueOrDefault3 = dictionary.GetValueOrDefault("vnp_CardType");
        string valueOrDefault4 = dictionary.GetValueOrDefault("vnp_PayDate");
        string valueOrDefault5 = dictionary.GetValueOrDefault("vnp_OrderInfo");
        string valueOrDefault6 = dictionary.GetValueOrDefault("vnp_TransactionNo");
        string valueOrDefault7 = dictionary.GetValueOrDefault("vnp_ResponseCode");
        string valueOrDefault8 = dictionary.GetValueOrDefault("vnp_TransactionStatus");
        string valueOrDefault9 = dictionary.GetValueOrDefault("vnp_TxnRef");
        string valueOrDefault10 = dictionary.GetValueOrDefault("vnp_SecureHash");
        if (string.IsNullOrEmpty(valueOrDefault) || string.IsNullOrEmpty(valueOrDefault5) || string.IsNullOrEmpty(valueOrDefault6) || string.IsNullOrEmpty(valueOrDefault7) || string.IsNullOrEmpty(valueOrDefault8) || string.IsNullOrEmpty(valueOrDefault9) || string.IsNullOrEmpty(valueOrDefault10))
        {
            throw new ArgumentException("Không đủ dữ liệu để xác thực giao dịch");
        }

        PaymentHelper paymentHelper = new PaymentHelper();
        foreach (var (text3, value) in dictionary)
        {
            if (!text3.Equals("vnp_SecureHash"))
            {
                paymentHelper.AddResponseData(text3, value);
            }
        }

        ResponseCode responseCode = (ResponseCode)sbyte.Parse(valueOrDefault7);
        TransactionStatusCode transactionStatusCode = (TransactionStatusCode)sbyte.Parse(valueOrDefault8);
        return new PaymentResult
        {
            PaymentId = long.Parse(valueOrDefault9),
            VnpayTransactionId = long.Parse(valueOrDefault6),
            IsSuccess = (transactionStatusCode == TransactionStatusCode.Code_00 && responseCode == ResponseCode.Code_00 && paymentHelper.IsSignatureCorrect(valueOrDefault10, _hashSecret)),
            Description = valueOrDefault5,
            PaymentMethod = (string.IsNullOrEmpty(valueOrDefault3) ? "Không xác định" : valueOrDefault3),
            Timestamp = (string.IsNullOrEmpty(valueOrDefault4) ? DateTime.Now : DateTime.ParseExact(valueOrDefault4, "yyyyMMddHHmmss", CultureInfo.InvariantCulture)),
            TransactionStatus = new TransactionStatus
            {
                Code = transactionStatusCode,
                Description = EnumHelper.GetDescription(transactionStatusCode)
            },
            PaymentResponse = new PaymentResponse
            {
                Code = responseCode,
                Description = EnumHelper.GetDescription(responseCode)
            },
            BankingInfor = new BankingInfor
            {
                BankCode = valueOrDefault,
                BankTransactionId = (string.IsNullOrEmpty(valueOrDefault2) ? "Không xác định" : valueOrDefault2)
            }
        };
    }

    private void EnsureParametersBeforePayment()
    {
        if (string.IsNullOrEmpty(_baseUrl) || string.IsNullOrEmpty(_tmnCode) || string.IsNullOrEmpty(_hashSecret) || string.IsNullOrEmpty(_callbackUrl))
        {
            throw new ArgumentException("Không tìm thấy BaseUrl, TmnCode, HashSecret, hoặc CallbackUrl");
        }
    }
}