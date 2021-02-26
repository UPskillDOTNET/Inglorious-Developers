import React from "react";
import { createStackNavigator } from "@react-navigation/stack";

import Login from "../screens/login";
import Register from "../screens/register";

const Stack = createStackNavigator();

const screenOptionStyle = {
  headerStyle: {
    backgroundColor: "#9AC4F8",
  },
  headerTintColor: "white",
  headerBackTitle: "Back",
};

function MainStackNavigator() {
  return (
    <Stack.Navigator mode="modal" screenOptions={screenOptionStyle}>
      <Stack.Screen name="Login" component={Login} options={{ headerShown: false }} />
      <Stack.Screen name="Register" component={Register}  options={{ headerShown: false }}/>
    </Stack.Navigator>
  );
}

export default MainStackNavigator;
