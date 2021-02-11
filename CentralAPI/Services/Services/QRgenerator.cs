using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
   
    public class QRgenerator
    {
        private readonly IParkingLotService _parkingLotService;

        public QRgenerator(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }
        public async Task<ActionResult<byte[]>> MakeQR(CentralReservationDTO centralResevationDTO)
        {

            var ParkingLot = await _parkingLotService.GetParkingLot(centralResevationDTO.parkingLotID);
            var qrText = ("ParkingSpot Reservation: \n ReservationID: " + centralResevationDTO.reservationID 
                +"\n\n ParkingLot: " + ParkingLot.Value.name
                +"\n Location: " + ParkingLot.Value.location
                + "\n\n ParkingSpotID: " + centralResevationDTO.parkingSpotID 
                + "\n StartTime: " +centralResevationDTO.startTime 
                +"\n EndTime: " + centralResevationDTO.endTime 
                + "\n Duration: " + centralResevationDTO.hours
                + "\n Price: " + centralResevationDTO.finalPrice);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);


            return BitmapToBytes(qrCodeImage);
        }


        private static Byte[] BitmapToBytes(Bitmap img)
        {

            using (MemoryStream stream = new MemoryStream())
            {

                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();

            }
        }
    }
}
