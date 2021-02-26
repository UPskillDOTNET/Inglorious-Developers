import React, { useEffect, useState, Object } from "react";
import axios from 'axios';
import CardTest from "../CardTest";
import PParkingLot from "../ParkingLotCard";
import Explore from "../screens/explore";


export default function Parent() {
  // const [parkingLots, getParkingLots] = useState("");

  const url = "https://localhost:44381/";

  // useEffect(() => {
  //   getAllParkingLots();
  // }, []);


    axios
      .get(`${url}central/parkinglots`)
      .then((response) => {
        console.log(response.data);
        const ParkingLotList = response.data;
        const ParkingLots = ParkingLotList.map(function(item,index){
          return {
            key: item.parkingLotID,
            name: item.name,
            capacity: item.capacity,
          }
        })
        console.log(ParkingLots);
      })
      .catch((error) => console.error(`Error: ${error}`));

      return ParkingLots;
  
  
}
