// ./navigation/DrawerNavigator.js

import React from "react";

import { createDrawerNavigator } from "@react-navigation/drawer";

import TabNavigator from "./TabNavigator";
import UserDasboard from "../screens/userdashboard";

const Drawer = createDrawerNavigator();

function DrawerNavigator() {
  return (
    <Drawer.Navigator>
      <Drawer.Screen name="Explore" component={TabNavigator} />
      <Drawer.Screen name="UserDashboard" component={UserDasboard} />
    </Drawer.Navigator>
  );
}

export { DrawerNavigator };
