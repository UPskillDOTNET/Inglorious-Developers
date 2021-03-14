import React from "react";
import { View, StyleSheet } from "react-native";
import ReservationsList from "./ReservationsList";

const Wrapper = () => {
  return (
    <View style={styles.container}>
      <ReservationsList />
    </View>
  );
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10,
  },
});

export default Wrapper;
