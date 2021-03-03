import * as React from "react";
import { Text, View, StyleSheet } from "react-native";
import { ScrollView } from "react-native-gesture-handler";
import ParkingLotCard from "../ParkingLotCard";

export default function Explore() {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>PARKING LOTS</Text>
      <ScrollView>
        <ParkingLotCard />
      </ScrollView>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 24,
    backgroundColor: "#24305e",
  },
  title: {
    marginTop: 16,
    paddingVertical: 8,
    color: "white",
    textAlign: "center",
    fontSize: 30,
    fontWeight: "bold",
  },
});
