using Cypher.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace Cypher.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}