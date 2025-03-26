﻿using SneakerAPI.Core.VnpayEnums;

namespace SneakerAPI.Core.Models.Vnpay
{
    /// <summary>
    /// Yêu cầu thanh toán gửi đến cổng thanh toán VNPAY.
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        /// Mã tham chiếu giao dịch (Transaction Reference). Đây là mã số duy nhất dùng để xác định giao dịch.  
        /// Lưu ý: Giá trị này bắt buộc và cần đảm bảo không bị trùng lặp giữa các giao dịch.
        /// </summary>
        public required long PaymentId { get; set; }

        /// <summary>
        /// Thông tin mô tả nội dung thanh toán, không dấu và không bao gồm các ký tự đặc biệt
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Số tiền phải nằm trong khoảng 5.000 (VND) đến 1.000.000.000 (VND).
        /// </summary>
        public required double Money { get; set; }

        /// <summary>
        /// Địa chỉ IP của thiết bị thực hiện giao dịch.  
        /// </summary>
        public required string IpAddress { get; set; }

        /// <summary>
        /// Mã phương thức thanh toán, mã loại ngân hàng hoặc ví điện tử thanh toán. Nếu mang giá trị <c>BankCode.ANY</c> thì chuyển hướng người dùng sang VNPAY chọn phương thức thanh toán.
        /// </summary>
        public BankCode BankCode { get; set; } = BankCode.ANY;

        /// <summary>
        /// Thời điểm khởi tạo giao dịch. Giá trị mặc định là ngày và giờ hiện tại tại thời điểm yêu cầu được khởi tạo.  
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
        /// </summary>
        public Currency Currency { get; set; } = Currency.VND;

        /// <summary>
        /// Ngôn ngữ hiển thị trên giao diện thanh toán của VNPAY, mặc định là tiếng Việt.  
        /// </summary>
        public DisplayLanguage Language { get; set; } = DisplayLanguage.Vietnamese;
        public string Command {get;set;}=CommandType.Pay.ToString().ToLower();
    }
}
