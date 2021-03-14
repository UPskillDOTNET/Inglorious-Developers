import React, { useContext } from "react";
import { View, Text, TouchableOpacity, StyleSheet } from "react-native";
import {
  URL_API,
  fetchUserTodosStarted,
  deleteUserTodos,
  fetchUserTodos,
  fetchReservationsStarted,
} from "../context/Actions";
import AppContext from "../context/AppContext";

const ReservationListItem = (props) => {
  const { state, dispatch } = useContext(AppContext);

  const handleOnClick = () => {
    dispatch(fetchReservationsStarted());
    const url = ``;
  };
};
