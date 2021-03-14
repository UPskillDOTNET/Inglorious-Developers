import React from "react";
import { View, StyleSheet } from "react-native";
import ReservationsList from "./ReservationsList";
import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";
import { Provider as AuthProvider } from "../context/AuthContext.js";
import { Context as AuthContext } from "../context/AuthContext";
import authFlow from "../components/authFlow";
import homeFlow from "../components/homeFlow";
import AppProvider from "../context/AppProvider";
import Explore from "../screens/explore";

const Wrapper = () => {
  const Stack = createStackNavigator();
  const { state } = React.useContext(AuthContext);
  return (
    // <View style={styles.container}>
    //   <ReservationsList />
    // </View>
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
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10,
  },
});

export default Wrapper;

// const Stack = createStackNavigator();

// function App() {
//   const { state } = React.useContext(AuthContext);
//   console.log(state);
//   return (
//     <NavigationContainer>
//       <Stack.Navigator>
//         {state.token === null ? (
//           <>
//             <Stack.Screen
//               options={{ headerShown: false }}
//               name="Auth"
//               component={authFlow}
//             />
//           </>
//         ) : (
//           <Stack.Screen
//             options={{ headerShown: false }}
//             name="Home"
//             component={homeFlow}
//           />
//         )}
//       </Stack.Navigator>
//     </NavigationContainer>
//   );
// }
