using System.Security.Cryptography;

public static class Utils
{
  public static string GeneratePasscode()
  {
    int passcode = RandomNumberGenerator.GetInt32(10000, 100000);
    return passcode.ToString();
  }
  public static void SendEmail() { }
}