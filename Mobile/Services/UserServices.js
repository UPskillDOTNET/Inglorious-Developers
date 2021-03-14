import axios from 'axios';
import qs from 'qs';
import AsyncStorage from '@react-native-async-storage/async-storage';

const API_CLIENT_ID = 'mobile.client';
const API_CLIENT_SECRET = 'Secret';
const API_BASE_URL = "https://localhost:5001";    

export default async function  getAccessToken (credentials) {
    const axiosConfig = {
        baseURL: API_BASE_URL,
        timeout: 30000,
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    };

    const requestData = {
        client_id: API_CLIENT_ID,
        client_secret: API_CLIENT_SECRET,
        grant_type: 'password',
        username: credentials.username,
        password: credentials.password,
        scope: ['openid, PrivAPI.read PrivAPI.write PubAPI.read PubAPI.write CAPI.read CAPI.write']
    };

    try {
        const result = await axios.post('/connect/token', qs.stringify(requestData), axiosConfig);  
        console.log("Token:",result.data);
        console.log(result.data.access_token);
        var token = result.data.access_token;
        await AsyncStorage.setItem("token", token);
        var userinfo = await getUserInfo(token);
        console.log(userinfo); 
        var reservations = getUserReservations();
        console.log(reservations);
        return result.data;
        
    } catch (err) {
        return err;
    }  

};



 async function  getUserInfo(token){

    const axiosConfig = {
        baseURL: API_BASE_URL,
        timeout: 30000,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        }
    };
    console.log("Config:", axiosConfig);
    try {
        const result = await axios.get('/connect/userinfo', axiosConfig);  
        console.log(result.data);
        var userId = result.data.sub;
        await AsyncStorage.setItem("userID", userId);
        console.log(userId);
        return userId;
    } catch (err) {
        return err;
    }  

}

 async function  getUserReservations(){

    var xeila1 = await AsyncStorage.getItem('accessToken');
    const axiosConfig = {
        baseURL: "http://192.168.1.3:90",
        timeout: 30000,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + xeila1,
        }
    };

    var xeila2 = await AsyncStorage.getItem('userID');
    console.log("sub",xeila2);
    console.log("Config:", axiosConfig);
    try {
        const result = await axios.get('/reservations/users/' + xeila2, axiosConfig);  
        console.log(result.data);
        var reservations = data;
        console.log(reservations);         
        return result.data;
        
    } catch (err) {
        return err;
    }  

}