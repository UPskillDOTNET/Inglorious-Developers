import * as React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";
import { Provider as AuthProvider } from "./context/AuthContext.js";
import { Context as AuthContext } from "./context/AuthContext";
import authFlow from "./components/authFlow";
import homeFlow from "./components/homeFlow";
import MyContext from "./context/AppContext";

const Stack = createStackNavigator();

function App() {
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
}

export default () => {
  return (
     <AuthProvider>
        <App />
      </AuthProvider>

  );
};
