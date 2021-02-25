import React from "react";
import { StyleSheet, Text, View, Image, TextInput } from "react-native";
import { Button } from "react-native-elements";

export default function LoginComponent() {
  return (
    <View style={styles.container}>
      <Image
        style={{ width: 250, height: 250 }}
        source={require("./assets/Park_Around.png")}
      />
      <TextInput placeholder="Username" style={styles.input} />
      <TextInput
        placeholder="Password"
        secureTextEntry={true}
        style={styles.input}
      />
      <View style={styles.test}>
        <Button title="Login" />
        <Button title="Register" />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#24305e",
    // backgroundColor: "#2e789e",
    alignItems: "center",
    justifyContent: "center",
  },
  input: {
    backgroundColor: "white",
    borderRadius: 25,
    padding: 8,
    margin: 10,
    width: 300,
    fontWeight: "bold",
    textAlign: "center",
    textTransform: "uppercase",
  },
  login: {
    color: "white",
    fontWeight: "bold",
    textAlign: "center",
  },
  test: {
    flexDirection: "row",
    justifyContent: "space-between",
  },
});
