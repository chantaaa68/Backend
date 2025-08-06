using Backend.Model;
using Backend.Repository;
using MailKit.Security;
using MimeKit;
using Org.BouncyCastle.Crypto.Macs;
using WebApplication.Repository;
using static Backend.Dto.NewsletterDto;

namespace Backend.service
{
    public class NewsletterService
    {
        private readonly NewsletterRepository newsletterRepository;

        private readonly UserDataRepository userDataRepository;

        public NewsletterService(NewsletterRepository newsletterRepository, UserDataRepository userDataRepository)
        {
            this.newsletterRepository = newsletterRepository;
            this.userDataRepository = userDataRepository;
        }

        public void NewsletterResist(NewsletterRigistRequest request)
        {
            var newsletter = new NewsletterTemplate
            {
                MailTitle = request.Title,
                MailBody = request.Content,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                DeleteDate = null // 初期値はnull
            };
            this.newsletterRepository.NewsletterResist(newsletter);
        }

        public NewsletterSendResponse NewsletterSend(NewsletterSendRequest request)
        {

            var newsletter = this.newsletterRepository.GetNewsletterById(request.NewsletterId);

            var user = this.userDataRepository.GetUserById(request.UserId);

            int sendCount = 1;

            // ここでメール送信処理を実装する
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("chan.taaaTest", "noreply.tateyama@gmail.com")); // 送信者のメールアドレスを設定
            message.To.Add(new MailboxAddress(user.Name, user.Email)); // 受信者のメールアドレスを設定
            message.Subject = newsletter.MailTitle; // メールのタイトルを設定
            message.Body = new TextPart("plain")
            {
                Text = newsletter.MailBody // メールの本文を設定
            };
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    Console.WriteLine("メール送信 start");
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("noreply.tateyama@gmail.com", "vhqbuojsonlfgcxy"); 
                    client.Send(message);
                    client.Disconnect(true);
                    Console.WriteLine("メール送信 end");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"メール送信に失敗しました: {ex.Message}");
                throw;
            }


            var response = new NewsletterSendResponse
            {
                sendCount = sendCount
            };
            return response;
        }
    }
}
