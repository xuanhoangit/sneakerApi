using System;

namespace SneakerAPI.Core.Libraries;
    public class HandleString
{   
    public static string GenerateVerifyCode(int length=6)
    {
        Random random = new Random();
        string numberString = random.Next(100000, 999999).ToString();
        return numberString;
    }
    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}