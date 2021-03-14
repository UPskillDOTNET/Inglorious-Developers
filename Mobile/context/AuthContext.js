import createDataContext from "./createDataContext";
import getAccessToken from "../Services/UserServices";

const authReducer = (state, action) => {
  switch (action.type) {
    case "signout":
      return { token: null, username: "" };
    case "signin":
    case "signup":
      return {
        token: action.payload.token,
        username: action.payload.username,
      };
    default:
      return state;
  }
};

const signup = (dispatch) => {
  return ({ username, password, email, name, nif }) => {
    console.log("Signup");
    var credentials = { username, password, email, name, nif };
    console.log(credentials);
    var token = getAccessToken(credentials);
    console.log(token);
    console.log("Signin feito");
    dispatch({
      type: "signUp",
      payload: {
        token,
        username,
      },
    });
  };
};

const signin = (dispatch) => {
  return ({ username, password }) => {
    var credentials = { username, password };
    console.log(credentials);
    var token = getAccessToken(credentials);
    console.log(token);
    console.log("Signin feito");
    dispatch({
      type: "signin",
      payload: {
        token,
        username,
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
  { token: null, username: "" }
);
