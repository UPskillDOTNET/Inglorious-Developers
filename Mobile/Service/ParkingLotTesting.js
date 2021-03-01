import React, { Component, button } from "react";
import API from './ApiHelper';
import { Text,View ,StyleSheet } from "react-native";
import { Card, Button, Icon, ThemeProvider } from "react-native-elements";


export default async function GetParkinglot() {
    let data =  await API.get('central/parkinglots').then(({data}) => data);
    console.log(data);
    return data};
