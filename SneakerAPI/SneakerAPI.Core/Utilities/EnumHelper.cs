﻿using System.ComponentModel;
using System.Reflection;

namespace SneakerAPI.Core.Utilities
{
    public static class EnumHelper
    {
        /// <summary>
        /// Lấy mô tả của giá trị enum thông qua thuộc tính Description nếu có.
        /// Nếu giá trị enum không có mô tả, phương thức này sẽ trả về tên của giá trị enum.
        /// </summary>
        /// <param name="value">Giá trị enum cần lấy mô tả.</param>
        /// <returns>Mô tả của giá trị enum nếu có, nếu không thì trả về tên giá trị enum.</returns>
        public static string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return value.ToString();
            }
            DescriptionAttribute? attribute = (DescriptionAttribute?)field.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
