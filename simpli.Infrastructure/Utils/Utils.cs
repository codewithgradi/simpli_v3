using System.Security.Cryptography;
using QRCoder;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using Microsoft.Extensions.Options;

public interface IEmailService
{
  Task SendVisitorEmailAsync(string email, string firstName, string roomNumber, string passCode);
}
public class EmailService : IEmailService
{
  private readonly OtherSettings _setings;

  public EmailService(IOptions<OtherSettings> settings)
  {
    _setings = settings.Value;

    if (_setings == null)
    {
      throw new ArgumentNullException("Entire other setting is empty");
    }
    if (string.IsNullOrEmpty(_setings.SystemEmail))
    {
      throw new InvalidOperationException("OtherSettings:SystemEmail is null or empty! Check your environment variables.");
    }

    if (string.IsNullOrEmpty(_setings.AppPassword))
    {
      throw new InvalidOperationException("OtherSettings:AppPassword is null or empty! Check your environment variables.");
    }
  }
  public async Task SendVisitorEmailAsync(string email, string firstName, string roomNumber, string passCode)
  {
    // 1. Generate the QR Code with specific colors (Cross-Platform)
    using var qrGenerator = new QRCodeGenerator();
    using var qrCodeData = qrGenerator.CreateQrCode(passCode, QRCodeGenerator.ECCLevel.Q);

    // PngByteQRCode works everywhere (Linux, Mac, Windows, Docker)
    using var qrCode = new PngByteQRCode(qrCodeData);

    byte[] darkColor = new byte[] { 0x00, 0xED, 0x64 };
    byte[] lightColor = new byte[] { 0x00, 0x1E, 0x2B };

    byte[] qrCodeBytes = qrCode.GetGraphic(20, darkColor, lightColor, true);

    // 2. Build the Email
    var message = new MimeMessage();
    message.From.Add(new MailboxAddress("Check-in System", "your-email@gmail.com"));
    message.To.Add(new MailboxAddress(firstName, email));
    message.Subject = $"Your Exit Pass - {firstName}";

    var bodyBuilder = new BodyBuilder();

    // Attach the QR code and link it to the HTML via Content ID (CID)
    var image = bodyBuilder.Attachments.Add("exit-pass.png", qrCodeBytes);
    image.ContentId = MimeUtils.GenerateMessageId();

    bodyBuilder.HtmlBody = $@"
            <div style=""background-color: #001E2B; color: #ffffff; padding: 40px; font-family: sans-serif; text-align: center; border-radius: 15px;"">
                <h1 style=""color: #00ED64; margin-bottom: 5px;"">Check-in Successful</h1>
                <p style=""color: #8899A6; margin-bottom: 30px;"">Welcome to Room {roomNumber}, {firstName}.</p>
                <div style=""background: #ffffff; padding: 20px; display: inline-block; border-radius: 12px; margin-bottom: 20px;"">
                    <img src=""cid:{image.ContentId}"" width=""200"" height=""200"" />
                </div>
                <p style=""font-size: 14px; color: #E8EDF0;"">Passcode: <br/> 
                   <span style=""font-family: monospace; font-size: 18px; color: #00ED64;"">{passCode}</span>
                </p>
            </div>";

    message.Body = bodyBuilder.ToMessageBody();

    // 3. Send via Free SMTP
    using var client = new SmtpClient();
    await client.ConnectAsync(
      "smtp.gmail.com",
      587,
      MailKit.Security.SecureSocketOptions
      .StartTlsWhenAvailable);
    await client.AuthenticateAsync(_setings.SystemEmail!, _setings.AppPassword!);
    await client.SendAsync(message);
    await client.DisconnectAsync(true);
  }
}

public static class Utils
{
  public static string GeneratePasscode()
  {
    int passcode = RandomNumberGenerator.GetInt32(10000, 100000);
    return passcode.ToString();
  }
}