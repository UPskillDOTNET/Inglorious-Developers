import React, { useContext, useEffect } from "react";
import MyContext from "../context/AppContext";
import {
  fetchReservations,
  fetchReservationsStarted,
  URL_API,
} from "../context/Actions";
import { View, Text, StyleSheet } from "react-native";
import { Card } from "react-native-elements";

const ReservationsList = () => {
  const { state, dispatch } = useContext(MyContext);
  const { reservations } = state;
  const { loading, error, data } = reservations;

  useEffect(() => {
    dispatch(fetchReservationsStarted());
    const url = `${URL_API}/central/reservations/users/3`;
    console.log(url);
    const request = {};
    fetchReservations(url, request, dispatch);
  }, []);
  console.log(data);
  if (loading === true) {
    return (
      <View>
        <Text>Loading...please wait!</Text>
      </View>
    );
  } else {
    if (error !== null) {
      return (
        <View>
          <Text>There was an Error!</Text>
        </View>
      );
    } else {
      if (data.length > 0) {
        return (
          <View>
            <Text style={styles.title}>YOUR RESERVATIONS</Text>{" "}
            {data.map((reservation, index) => (
              <Card key={index}>
                <Text>ReservationID: {reservation.centralReservationID}</Text>
                <Text>Final Price: {reservation.finalPrice}</Text>
                <Text>Parking Spot ID: {reservation.parkingSpotID}</Text>
              </Card>
            ))}
          </View>
        );
      } else {
        return (
          <View>
            <Text>No data...</Text>
          </View>
        );
      }
    }
  }
};

const styles = StyleSheet.create({
  title: {
    marginTop: 16,
    paddingVertical: 8,
    color: "white",
    textAlign: "center",
    fontSize: 30,
    fontWeight: "bold",
    flex: 1,
  },
});
export default ReservationsList;
