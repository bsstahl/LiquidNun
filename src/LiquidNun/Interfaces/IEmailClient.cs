using System;

namespace LiquidNun.Interfaces;

public interface IEmailClient
{
    void SendEmail(string subject, string messageBody, string sendFromName, string sendFromAddress, string sendToAddresses, string sendCCAddresses, string sendBccAddresses);
}