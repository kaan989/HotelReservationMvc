namespace HotelReservationMvc.EmailSenderProcesess
{
    public interface IEmailManager
    {
        bool SendEmail(EmailMessageModel model);
        Task SendMailAsync(EmailMessageModel model);


        bool SendEmailviaGmail(EmailMessageModel model);
    }
}
