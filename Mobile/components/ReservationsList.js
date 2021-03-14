import React, { useContext, useEffect } from "react";
import { FlatList } from "react-native-gesture-handler";
import MyContext from "../context/AppContext";
import {
  fetchReservations,
  fetchReservationsStarted,
  URL_API,
} from "../context/Actions";


const ReservationsList = () => {
  const { state, dispatch } = useContext(MyContext);
  const {reservations} = state;
  const { loading, error, data } = reservations;

  useEffect(() => {
    dispatch(fetchReservationsStarted());
    const url = `${URL_API}/central/reservations/users/3`;
    const request = {};
    fetchReservations(url, request, dispastch);
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
        return (
          <FlatList data={data} keyExtractor={(item) => item.id.toString()} />
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

export default ReservationsList;
