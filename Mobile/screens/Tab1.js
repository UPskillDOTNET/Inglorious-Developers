import React, { useContext } from "react";
import { View, Text,Button ,StyleSheet } from "react-native";
import { Context as AuthContext } from "../context/AuthContext";
import getAccessToken from "../Services/UserServices"


const Tab1 = ({ navigation }) => {
  const { state } = useContext(AuthContext);

  return (
    <View style={styles.master}>
      <Text style={styles.header}>Tab1</Text>
      <Text style={{ fontSize: 28 }}>Welcome, {state.username}</Text>
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

export default Tab1;
