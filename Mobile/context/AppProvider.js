import React, { useReducer } from "react";
import  MyContext  from "./AppContext";
import reducer from "./Reducer";

const initialState = {
  reservations: {
    loading: true,
    error: null,
    data: [],
  },
};

const reservation_labels = {
  id: "ID",
  userID: "userID",
  
};

const AppProvider = (props) => {
  const [state, dispatch] = useReducer(reducer, initialState);
  return (
    <MyContext.Provider
      value={{
        state,
        reservation_labels,
        dispatch,
      }}
    >
      {props.children}
    </MyContext.Provider>
  );
  AppProvider.propTypes = {
    children: PropTypes.node,
};

export default AppProvider;
