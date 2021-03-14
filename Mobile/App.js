import * as React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";
import { Provider as AuthProvider } from "./context/AuthContext.js";
import { Context as AuthContext } from "./context/AuthContext";
import authFlow from "./components/authFlow";
import homeFlow from "./components/homeFlow";
import AppProvider from "./context/AppProvider";
import Wrapper from "./components/Wrapper.js";

// export default () => {
//   return (
//     <AuthProvider>
//       <AppProvider>
//         <App />
//       </AppProvider>
//     </AuthProvider>
//   );
// };

const App = () => {
  return (
    <NavigationContainer>
      <AuthProvider>
        <AppProvider>
          <Wrapper />
        </AppProvider>
      </AuthProvider>
    </NavigationContainer>
  );
};

export default App;
