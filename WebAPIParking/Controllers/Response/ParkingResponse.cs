﻿using System.Net;
using WebAPIParking.Models;

namespace WebAPIParking.Controllers.Response
{
    public class ParkingResponse
    {

        public ParkingModel? Parking { get; set; }
        public string? Message   { get; set; }
        public HttpStatusCode HttpCode { get; set; }

        public ParkingResponse(ParkingModel parking, string message, HttpStatusCode code)
        {
            this.Parking = parking; 
            this.Message = message;
            this.HttpCode = code;
        }
    }
}
