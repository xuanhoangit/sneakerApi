using System.ComponentModel;

namespace VNPAY.Enums;
public enum TransactionStatusCode : sbyte
{
    [Description("Giao dịch thành công")]
    Code_00 = 0,
    [Description("Giao dịch chưa hoàn tất")]
    Code_01 = 1,
    [Description("Giao dịch bị lỗi")]
    Code_02 = 2,
    [Description("Khách hàng đã bị trừ tiền tại ngân hàng nhưng giao dịch chưa thành công ở VNPAY")]
    Code_04 = 4,
    [Description("VNPAY đang xử lý giao dịch này (hoàn tiền)")]
    Code_05 = 5,
    [Description("VNPAY đã gửi yêu cầu hoàn tiền đến ngân hàng (hoàn tiền)")]
    Code_06 = 6,
    [Description("Giao dịch bị nghi ngờ gian lận")]
    Code_07 = 7,
    [Description("Giao dịch hoàn trả bị từ chối")]
    Code_09 = 9
}
public enum ResponseCode : sbyte
{
    [Description("Giao dịch thành công")]
    Code_00 = 0,
    [Description("Trừ tiền thành công. Giao dịch bị nghi ngờ (liên quan tới lừa đảo, giao dịch bất thường)")]
    Code_07 = 7,
    [Description("Thẻ hoặc tài khoản chưa đăng ký dịch vụ Internet Banking tại ngân hàng")]
    Code_09 = 9,
    [Description("Xác thực thông tin thẻ hoặc tài khoản không đúng quá 3 lần")]
    Code_10 = 10,
    [Description("Hết hạn chờ thanh toán")]
    Code_11 = 11,
    [Description("Thẻ hoặc tài khoản ngân hàng của quý khách bị khóa")]
    Code_12 = 12,
    [Description("Mã OTP không chính xác")]
    Code_13 = 13,
    [Description("Giao dịch bị hủy bởi người dùng")]
    Code_24 = 24,
    [Description("Tài khoản của không đủ số dư để thực hiện giao dịch")]
    Code_51 = 51,
    [Description("Tài khoản đã vượt quá hạn mức giao dịch trong ngày")]
    Code_65 = 65,
    [Description("Ngân hàng thanh toán đang bảo trì")]
    Code_75 = 75,
    [Description("Nhập sai mật khẩu thanh toán quá số lần quy định")]
    Code_79 = 79,
    [Description("Lỗi không xác định")]
    Code_99 = 99
}
public enum DisplayLanguage
{
    [Description("vn")]
    Vietnamese,
    [Description("en")]
    English
}
public enum Currency
{
    [Description("Việt Nam đồng")]
    VND
}
public enum BankCode : sbyte
{
    [Description("Tất cả phương thức thanh toán")]
    ANY,
    [Description("Thanh toán quét mã QR")]
    VNPAYQR,
    [Description("Thẻ ATM hoặc tài khoản ngân hàng tại Việt Nam")]
    VNBANK,
    [Description("Thẻ thanh toán quốc tế")]
    INTCARD
}