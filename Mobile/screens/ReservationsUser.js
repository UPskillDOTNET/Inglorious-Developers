import React, { useEffect } from "react";
import { View, StyleSheet } from "react-native";
import ReservationsList from "../components/ReservationsList";

const ReservationsUser = ({ navigation }) => {
  
  return (
    <View style={styles.master}>
      <ReservationsList />
    </View>
  );
};

const styles = StyleSheet.create({
  master: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#2A1B3D",
  },
  header: {
    fontSize: 32,
  },
});

export default ReservationsUser;
