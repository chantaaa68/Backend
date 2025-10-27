using Backend.Annotation;
using Backend.Dto.service.newsletter;
using Backend.Model;
using Backend.Repository;
using Backend.Utility;
using Backend.Utility.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Backend.service
{
    [AutoDI]
    public class NewsletterService(
        NewsletterRepository _newsletterRepository,
        SendMail _sendMail)
    {
        private readonly NewsletterRepository newsletterRepository = _newsletterRepository;

        private readonly SendMail sendMail = _sendMail;

        /// <summary>
        /// メールテンプレートの登録
        /// </summary>
        /// <param name="req"></param>
        public async Task<IActionResult> ResistNewsletterAsync(RigistNewsletterRequest req)
        {
            var newsletter = new NewsletterTemplate
            {
                MailTitle = req.Title,
                MailBody = req.MailBody,
            };

            RigistNewsletterResponse response = new()
            {
                Count = await this.newsletterRepository.ResistNewsletterTemplateAsync(newsletter)
            };

            return ApiResponseHelper.Success(response);
        }

        /// <summary>
        /// メールテンプレートの更新
        /// </summary>
        /// <param name="req"></param>
        public async Task<IActionResult> UpdateNewsletterAsync(UpdateNewsletterRequest req)
        {
            NewsletterTemplate? template = await this.newsletterRepository.GetNewsletterById(req.Id);

            if (template == null)
            {
                return ApiResponseHelper.Fail("テンプレートが存在しません。");
            }
            else
            {
                template.MailTitle = req.Title;
                template.MailBody = req.MailBody;

                UpdateNewsletterResponse response = new()
                {
                    Count = await this.newsletterRepository.ResistNewsletterTemplateAsync(template)
                };

                return ApiResponseHelper.Success(response);
            }
        }

        /// <summary>
        /// メール送信
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<IActionResult> SendNewsletterAsync(SendNewsletterRequest req)
        {
            int sendCount = await this.sendMail.SendNewsletterAsync(new SendMailParameter()
            {
                NewsletterId = req.NewsletterId,
                ItemId = req.ItemId,
                UserId = req.UserId
            });

            SendNewsletterResponse response =  new()
            {
                SendCount = sendCount
            };

            return ApiResponseHelper.Success(response);
        }
    }
}
