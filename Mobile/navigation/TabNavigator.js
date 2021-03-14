import * as React from "react";
import { Text, View } from "react-native";
import { NavigationContainer } from "@react-navigation/native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import Ionicons from "@expo/vector-icons/Ionicons";

import Login from "../screens/login";
import Dash from "../screens/register";
import Home from "../screens/home.js";
import Explore from "../screens/explore.js";
import MainStackNavigator from "./MainStackNavigator";

const Tab = createBottomTabNavigator();

function TabNavigator() {
  return (
    <Tab.Navigator
    //   screenOptions={({ route }) => ({
    //     tabBarIcon: ({ focused, color, size }) => {
    //       let iconName;

    //       if (route.name === "UserDasboard") {
    //         iconName = focused
    //           ? "ios-information-circle"
    //           : "ios-information-circle-outline";
    //       } else if (route.name === "Explore") {
    //         iconName = focused ? "ios-list" : "ios-list";
    //       }

    //       // You can return any component that you like here!
    //       return <Ionicons name={iconName} size={size} color={color} />;
    //     },
    //   })}
    //   tabBarOptions={{
    //     activeTintColor: "tomato",
    //     inactiveTintColor: "gray",
    //   }}
    >
      <Tab.Screen name="Home" component={Home} />
      <Tab.Screen name="Explore" component={Explore} />
      <Tab.Screen name="Login" component={MainStackNavigator} />
    </Tab.Navigator>
  );
}

export default TabNavigator;
