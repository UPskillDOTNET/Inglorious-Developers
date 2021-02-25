import React from "react";
import { StyleSheet, Text, View, Image, TextInput } from "react-native";
import { SafeAreaProvider } from "react-native-safe-area-context";
import { Button, ThemeProvider } from "react-native-elements";

const theme = {
  Button: {
    titleStyle: {
      color: "white",
    },
  },
};

export default function App() {
  return (
    <SafeAreaProvider>
      <ThemeProvider theme={theme}>
        <View style={styles.container}>
          <Image
            style={{ width: 250, height: 250 }}
            source={require("./assets/Park_Around.png")}
          />

          <Text style={styles.login}>USERNAME </Text>
          <TextInput style={styles.input} />
          <Text Text style={styles.login}>
            PASSWORD{" "}
          </Text>
          <TextInput secureTextEntry={true} style={styles.input} />
          <View style={styles.test}>
            <Button title="Login" />
            <Button title="Register" />
          </View>
        </View>
      </ThemeProvider>
    </SafeAreaProvider>
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
