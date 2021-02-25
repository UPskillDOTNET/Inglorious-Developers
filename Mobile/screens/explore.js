import * as React from "react";
import { Text, View, StyleSheet } from "react-native";
import { ScrollView } from "react-native-gesture-handler";
import CardTest from "../CardTest";

export default function Explore() {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>PARKING LOTS</Text>
      <ScrollView>
        <CardTest name="Parque da Maria" city="Viana do Castelo" />
        <CardTest name="Parque do João" city="Esposende" />
        <CardTest name="Parque da Mariana" city="Felgueiras" />
        <CardTest name="Parque do Sérgio" city="Porto" />
        <CardTest name="Parque do Caio" city="Porto" />
        <CardTest name="Parque do Tiago" city="Viana do Castelo" />
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
