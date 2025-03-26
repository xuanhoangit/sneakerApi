﻿namespace SneakerAPI.Core.Models.Vnpay
{
    /// <summary>
    /// Phản hồi từ VNPAY sau khi thực hiện giao dịch thanh toán.
    /// </summary>
    public class PaymentResult
    {
        /// <summary>
        /// Mã tham chiếu giao dịch (Transaction Reference). Đây là mã số duy nhất dùng để xác định giao dịch.
        /// </summary>
        public long PaymentId { get; set; }

        /// <summary>
        /// Trạng thái thành công của giao dịch. 
        /// Giá trị là <c>true</c> nếu chữ ký chính xác, <see cref="PaymentResponse.ResponseCode"/> và <see cref="TransactionStatus"/> đều bằng <c>0</c>.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Thông tin mô tả nội dung thanh toán, viết bằng tiếng Việt không dấu.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Thời gian phản hồi từ VNPAY, được ghi nhận tại thời điểm giao dịch kết thúc.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Mã giao dịch được ghi nhận trên hệ thống VNPAY, đại diện cho giao dịch duy nhất tại VNPAY.
        /// </summary>
        public long VnpayTransactionId { get; set; }

        /// <summary>
        /// Phương thức thanh toán được sử dụng, ví dụ: thẻ tín dụng, ví điện tử, hoặc chuyển khoản ngân hàng.
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Phản hồi chi tiết từ hệ thống VNPAY về giao dịch.
        /// </summary>
        public PaymentResponse PaymentResponse { get; set; }

        /// <summary>
        /// Trạng thái giao dịch sau khi thực hiện, ví dụ: Chờ xử lý, Thành công, hoặc Thất bại.
        /// </summary>
        public TransactionStatus TransactionStatus { get; set; }

        /// <summary>
        /// Thông tin ngân hàng liên quan đến giao dịch, bao gồm tên ngân hàng và mã ngân hàng.
        /// </summary>
        public BankingInfor BankingInfor { get; set; }
        /// <summary>
        /// Loại giao dịch
        /// </summary>
        public string Command { get; set; }
    }
}