using Forward.Core;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Forward.ExtensionService
{
    public class MaillService : AbstractPublishService
    {
        private string userName = "2598145226@qq.com";
        private string pawssword = "dobjwhiwmxmfdhih";
        private string _host = "smtp.qq.com";

        [ParamType(typeof(MaillModel))]
        public override async Task<object> ExecuteAsync(object param)
        {
            MaillModel maillModel = param as MaillModel;

            using (SmtpClient smtpClient = new SmtpClient(_host, 587))
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
                smtpClient.Host = _host;//指定SMTP服务器
                smtpClient.Credentials = new NetworkCredential(userName, pawssword);//用户名和密码
                smtpClient.EnableSsl = true;

                MailAddress fromAddress = new MailAddress(userName, "华灯");
                MailAddress toAddress = new MailAddress(maillModel.ToMail);
                MailMessage mailMessage = new MailMessage(fromAddress, toAddress);

                mailMessage.Subject = maillModel.Subject;//主题
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Body = maillModel.Body;//内容
                mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
                mailMessage.IsBodyHtml = true;//设置为HTML格式
                mailMessage.Priority = MailPriority.Normal;//优先级

                await smtpClient.SendMailAsync(mailMessage);
            }

            return true;
        }
    }
}
