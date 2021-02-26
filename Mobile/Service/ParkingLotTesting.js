import React, { useEffect } from "react";
import axios from 'axios';
import CardTest from "../CardTest";
import PParkingLot from "../ParkingLotCard";

export default function Parent() {
  const [parkingLots, getParkingLots] = useState("");

  const url = "https://localhost:44381/";

  useEffect(() => {
    getAllParkingLots();
  }, []);

  const getAllParkingLots = () => {
    axios
      .get(`${url}central/parkinglots`)
      .then((response) => {
        const allParkingLots = response.data.parkingLots.allParkingLots;
        getParkingLots(allParkingLots);
      })
      .catch((error) => console.error(`Error: ${error}`));
  };

  return <PParkingLot parkingLots={parkingLots}></PParkingLot>;
}
