using VNPAY.Enums;

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
public class PaymentResult
{
    public long PaymentId { get; set; }

    public bool IsSuccess { get; set; }

    public string Description { get; set; }

    public DateTime Timestamp { get; set; }

    public long VnpayTransactionId { get; set; }

    public string PaymentMethod { get; set; }

    public PaymentResponse PaymentResponse { get; set; }

    public TransactionStatus TransactionStatus { get; set; }

    public BankingInfor BankingInfor { get; set; }
}
public class BankingInfor
{
    public string BankCode { get; set; }

    public string BankTransactionId { get; set; }
}
public class PaymentResponse
{
    public ResponseCode Code { get; set; }

    public string Description { get; set; }
}

public class TransactionStatus
{
    public TransactionStatusCode Code { get; set; }

    public string Description { get; set; }
}