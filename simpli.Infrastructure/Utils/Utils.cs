using System.Security.Cryptography;
using QRCoder;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using System.Drawing;

public static class Utils
{
  public static async Task SendVisitorEmailAsync(string email, string firstName, string roomNumber, string passCode)
  {
    // 1. Generate the QR Code with  specific colors
    using var qrGenerator = new QRCodeGenerator();
    var qrCodeData = qrGenerator.CreateQrCode(passCode, QRCodeGenerator.ECCLevel.Q);

    // We use the 'QRCode' class for color support
    using var qrCode = new QRCode(qrCodeData);

    // Convert your hex colors to System.Drawing.Color
    Color darkColor = ColorTranslator.FromHtml("#00ED64");
    Color lightColor = ColorTranslator.FromHtml("#001E2B");

    // Generate as a Bitmap (standard free way)
    using Bitmap qrImage = qrCode.GetGraphic(20, darkColor, lightColor, true);

    // Convert Bitmap to Byte Array (to send as attachment)
    using var ms = new MemoryStream();
    qrImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
    byte[] qrCodeBytes = ms.ToArray();

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

    // 3. Send via Free SMTP (e.g., Gmail or Outlook)
    using var client = new SmtpClient();
    // For Gmail: use smtp.gmail.com | Port 587 | StartTls
    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
    await client.AuthenticateAsync("your-email@gmail.com", "your-app-password");
    await client.SendAsync(message);
    await client.DisconnectAsync(true);
  }

  public static string GeneratePasscode()
  {
    int passcode = RandomNumberGenerator.GetInt32(10000, 100000);
    return passcode.ToString();
  }
}