import * as React from "react";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { Icon } from "react-native-elements";
import ReservationsUser from "../screens/ReservationsUser";
import Explore from "../screens/Explore";
import Logout from "../screens/Logout";
import SignUp from "../screens/SignUp";

const Tab = createBottomTabNavigator();

const homeFlow = () => {
  return (
    <Tab.Navigator
      screenOptions={({ route }) => ({
        tabBarIcon: ({ focused, color, size }) => {
          let iconName;

          switch (route.name) {
            case "Explore":
              iconName = focused ? "ios-checkbox" : "ios-checkbox-outline";
              break;
            case "User Reservations":
              iconName = focused ? "ios-add-circle" : "ios-add-circle-outline";
              break;
            case "Log Out":
              iconName = focused
                ? "ios-information-circle"
                : "ios-information-circle-outline";
              break;
          }
          return (
            <Icon name={iconName} type="ionicon" size={size} color={color} />
          );
        },
      })}
      tabBarOptions={{
        activeTintColor: "tomato",
        inactiveTintColor: "gray",
      }}
    >
      <Tab.Screen name="Explore" component={Explore} />
      <Tab.Screen name="User Reservations" component={ReservationsUser} />
      <Tab.Screen name="Log Out" component={Logout} />
      <Tab.Screen name="SignUp" component={SignUp} />
    </Tab.Navigator>
  );
};

export default homeFlow;
