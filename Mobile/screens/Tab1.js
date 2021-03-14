import React, { useContext } from "react";
import { View, Text,Button ,StyleSheet } from "react-native";
import { Context as AuthContext } from "../context/AuthContext";
import AsyncStorage from '@react-native-async-storage/async-storage';


const Tab1 = ({ navigation }) => {
  const { state } = useContext(AuthContext);

 var xeila = AsyncStorage.getItem("userID");
 console.log(xeila);
  return (
    <View style={styles.master} >
      <Text style={styles.header}>Tab1</Text>
      <Text style={{ fontSize: 28 }}>Welcome, {state.username} </Text>  
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
