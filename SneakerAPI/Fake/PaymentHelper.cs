#region Assembly VNPAY.NET, Version=8.5.0.0, Culture=neutral, PublicKeyToken=null
// C:\Users\WelCome To BVCN02\.nuget\packages\vnpay.net\8.5.0\lib\net8.0\VNPAY.NET.dll
// Decompiled with ICSharpCode.Decompiler 8.2.0.7535
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;

namespace VNPAY.Utilities;
public static class EnumHelper
{
    public static string GetDescription(Enum value)
    {
        DescriptionAttribute descriptionAttribute = (DescriptionAttribute)value.GetType().GetField(value.ToString()).GetCustomAttribute(typeof(DescriptionAttribute));
        if (descriptionAttribute != null)
        {
            return descriptionAttribute.Description;
        }

        return value.ToString();
    }
}
internal class Comparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        if (x == y)
        {
            return 0;
        }

        if (x == null)
        {
            return -1;
        }

        if (y == null)
        {
            return 1;
        }

        return CompareInfo.GetCompareInfo("en-US").Compare(x, y, CompareOptions.Ordinal);
    }
}
internal class PaymentHelper
{
    private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new Comparer());

    private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new Comparer());

    internal void AddRequestData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _requestData.Add(key, value);
        }
    }

    internal string GetPaymentUrl(string baseUrl, string hashSecret)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (KeyValuePair<string, string> item in _requestData.Where<KeyValuePair<string, string>>((KeyValuePair<string, string> kv) => !string.IsNullOrEmpty(kv.Value)))
        {
            item.Deconstruct(out var key, out var value);
            string value2 = key;
            string value3 = value;
            StringBuilder stringBuilder2 = stringBuilder;
            StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(2, 2, stringBuilder2);
            handler.AppendFormatted(WebUtility.UrlEncode(value2));
            handler.AppendLiteral("=");
            handler.AppendFormatted(WebUtility.UrlEncode(value3));
            handler.AppendLiteral("&");
            stringBuilder2.Append(ref handler);
        }

        if (stringBuilder.Length > 0)
        {
            stringBuilder.Length--;
        }

        string text = stringBuilder.ToString();

        string value4 = Encoder.AsHmacSHA512(hashSecret, text);
        return $"{baseUrl}?{text}&vnp_SecureHash={WebUtility.UrlEncode(value4)}";
    }

    internal void AddResponseData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _responseData.Add(key, value);
        }
    }

    internal bool IsSignatureCorrect(string? inputHash, string secretKey)
    {
        if (string.IsNullOrEmpty(inputHash))
        {
            return false;
        }

        string responseData = GetResponseData();
        return Encoder.AsHmacSHA512(secretKey, responseData).Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
    }

    internal string GetResponseData()
    {
        _responseData.Remove("vnp_SecureHashType");
        _responseData.Remove("vnp_SecureHash");
        IEnumerable<string> values = from kv in _responseData
                                     where !string.IsNullOrEmpty(kv.Value)
                                     select WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value);
        return string.Join("&", values);
    }

    internal string GetResponseValue(string key)
    {
        if (!_responseData.TryGetValue(key, out string value))
        {
            return string.Empty;
        }

        return value;
    }
}
