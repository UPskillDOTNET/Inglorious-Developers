import React from "react";
import {
  StyleSheet,
  Text,
  View,
  Image,
  TextInput,
  TouchableOpacity,
} from "react-native";
import { Button } from "react-native-elements";
import { useNavigation } from "@react-navigation/native";
import Register from "./screens/register";

export default function LoginComponent() {
  const navigation = useNavigation();

  return (
    <View style={styles.container}>
      <Image
        style={{ width: 250, height: 250 }}
        source={require("./assets/Park_Around.png")}
      />
      <View style={styles.general}>
        <TextInput placeholder="Username" style={styles.input} />
        <TextInput
          placeholder="Password"
          secureTextEntry={true}
          style={styles.input}
        />
        <View style={styles.test}>
          <View>
            <Button style={styles.loginButtons} title="Login" />
          </View>
          <TouchableOpacity>
            <Text style={styles.forgetPassword}>Forgot your password?</Text>
          </TouchableOpacity>
          <TouchableOpacity onPress={() => navigation.navigate(Register)}>
            <Text style={[styles.forgetPassword, styles.registerLink]}>
              New Account
            </Text>
          </TouchableOpacity>
        </View>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#24305e",
    alignItems: "center",
    justifyContent: "center",
  },
  general: {
    width: 300,
    alignItems: "center",
  },
  input: {
    backgroundColor: "white",
    borderRadius: 25,
    padding: 8,
    margin: 10,
    width: 300,
    textAlign: "center",
    textTransform: "uppercase",
  },
  login: {
    color: "white",
    fontWeight: "bold",
    textAlign: "center",
  },
  loginButtons: {
    margin: 20,
    textTransform: "uppercase",
    width: 300,
  },
  forgetPassword: {
    flex: 1,
    color: "white",
    textAlign: "center",
    flexDirection: "column",
  },
  registerLink: {
    marginVertical: 10,
    textDecorationLine: "underline",
  },
});
