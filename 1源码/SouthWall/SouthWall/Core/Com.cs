using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Memory;
using MimeKit;

namespace SouthWall
{
    public class Com
    {

        private static MemoryCache _MemoryCache = new MemoryCache(new MemoryCacheOptions());
        public static readonly Random _Random = new Random();

        private const string _CacheKey_Login_Token = "_CacheKey_Login_Token";
        private const string _CacheKey_Login_Code = "_CacheKey_Login_Code";
        public static string GenLoginCode()
        {
            return _Random.Next(100000, 999999).ToString();
        }
        public static void SetLoginCode(string code)
        {
            _MemoryCache.Set( _CacheKey_Login_Code, code, TimeSpan.FromMinutes(5));
        }
        public static void RemoveLoginCode()
        {
            _MemoryCache.Remove( _CacheKey_Login_Code);
        }
        public static string GetLoginCode()
        {
            return _MemoryCache.Get<string>(_CacheKey_Login_Code);
        }
        public static void SetLoginToken(string token)
        {
            _MemoryCache.Set(_CacheKey_Login_Token, token, TimeSpan.FromMinutes(5));
        }
        public static void RemoveLoginToken()
        {
            _MemoryCache.Remove(_CacheKey_Login_Token);
        }
        public static string GetLoginToken()
        {
            return _MemoryCache.Get<string>(_CacheKey_Login_Token);
        }
       
        public static string GenPageId()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
        }
        public static Task SendCodeToEmailForLogin(string code,string toEmail) 
        {
           return SendEmail(
                     Config.ServiceMail,
                     Config.ServiceMailName,
                     Config.ServiceMailPassword,
                     Config.ServiceMailSMTP,
                     Config.ServiceMailPort,
                     Config.ServiceMailSSL,
                     toEmail, "亲爱的用户", $"【{Config.ServiceMailName}】登录验证码", $"验证码：{code}，5分钟内有效。");
        }
        public static async Task SendEmail(string fromMail, string fromMailName, string fromMailPassword, string smtp, int port, bool ssl, string toMail, string toMailName, string subject, string body, bool isHtmlBody = false)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromMailName, fromMail));
            message.To.Add(new MailboxAddress(toMailName, toMail));
            message.Subject = subject; //邮件标题
            var builder = new BodyBuilder
            {

                //  TextBody=body,
                // HtmlBody = body//正文
            };
            if (isHtmlBody)
            {
                builder.HtmlBody = body;
            }
            else
            {
                builder.TextBody = body;
            }
            message.Body = builder.ToMessageBody();
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                var mSendMail = fromMail;
                var mSendPwd = fromMailPassword;//不是邮箱密码
                client.Connect(smtp, port, ssl);//网易、QQ支持 25(未加密)，465和587(SSL加密）
                client.Authenticate(mSendMail, mSendPwd);
                try
                {
                    await client.SendAsync(message);//发送邮件                    
                    await client.DisconnectAsync(true);
                }
                catch (SmtpCommandException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
