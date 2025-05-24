using NotificationService.DTOs;
using NotificationService.Interfaces;

namespace NotificationService.Services
{
    public class JobNotificationService
    {
        private readonly IEmailSender _emailSender;

        public JobNotificationService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task HandleNotificationAsync(NotificationDto dto)
        {
            string email = $"{dto.Selger.ToLower()}@grasdal.no";
            string subject = $"Statusoppdatering: {dto.Status} – Ref. {dto.RefNo}";
            string body = "";

            switch (dto.Status.ToLower())
            {
                case "levert":
                    body = $"Ref.no: {dto.RefNo}\nPlassering: {dto.Plassering}\nDato levert: {dto.DatoLevert?.ToShortDateString()}\n\n{dto.CustomText}";
                    break;

                case "hentes":
                    body = $"Ref.no: {dto.RefNo}\nPlassering: {dto.Plassering}\n\n{dto.CustomText}";
                    break;

                case "hentet":
                    body = $"Varen med Ref.no {dto.RefNo} er nå hentet av kunde.\n\n{dto.CustomText}";
                    break;

                case "montering":
                    body = $"Ref.no: {dto.RefNo}\nAdresse: {dto.Adresse}\nDato for montering: {dto.MonteringDato?.ToShortDateString()}\n\n{dto.CustomText}";
                    break;

                case "slett":
                    body = $"Oppdrag for Ref.no {dto.RefNo} er montert.\n\n{dto.CustomText}";
                    break;

                case "retur":
                    body = $"Ref.no: {dto.RefNo}\nGrunn for retur: {dto.GrunnAvRetur}\n\n{dto.CustomText}";
                    break;

                default:
                    body = $"Status for bestilling {dto.RefNo} er oppdatert til: {dto.Status}\n\n{dto.CustomText}";
                    break;
            }

            await _emailSender.SendEmailAsync(email, subject, body);
        }
    }
}
