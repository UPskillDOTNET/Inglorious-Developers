import React, { useState, useContext } from "react";
import { Icon } from "react-native-elements";
import { View, Text, StyleSheet, TouchableOpacity, Image } from "react-native";
import { Input, Button } from "react-native-elements";
import { Context as AuthContext } from "../context/AuthContext";

const Signin = ({ navigation }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const { state, signin } = useContext(AuthContext);

  return (
    <View style={styles.master}>
      <Image
        style={styles.image}
        source={require("../assets/Park_Around_White.png")}
      />
      <View style={styles.body}>
        <Input
          placeholder="Username"
          onChangeText={setUsername}
          value={username}
          style={styles.bodyText}
        />
        <Input
          placeholder="Password"
          onChangeText={setPassword}
          value={password}
          secureTextEntry
          style={styles.bodyText}
        />
        <Button
          title="Login"
          type="clear"
          onPress={() => {
            signin({ username, password });
          }}
        />
        <View style={styles.link}>
          <Text style={styles.text}>Dont have an account? </Text>
          <TouchableOpacity onPress={() => {}}>
            <Text style={styles.textSignUp}>Sign up Here</Text>
          </TouchableOpacity>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  master: {
    padding: 16,
    flex: 1,
    alignItems: "stretch",
    justifyContent: "center",
    backgroundColor: "#2A1B3D",
  },
  header: {
    fontSize: 32,
    marginBottom: 18,
    alignSelf: "center",
  },
  text: {
    fontSize: 16,
    marginTop: 16,
    color: "white",
  },
  textSignUp: {
    fontSize: 16,
    marginTop: 16,
    color: "rgb(216,81,193)",
    fontWeight: "700",
  },
  link: {
    flexDirection: "row",
    justifyContent: "center",
  },
  image: {
    flex: 1,
    width: 250,
    height: 250,
    padding: 50,
    margin: 30,
    resizeMode: "contain",
    alignSelf: "center",
  },
  body: {
    flex: 1,
  },
  icon: {
    backgroundColor: "white",
  },
  bodyText: {
    backgroundColor: "white",
    borderRadius: 25,
    padding: 8,
    margin: 10,
    width: 300,
    textAlign: "center",
    textTransform: "uppercase",
  },
});

export default Signin;
