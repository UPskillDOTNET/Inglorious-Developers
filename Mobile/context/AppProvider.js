import React, { useReducer } from "react";
import { Provider } from "./AppContext";
import reducer from "./Reducer";

const initialState = {
  reservations: {
    loading: true,
    error: null,
    data: [],
  },
  parkinglots: {
    loading: true,
    error: null,
    data: [],
  },
};

const AppProvider = (props) => {
  const [state, dispatch] = useReducer(reducer, initialState);
  return (
    <Provider
      value={{
        state,
        dispatch,
      }}
    >
      {props.children}
    </Provider>
  );
};

export default AppProvider;
