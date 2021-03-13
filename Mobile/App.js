// import "react-native-gesture-handler";
// import * as React from "react";
// import  TabNavigator  from "./navigation/TabNavigator";
// import { NavigationContainer } from "@react-navigation/native";

// export default function App() {
//   return (
//     <NavigationContainer>
//       <TabNavigator />
//     </NavigationContainer>
//   );
// }

import * as React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { Icon } from "react-native-elements";
import Signin from "./screens/Singin";
import Tab1 from "./screens/Tab1";
import Explore from "./screens/explore";
import Tab3 from "./screens/Tab3";
import { Provider as AuthProvider } from "./context/AuthContext.js";
import { Context as AuthContext } from "./context/AuthContext";

const AuthStack = createStackNavigator();

function authFlow() {
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

const Tab = createBottomTabNavigator();

function homeFlow() {
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

const Stack = createStackNavigator();
function App() {
  const { state } = React.useContext(AuthContext);
  console.log(state);
  return (
    <NavigationContainer>
      <Stack.Navigator>
        {state.token === null ? (
          <>
            <Stack.Screen
              options={{ headerShown: false }}
              name="Auth"
              component={authFlow}
            />
          </>
        ) : (
          <Stack.Screen
            options={{ headerShown: false }}
            name="Home"
            component={homeFlow}
          />
        )}
      </Stack.Navigator>
    </NavigationContainer>
  );
}

export default () => {
  return (
    <AuthProvider>
      <App />
    </AuthProvider>
  );
};
