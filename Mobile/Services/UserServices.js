import axios from "axios";
import qs from "qs";
import AsyncStorage from "@react-native-async-storage/async-storage";

const API_CLIENT_ID = "mobile.client";
const API_CLIENT_SECRET = "Secret";
const API_BASE_URL = "http://192.168.31.64:70";

async function getAccessToken(credentials) {
  const axiosConfig = {
    baseURL: API_BASE_URL,
    timeout: 30000,
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
  };

  const requestData = {
    client_id: API_CLIENT_ID,
    client_secret: API_CLIENT_SECRET,
    grant_type: "password",
    username: credentials.username,
    password: credentials.password,
    scope: [
      "openid, PrivAPI.read PrivAPI.write PubAPI.read PubAPI.write CAPI.read CAPI.write",
    ],
  };

  try {
    const result = await axios.post(
      "/connect/token",
      qs.stringify(requestData),
      axiosConfig
    );
    console.log("Token:", result.data);
    var token = result.data.access_token;
    await AsyncStorage.setItem("token", token);
    var x = await AsyncStorage.getItem("token");
    console.log(x);
    var userinfo = await getUserInfo(result.data.access_token);
    console.log(userinfo);
    var reservations = getUserReservations(result.data.access_token, userinfo);
    console.log(reservations);
    return result.data;
  } catch (err) {
    return err;
  }
}
export default getAccessToken;

async function getUserInfo() {
  var token = await AsyncStorage.getItem("token");
  const axiosConfig = {
    baseURL: API_BASE_URL,
    timeout: 30000,
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + token,
    },
  };
  console.log("Config:", axiosConfig);
  try {
    const result = await axios.get("/connect/userinfo", axiosConfig);
    console.log(result.data);
    var userId = result.data.sub;
    console.log(userId);
    await AsyncStorage.setItem("userID", userId);
    var userID = await AsyncStorage.getItem("userID");
    console.log(userID);
    return userId;
  } catch (err) {
    return err;
  }
}

