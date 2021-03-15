import React from "react";
import { StyleSheet } from "react-native";
import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";
import { Context as AuthContext } from "../context/AuthContext";
import authFlow from "../components/authFlow";
import homeFlow from "../components/homeFlow";

const Wrapper = () => {
  const Stack = createStackNavigator();
  const { state } = React.useContext(AuthContext);
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
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10,
  },
});

export default Wrapper;
