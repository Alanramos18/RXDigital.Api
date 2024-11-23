using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MimeKit;
using RXDigital.Api.Entities;
using RXDigital.Api.Services;

namespace RXDigital.Api.Helpers
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _emailSettings;
        private readonly string sFilePath;

        public EmailService(IOptions<MailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;

            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = Path.Combine(sCurrentDirectory, @"..\..\..\Assets");
            sFilePath = Path.GetFullPath(sFile);
        }

        /// <inheritdoc/>
        public async Task SendVerificationAsync(string to, string link, string name, CancellationToken cancellationToken, string from = null)
        {
            var email = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            using (StreamReader SourceReader = File.OpenText($"{sFilePath}\\verify-email.html"))
            {
                var html = await SourceReader.ReadToEndAsync();
                bodyBuilder.HtmlBody = html.Replace("{NAME}", name).Replace("{LINK}", link);
            }

            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            //email.Subject = Constants.VerificationSubject;
            email.Body = bodyBuilder.ToMessageBody();

            await SendEmailAsync(email, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task SendRejectEmailAsync(string to, string name, CancellationToken cancellationToken, string from = null)
        {
            var email = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            using (StreamReader SourceReader = File.OpenText($"{sFilePath}\\mail-rechazo.html"))
            {
                var html = await SourceReader.ReadToEndAsync();
                bodyBuilder.HtmlBody = html.Replace("{NAME}", name);
            }

            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Respuesta de solicitud a Rp Digital";
            email.Body = bodyBuilder.ToMessageBody();

            await SendEmailAsync(email, cancellationToken);
        }

        public async Task SendApprovedEmailAsync(string to, string name, CancellationToken cancellationToken, string from = null)
        {
            var email = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            using (StreamReader SourceReader = File.OpenText($"{sFilePath}\\mail-aprobado.html"))
            {
                var html = await SourceReader.ReadToEndAsync();
                bodyBuilder.HtmlBody = html.Replace("{NAME}", name);
            }

            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Respuesta de solicitud a Rp Digital";
            email.Body = bodyBuilder.ToMessageBody();

            await SendEmailAsync(email, cancellationToken);
        }

        public async Task SendRxEmailAsync(string to, AllInfo allInfo, CancellationToken cancellationToken, string from = null)
        {
            var email = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            using (StreamReader SourceReader = File.OpenText($"{sFilePath}\\mail-emitido.html"))
            {
                var html = await SourceReader.ReadToEndAsync();
                bodyBuilder.HtmlBody = html
                    .Replace("{NAME}", allInfo.Patient.Nombre)
                    .Replace("{DOC}", $"{allInfo.AccountEntity.LastName}, {allInfo.AccountEntity.FirstName}")
                    .Replace("{rxCode}", allInfo.Prescription.PrescriptionCode)
                    .Replace("{fechaEmision}", allInfo.Prescription.CreatedDate.ToString())
                    .Replace("{expiracion}", allInfo.Prescription.Expiration.ToString())
                    .Replace("{nombrePaciente}", $"{allInfo.Patient.Apellido}, {allInfo.Patient.Nombre}")
                    .Replace("{dni}", allInfo.Patient.Dni.ToString())
                    .Replace("{obraSocial}", allInfo.Patient.SocialWork.Name)
                    .Replace("{planSocial}", allInfo.Patient.SocialWork.SocialPlan)
                    .Replace("{numeroAfiliado}", allInfo.Patient.NumeroAfiliado)
                    .Replace("{nombreMedico}", $"{allInfo.AccountEntity.LastName}, {allInfo.AccountEntity.FirstName}")
                    .Replace("{especialidad}", allInfo.Doctor.Especialidad.Descripcion)
                    .Replace("{matricula}", allInfo.Prescription.RegistrationId.ToString())
                    .Replace("{diagnostico}", allInfo.Prescription.Diagnostic)
                    .Replace("{indicaciones}", allInfo.Prescription.Indications)
                    .Replace("{TABLAIMPORTANTE}", GenerateMedicinesTableHtml(allInfo.Medicines));
            }

            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Respuesta de solicitud a Rp Digital";
            email.Body = bodyBuilder.ToMessageBody();

            await SendEmailAsync(email, cancellationToken);
        }

        public async Task SendUpdatedRxEmailAsync(string to, AllInfo allInfo, CancellationToken cancellationToken, string from = null)
        {
            var email = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            using (StreamReader SourceReader = File.OpenText($"{sFilePath}\\mail-emitido.html"))
            {
                var html = await SourceReader.ReadToEndAsync();
                bodyBuilder.HtmlBody = html
                    .Replace("{NAME}", allInfo.Patient.Nombre)
                    .Replace("{DOC}", $"{allInfo.AccountEntity.LastName}, {allInfo.AccountEntity.FirstName}")
                    .Replace("{rxCode}", allInfo.Prescription.PrescriptionCode)
                    .Replace("{fechaEmision}", allInfo.Prescription.CreatedDate.ToString())
                    .Replace("{expiracion}", allInfo.Prescription.Expiration.ToString())
                    .Replace("{nombrePaciente}", $"{allInfo.Patient.Apellido}, {allInfo.Patient.Nombre}")
                    .Replace("{dni}", allInfo.Patient.Dni.ToString())
                    .Replace("{obraSocial}", allInfo.Patient.SocialWork.Name)
                    .Replace("{planSocial}", allInfo.Patient.SocialWork.SocialPlan)
                    .Replace("{numeroAfiliado}", allInfo.Patient.NumeroAfiliado)
                    .Replace("{nombreMedico}", $"{allInfo.AccountEntity.LastName}, {allInfo.AccountEntity.FirstName}")
                    .Replace("{especialidad}", allInfo.Doctor.Especialidad.Descripcion)
                    .Replace("{matricula}", allInfo.Prescription.RegistrationId.ToString())
                    .Replace("{diagnostico}", allInfo.Prescription.Diagnostic)
                    .Replace("{indicaciones}", allInfo.Prescription.Indications)
                    .Replace("{TABLAIMPORTANTE}", GenerateMedicinesTableHtml(allInfo.Medicines));
            }

            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Respuesta de solicitud a Rp Digital";
            email.Body = bodyBuilder.ToMessageBody();

            await SendEmailAsync(email, cancellationToken);
        }

        private string GenerateMedicinesTableHtml(List<MedsDto> medicines)
        {
            var sb = new StringBuilder();

            foreach (var med in medicines)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{med.CommercialName}</td>");
                sb.AppendLine($"<td>{med.Presentation}</td>");
                sb.AppendLine($"<td>{med.Concentration}</td>");
                sb.AppendLine($"<td>{med.Indications}</td>");
                sb.AppendLine("</tr>");
            }

            return sb.ToString();
        }


        private async Task SendEmailAsync(MimeMessage email, CancellationToken cancellationToken)
        {
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.CheckCertificateRevocation = false;
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.UserName, _emailSettings.Password);
            await smtp.SendAsync(email, cancellationToken);
            smtp.Disconnect(true);
        }
    }
}
