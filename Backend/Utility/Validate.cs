namespace Backend.Utility
{
    public class Validate
    {
        public static Boolean CheckEmail(string email)
        {
            // 正規表現を使用してメールアドレスの形式を検証
            var emailRegex = new System.Text.RegularExpressions.Regex(
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }
    }
}
