namespace Services.IService;

public interface IEmailService
{
    public Task<bool> SendEmailAsync(IEnumerable<string> toList, string subject, string body);
    public Task SendEmailWithQrCodeAsync(string email, string subject, string body, string qrCodeContent);


}