import axios from "axios";

import AsyncStorage from "@react-native-async-storage/async-storage";




  

export async function makeRequestToAPI(url, request, success, failure) {

  const token = await AsyncStorage.getItem("token");
  console.log(token);

 var myHeaders = new Headers({
    "Content-Type": "application/json",
    Authorization: "Bearer " + token,
  });

  var myInit = { method: 'GET',
               headers: myHeaders,
               mode: 'cors',
               cache: 'default' };

  fetch(url, myInit)
    .then((res) => res.json())
    .then((res) => success(res))
    .catch((err) => failure(err.message));
}
