using System;
using VNPAY.Enums;

namespace VNPAY.Models;

public class PaymentRequest
{
    public long PaymentId { get; set; }

    public string Description { get; set; }

    public double Money { get; set; }

    public string IpAddress { get; set; }

    public BankCode BankCode { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;


    public Currency Currency { get; set; }

    public DisplayLanguage Language { get; set; }
}