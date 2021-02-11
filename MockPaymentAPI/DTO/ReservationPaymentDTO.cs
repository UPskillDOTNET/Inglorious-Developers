namespace MockPaymentAPI.Controllers
{
    public class ReservationPaymentDTO
    {
        public string reservationID { get; set; }

        public decimal value { get; set; }

        public string userID { get; set; }
    }
}