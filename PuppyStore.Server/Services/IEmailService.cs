using System.Threading.Tasks;
using PuppyStore.Shared.Models;

namespace PuppyStore.Server.Services
{
    public interface IEmailService
    {
        Task SendContactAsync(ContactForm form);
        Task SendOrderConfirmationAsync(string toEmail, string body);
    }

}
