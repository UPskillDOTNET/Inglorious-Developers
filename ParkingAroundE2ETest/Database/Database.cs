using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAroundE2ETest
{
    public static class Database
    {
        private static SqlConnection sqlConnPark = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=Private-Park-Database;Trusted_Connection=True;MultipleActiveResultSets=true");
        private static SqlConnection sqlConnCentral = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=Central-Park-Database;Trusted_Connection=True;MultipleActiveResultSets=true");

        public static void DeleteReservation()
        {
            SqlCommand search = new SqlCommand(
                @"SELECT centralreservationID FROM CentralReservations r WHERE r.parkingSpotID = @parkingSpotID AND r.parkingLotID = @parkingLotID AND r.userID = @userID", sqlConnCentral);
            search.Parameters.Add(new SqlParameter("@parkingSpotID", "O2"));
            search.Parameters.Add(new SqlParameter("@parkingLotID", 1));
            search.Parameters.Add(new SqlParameter("@userID", 3));
            sqlConnCentral.Open();
            string resID = (string)search.ExecuteScalar();
            if (resID == null)
            {
                sqlConnCentral.Close();
                return;
            }
            SqlCommand cmdCentral = new SqlCommand(
                @"DELETE FROM CentralReservations WHERE centralreservationID = @centralreservationID", sqlConnCentral);
            cmdCentral.Parameters.Add(new SqlParameter("@centralreservationID", resID));            
            cmdCentral.ExecuteNonQuery();
            sqlConnCentral.Close();

            SqlCommand cmdPark = new SqlCommand(
                @"DELETE FROM Reservations WHERE reservationID = @reservationID", sqlConnPark);
            cmdPark.Parameters.Add(new SqlParameter("@reservationID", resID));
            sqlConnPark.Open();
            cmdPark.ExecuteNonQuery();
            sqlConnPark.Close();
        }

        public static void ResetWallet()
        {
            SqlCommand cmdCentral = new SqlCommand(
                @"UPDATE Wallets SET totalAmount = @totalAmount WHERE userID = @userID", sqlConnCentral);
            cmdCentral.Parameters.Add(new SqlParameter("@totalAmount", 25.00));
            cmdCentral.Parameters.Add(new SqlParameter("@userID", 3));
            sqlConnCentral.Open();
            cmdCentral.ExecuteNonQuery();
            sqlConnCentral.Close();

        }
    }
}
