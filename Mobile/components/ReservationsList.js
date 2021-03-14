import React, { useContext, useEffect } from "react";
import MyContext from "../context/AppContext";
import {
  fetchReservations,
  fetchReservationsStarted,
  URL_API,
} from "../context/Actions";
import { FlatList, View, Text, StyleSheet } from "react-native";

const ReservationsList = () => {
  const { state, dispatch } = useContext(MyContext);
  const { reservations } = state;
  const { loading, error, data } = reservations;

  useEffect(() => {
    dispatch(fetchReservationsStarted());
    const url = `${URL_API}/central/reservations/users/3`;
    console.log(url);
    const request = {};
    var x = fetchReservations(url, request, dispatch);
    console.log(x.payload.data);
  }, []);

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
          <Text>There was an error...</Text>
        </View>
      );
    } else {
      if (data.length > 0) {
        return <Text>Entrei aqui</Text>;
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

export default ReservationsList;
