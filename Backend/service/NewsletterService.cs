using Backend.Annotation;
using Backend.Dto.service.newsletter;
using Backend.Model;
using Backend.Repository;
using Backend.Utility;
using Backend.Utility.Dto;
using MailKit.Security;
using MimeKit;
using WebApplication.Repository;

namespace Backend.service
{
    [Component]
    public class NewsletterService(
        NewsletterRepository _newsletterRepository,
        UserDataRepository _userDataRepository,
        KakeiboRepository _kakeiboRepository)
    {
        private readonly NewsletterRepository newsletterRepository = _newsletterRepository;

        private readonly UserDataRepository userDataRepository = _userDataRepository;

        private readonly KakeiboRepository kakeiboRepository = _kakeiboRepository;

        /// <summary>
        /// メールテンプレートの登録
        /// </summary>
        /// <param name="req"></param>
        public async Task<RigistNewsletterResponse> ResistNewsletterAsync(RigistNewsletterRequest req)
        {
            var newsletter = new NewsletterTemplate
            {
                MailTitle = req.Title,
                MailBody = req.MailBody,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            RigistNewsletterResponse response = new()
            {
                Count = await this.newsletterRepository.ResistNewsletterTemplateAsync(newsletter)
            };

            return response;
        }

        /// <summary>
        /// メールテンプレートの更新
        /// </summary>
        /// <param name="req"></param>
        public async Task<UpdateNewsletterResponse> UpdateNewsletterAsync(UpdateNewsletterRequest req)
        {
            NewsletterTemplate? template = await this.newsletterRepository.GetNewsletterById(req.Id);

            if (template == null)
            {
                throw new Exception("テンプレートが存在しません");
            }
            else
            {
                template.MailTitle = req.Title;
                template.MailBody = req.MailBody;
                template.UpdateDate = DateTime.UtcNow;

                UpdateNewsletterResponse response = new()
                {
                    Count = await this.newsletterRepository.ResistNewsletterTemplateAsync(template)
                };

                return response;
            }
        }

        /// <summary>
        /// メール送信
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<SendNewsletterResponse> SendNewsletterAsync(SendNewsletterRequest req)
        {

            NewsletterTemplate? newsletter = await this.newsletterRepository.GetNewsletterById(req.NewsletterId);

            if(newsletter == null)
            {
                throw new Exception("そんざいしません。");
            }

            // ユーザー情報を取得する
            var user = this.userDataRepository.GetUserById(req.UserId);

            // アイテム情報を取得する(実装予定）
            KakeiboItem? item = await this.kakeiboRepository.GetKakeiboItemAsync(req.ItemId);

            if(item == null)
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


            var response = new SendNewsletterResponse
            {
                SendCount = sendCount
            };
            return response;
        }

    }
}
