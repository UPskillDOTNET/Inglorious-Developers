import React, { Component } from "react";
import { Dimensions, Platform } from "react-native";
import {
  createStackNavigator,
  createBottomTabNavigator,
  createAppContainer,
} from "react-navigation-stack";

import Login from "../screens/login";
import Register from "../screens/register";
import Reservations from "../screens/reservations";
import Explore from "../screens/explore";
import UserDashboard from "../screens/userdashboard";

let screen = Dimensions.get("window");

export const Tabs = createBottomTabNavigator({
  Login: {
    screen: Login,
    navigationOptions: {
      tabBarLabel: "Login",
      tabBarIcon: ({ tintColor }) => (
        <Icon name="open-book" type="entypo" size={28} color={tintColor} />
      ),
    },
  },
  UserDashboard: {
    screen: UserDashboard,
    navigationOptions: {
      tabBarLabel: "UserDashboard",
      tabBarIcon: ({ tintColor }) => (
        <Icon name="list" type="entypo" size={28} color={tintColor} />
      ),
    },
  },
  Explore: {
    screen: Explore,
    navigationOptions: {
      tabBarLabel: "Explore",
      tabBarIcon: ({ tintColor }) => (
        <Icon name="ios-person" type="ionicon" size={28} color={tintColor} />
      ),
    },
  },
});

export const LoginStack = createStackNavigator({
  Login: {
    screen: Login,
    navigationOptions: ({ navigation }) => ({
      tabBarLabel: "Login",
      header: null,
    }),
  },
  Register: {
    screen: Register,
    navigationOptions: ({ navigation }) => ({
      tabBarLabel: "Register",
      header: null,
    }),
  },
});

export const ExploreStack = createStackNavigator({
  Explore: {
    screen: Explore,
    navigationOptions: ({ navigation }) => ({
      tabBarLabel: "Explore",
      header: null,
    }),
  },
  Reservations: {
    screen: Reservations,
    navigationOptions: ({ navigation }) => ({
      tabBarLabel: "Reservations",
      header: null,
    }),
  },
});

export const CreateAppContainer = createAppContainer(stackNavigator);
