using System.Security.Cryptography;
using QRCoder;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;


public class EmailService : IEmailService
{
  private readonly OtherSettings _setings;
  private readonly ILogger<EmailService> _logger;
  private readonly HttpClient _httpClient;

  public EmailService(IOptions<OtherSettings> settings, ILogger<EmailService> logger, HttpClient? httpClient)
  {
    _setings = settings.Value;
    _logger = logger;
    _httpClient = httpClient;

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
  public async Task SendVisitorEmailAsync(string email, string firstName, string roomNumber, string data)
  {
    // 1. Generate the QR Code with specific colors 
    using var qrGenerator = new QRCodeGenerator();
    using var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);

    // PngByteQRCode works everywhere 
    using var qrCode = new PngByteQRCode(qrCodeData);

    byte[] darkColor = new byte[] { 0x00, 0xED, 0x64 };
    byte[] lightColor = new byte[] { 0x00, 0x1E, 0x2B };

    byte[] qrCodeBytes = qrCode.GetGraphic(20, darkColor, lightColor, true);
    // Prepare the payload for Brevo's API
    var emailPayload = new
    {
      sender = new { name = "Check-in System", email = _setings.SystemEmail }, // Watch out for 'setings' vs 'settings' spelling
      to = new[] { new { email = email, name = firstName } },
      subject = $"Your Exit Pass - {firstName}",
      htmlContent = $@"
        <div style='background-color: #001E2B; color: #ffffff; padding: 40px; text-align: center;'>
            <h1>Check-in Successful</h1>
            <p>Welcome to Room {roomNumber}, {firstName}.</p>
            <img src='data:image/png;base64,{qrCodeBytes}' width='200' height='200' />
            <p>Passcode: {Utils.GetPassCodeUtility(data)}</p>
        </div>"
    };

    // Create an explicit HttpRequestMessage wrapper
    var request = new HttpRequestMessage(HttpMethod.Post, _setings.BrevoLink);
    request.Content = JsonContent.Create(emailPayload);

    // Force the header directly onto this specific request transaction
    request.Headers.Add("api-key", _setings.ApiKey!.Trim());

    var response = await _httpClient.SendAsync(request);
  }
}

public static class Utils
{
  public static string GetPassCodeUtility(string data)
  {
    int positionOfPeriod = data.IndexOf('.');
    return data.Substring(0, positionOfPeriod - 1);
  }
  public static string GetRoomIdUtility(string data)
  {
    int positionOfPeriod = data.IndexOf('.');
    return data.Substring(positionOfPeriod);
  }
  public static string GeneratePasscode(int roomId)
  {
    int passcode = RandomNumberGenerator.GetInt32(10000, 100000);
    string result = $"{passcode.ToString()}.{roomId.ToString()}";
    return result;
  }
}