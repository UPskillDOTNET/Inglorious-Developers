// import * as React from "react";
// import { Text, View } from "react-native";
// import { NavigationContainer } from "@react-navigation/native";
// import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
// import Ionicons from "@expo/vector-icons/Ionicons";

// import Login from "../screens/login";
// import Register from "../screens/register";
// import Reservations from "../screens/reservations";
// import Explore from "../screens/explore.js";
// import UserDasboard from "../screens/userdashboard";

// const Tab = createBottomTabNavigator();

// export function Navigator() {
//   return (
//       <Tab.Navigator
//         screenOptions={({ route }) => ({
//           tabBarIcon: ({ focused, color, size }) => {
//             let iconName;

//             if (route.name === "UserDasboard") {
//               iconName = focused
//                 ? "ios-information-circle"
//                 : "ios-information-circle-outline";
//             } else if (route.name === "Explore") {
//               iconName = focused ? "ios-list" : "ios-list";
//             }

//             // You can return any component that you like here!
//             return <Ionicons name={iconName} size={size} color={color} />;
//           },
//         })}
//         tabBarOptions={{
//           activeTintColor: "tomato",
//           inactiveTintColor: "gray",
//         }}
//       >
//         <Tab.Screen name="UserDasboard" component={UserDasboard} />
//         <Tab.Screen name="Explore" component={Explore} />
//         <Tab.Screen name="Login" component={Login} />
//       </Tab.Navigator>
//   );
// }
