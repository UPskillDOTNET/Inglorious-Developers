import createDataContext from "./createDataContext";

// Dar set up dos reducers, functions e default values e exporta-los como provider e context.

// Este reducer aceita o state e uma action. O principio base dum reducer
// é retornar sempre um state, incluindo o state não alterado, para ser override, de acordo com a action que for escolhida e o payload, no switch case.

const authReducer = (state, action) => {
  switch (action.type) {
    case "signout":
      return { token: null, email: "" };
    case "signin":
    case "signup":
      return {
        token: action.payload.token,
        email: action.payload.email,
      };
    default:
      return state;
  }
};

const signup = (dispatch) => {
  return ({ email, password }) => {
    console.log("Signup");
  };
};

const signin = (dispatch) => {
  return ({ email, password }) => {
    // API REQUEST HERE
    console.log("Signin");
    dispatch({
      type: "signin",
      payload: {
        token: "token here",
        email,
      },
    });
  };
};

const signout = (dispatch) => {
  return () => {
    dispatch({ type: "signout" });
  };
};

export const { Provider, Context } = createDataContext(
  authReducer,
  { signin, signout, signup },
  { token: null, email: "" }
);
