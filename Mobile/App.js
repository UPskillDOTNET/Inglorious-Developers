import "react-native-gesture-handler";
import * as React from "react";
import { DrawerNavigator } from "./navigation/DrawerNavigator";
import { NavigationContainer } from "@react-navigation/native";

export default function App() {
  return (
    <NavigationContainer>
      <DrawerNavigator />
    </NavigationContainer>
  );
}
