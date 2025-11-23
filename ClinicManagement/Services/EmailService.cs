using System.Net;
using System.Net.Mail;

namespace ClinicManagement.Services
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string toEmail, string resetLink);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string resetLink)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"] ?? "localhost";
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "25");
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var senderPassword = _configuration["EmailSettings:SenderPassword"];
            var fromEmail = _configuration["EmailSettings:FromEmail"] ?? "clinicsupport@clinic.local";
            var senderName = _configuration["EmailSettings:SenderName"] ?? "Clinic Support";
            var requireAuth = _configuration.GetValue<bool>("EmailSettings:RequireAuthentication", false);

            var emailBody = $@"
Hello,

You have requested to reset your password for your Clinic Management account.

Please click the following link to reset your password:
{resetLink}

This link will expire in 1 hour.

If you did not request this password reset, please ignore this email.

Best regards,
Clinic Support Team
";

            // Try to send via SMTP if configured
            if (!string.IsNullOrEmpty(smtpServer) && smtpServer != "localhost")
            {
                try
                {
                    using var client = new SmtpClient(smtpServer, smtpPort);

                    if (requireAuth)
                    {
                        if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword))
                        {
                            throw new InvalidOperationException("Email authentication is required but credentials are missing");
                        }

                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    }
                    else
                    {
                        client.EnableSsl = false;
                    }

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail, senderName),
                        Subject = "Password Reset Request",
                        Body = emailBody,
                        IsBodyHtml = false
                    };

                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                    _logger.LogInformation($"Password reset email sent successfully from {fromEmail} to {toEmail}");
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, $"Failed to send email via SMTP, falling back to logging");
                }
            }

            // Fallback: Log the email (works without any SMTP server)
            _logger.LogInformation("=== Password Reset Email ===");
            _logger.LogInformation($"From: {fromEmail} ({senderName})");
            _logger.LogInformation($"To: {toEmail}");
            _logger.LogInformation($"Subject: Password Reset Request");
            _logger.LogInformation($"Reset Link: {resetLink}");
            _logger.LogInformation("=== Email would be sent in production ===");
            
            await Task.CompletedTask;
        }
    }
}
