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
}