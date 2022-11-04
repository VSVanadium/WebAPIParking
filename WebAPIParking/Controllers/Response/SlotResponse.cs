using System.Net;

namespace WebAPIParking.Controllers.Response
{
    public class SlotResponse
    {
       HttpStatusCode HttpCode  { get; set; }
       string Message { get; set; }

        public SlotResponse(string message, HttpStatusCode code)
        {
            Message = message;
            HttpCode = code;
        }

    }
}
