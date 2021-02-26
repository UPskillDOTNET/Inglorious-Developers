import React, { useEffect } from "react";
import axios from "axios";
import CardTest from "../CardTest";

export default function Parent() {
  const [parkingLots, getAllParkingLots] = useState("");

  const url = "https://localhost:44381/";

  useEffect(() => {
    getAllParkingLots();
  }, []);

  const getAllParkingLots = () => {
    axios
      .get(`${url}central/parkinglots`)
      .then((response) => {
        const allParkingLots = response.data.parkingLots.allParkingLots;
        getAllParkingLots(allParkingLots);
      })
      .catch((error) => console.error(`Error: ${error}`));
  };

  return <CardTest parkingLots={parkingLots}></CardTest>;
}
