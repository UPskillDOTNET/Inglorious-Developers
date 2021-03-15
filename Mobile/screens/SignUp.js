import React, { useState, useContext } from "react";
import { View, Text, StyleSheet, TouchableOpacity, Image } from "react-native";
import { Input, Button } from "react-native-elements";
import { Context as AuthContext } from "../context/AuthContext";

const SignUp = ({ navigation }) => {
  // const [username, setUsername] = useState("");
  // const [password, setPassword] = useState("");
  // const [email, setEmail] = useState("");
  // const [name, setName] = useState("");
  // const [nif, setNif] = useState("");
  // const { state, signup } = useContext(AuthContext);
  console.log("como estamos?");

  return (
    <View style={styles.master}>
      <Image
        style={styles.image}
        source={require("../assets/Park_Around_White.png")}
      />
      <View style={styles.body}>
        <Input
          placeholder="Username"
          // onChangeText={setUsername}
          // value={username}
          style={styles.bodyText}
        />
        <Input
          placeholder="Password"
          // onChangeText={setPassword}
          // value={password}
          secureTextEntry
          style={styles.bodyText}
        />
        <View style={styles.body}>
          <Input
            placeholder="Email"
            // onChangeText={setEmail}
            // value={email}
            style={styles.bodyText}
          />
          <View style={styles.body}>
            <Input
              placeholder="Name"
              // onChangeText={setName}
              // value={name}
              style={styles.bodyText}
            />
            <Input
              placeholder="Nif"
              // onChangeText={setNif}
              // value={nif}
              style={styles.bodyText}
            />
            <Button
              title="Register"
              type="clear"
              onPress={() => {
                signup({ username, password, email, name, nif });
              }}
            />
          </View>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  master: {
    padding: 16,
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

export default SignUp;
