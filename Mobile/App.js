import { StatusBar } from "expo-status-bar";
import React from "react";
import { StyleSheet, Text, View } from "react-native";
import { SafeAreaProvider } from "react-native-safe-area-context";
import { Button, ThemeProvider } from "react-native-elements";

const theme = {
  Button: {
    titleStyle: {
      color: "red",
    },
  },
};

export default function App() {
  return (
    <SafeAreaProvider>
      <ThemeProvider theme={theme}>
        <Button title="Hey! Click me" />
        <Text>How you doing?</Text>
        <Button title="My Button" />
        <Button title="My 2nd Button" titleStyle={{ color: "pink" }} />
      </ThemeProvider>
    </SafeAreaProvider>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: "center",
    justifyContent: "center",
  },
});
