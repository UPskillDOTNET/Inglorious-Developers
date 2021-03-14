import * as React from "react";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { Icon } from "react-native-elements";
import Tab1 from "../screens/Tab1";
import Explore from "../screens/explore";
import Tab3 from "../screens/Tab3";

const Tab = createBottomTabNavigator();

export default function homeFlow() {
  return (
    <Tab.Navigator
      screenOptions={({ route }) => ({
        tabBarIcon: ({ focused, color, size }) => {
          let iconName;

          switch (route.name) {
            case "Tab1":
              iconName = focused ? "ios-checkbox" : "ios-checkbox-outline";
              break;
            case "Tab2":
              iconName = focused ? "ios-add-circle" : "ios-add-circle-outline";
              break;
            case "Tab3":
              iconName = focused
                ? "ios-information-circle"
                : "ios-information-circle-outline";
              break;
          }

          // You can return any component that you like here!
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
      <Tab.Screen name="Tab1" component={Tab1} />
      <Tab.Screen name="Explore" component={Explore} />
      <Tab.Screen name="Tab3" component={Tab3} />
    </Tab.Navigator>
  );
}
