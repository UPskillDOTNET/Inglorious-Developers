import React, { useEffect } from "react";
import { View, Text, StyleSheet } from "react-native";
import Wrapper from "../components/Wrapper";
import ReservationsList from "../components/ReservationsList";

const Tab2 = ({ navigation }) => {
  useEffect;
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
  },
  header: {
    fontSize: 32,
  },
});

export default Tab2;
