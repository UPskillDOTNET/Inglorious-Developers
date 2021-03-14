import * as React from "react";
import { createStackNavigator } from "@react-navigation/stack";
import Signin from "../screens/Singin";

const AuthStack = createStackNavigator();

export default function authFlow() {
  return (
    <AuthStack.Navigator>
      <AuthStack.Screen
        options={{ headerShown: false }}
        name="Signin"
        component={Signin}
      />
      <AuthStack.Screen
        options={{ headerShown: false }}
        name="Signup"
        component={Signin}
      />
    </AuthStack.Navigator>
  );
}
