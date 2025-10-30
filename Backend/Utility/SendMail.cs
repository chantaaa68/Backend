using Backend.Annotation;
using Backend.Model;
using Backend.Repository;
using Backend.Utility.Dto;
using MailKit.Security;
using MimeKit;
using WebApplication.Repository;

namespace Backend.Utility
{
    [AutoDI]
    public class SendMail(
        NewsletterRepository _newsletterRepository,
        UserDataRepository _userDataRepsitory, 
        KakeiboRepository _kakeiboRepository
        )
    {
        private NewsletterRepository newsletterRepository = _newsletterRepository;

        private UserDataRepository userDataRepository = _userDataRepsitory;

        private KakeiboRepository kakeiboRepository = _kakeiboRepository;
        public async Task<int> SendNewsletterAsync(SendMailParameter prm)
        {
            NewsletterTemplate? newsletter = await this.newsletterRepository.GetNewsletterById(prm.NewsletterId);

            if (newsletter == null)
            {
                throw new Exception("そんざいしません。");
            }

            // ユーザー情報を取得する
            var user = this.userDataRepository.GetUserById(prm.UserId);

            // アイテム情報を取得する(実装予定）
            KakeiboItem? item = await this.kakeiboRepository.GetKakeiboItemAsync(prm.ItemId);

            if (item == null)
            {
                throw new Exception("アイテムが存在しません。");
            }

            int sendCount = 1;

            // テンプレートからリプレースする
            // TODO: メールのテンプレートは共通処理として切り出す
            // ここから
            string mailBody = Replace.TemplateExchange(new TemplateExchangeParameter
            {
                baseMailBody = newsletter.MailBody,
                userName = user.Name,
                itemName = item.ItemName!,
                Price = item.ItemAmount.ToString()
            });

            // ここでメール送信処理を実装する
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("chan.taaaTest", "noreply.tateyama@gmail.com")); // 送信者のメールアドレスを設定
            message.To.Add(new MailboxAddress(user.Name, user.Email)); // 受信者のメールアドレスを設定
            message.Subject = newsletter.MailTitle; // メールのタイトルを設定
            message.Body = new TextPart("plain")
            {
                Text = mailBody // メールの本文を設定
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

            return sendCount;
        }
    }
}
