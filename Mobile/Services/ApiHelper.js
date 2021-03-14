import axios from "axios";

const API = axios.create({
  baseURL: "http://192.168.1.64:90/",
  mode: "cors",
});

export default API;
