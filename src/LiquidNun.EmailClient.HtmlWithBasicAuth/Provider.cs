using LiquidNun.EmailClient.HtmlWithBasicAuth.Extensions;
using MailKit.Security;
using MimeKit;

namespace LiquidNun.EmailClient.HtmlWithBasicAuth;

public class Provider: Interfaces.IEmailClient
{
    const int _defaultServerPort = 587;

    private readonly string _serverUserName;
    private readonly string _serverPassword;

    private readonly string _serverName;
    private readonly int _serverPort;

    public Provider(string serverUserName, string serverPassword, string serverName)
        : this(serverUserName, serverPassword, serverName, _defaultServerPort)
    { }

    public Provider(string serverUserName, string serverPassword, string serverName, int serverPort)
    {
        _serverUserName = serverUserName;
        _serverPassword = serverPassword;
        _serverName = serverName;
        _serverPort = serverPort;
    }

    public void SendEmail(string subject, string messageBody, string sendFromName, string sendFromAddress, string sendToAddresses, string sendCCAddresses, string sendBccAddresses)
    {
        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress(sendFromName, sendFromAddress));

        foreach (var address in sendToAddresses.ParseAddresses())
            msg.To.Add(new MimeKit.MailboxAddress(address, address));

        foreach (var address in sendCCAddresses.ParseAddresses())
            msg.Cc.Add(new MimeKit.MailboxAddress(address, address));

        foreach (var address in sendBccAddresses.ParseAddresses())
            msg.Bcc.Add(new MimeKit.MailboxAddress(address, address));

        msg.Subject = subject;
        msg.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = messageBody };

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect(_serverName, _serverPort, SecureSocketOptions.StartTls);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_serverUserName, _serverPassword);

            client.Send(msg);
        }
    }
}
