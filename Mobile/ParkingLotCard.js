import React, { Component, button } from "react";
import { Text,View ,StyleSheet } from "react-native";
import { Card, Button, Icon, ThemeProvider } from "react-native-elements";
import GetParkinglot from './Service/ParkingLotTesting'




export default class Test extends Component{
  
  state ={
    parkingLots: []
  }

  constructor (){
    super();
    this.getParkinglots();
  }
 
   async getParkinglots(){
     let parkingLot = await GetParkinglot();
     this.setState({parkingLots: parkingLot});
    console.log(this.state)
  }
 



render(){
  return(
    <View>
   {this.state.parkingLots.map( (parkingLot, index) =>  <Card Style={styles.container} key= {index}>
    <Card.Image source={(parkingLot.imageURL)} />
    <Text style={styles.title} >{parkingLot.name}</Text>
    <Text style={styles.location} >{parkingLot.location}</Text>
    <Text style={styles.location} >{parkingLot.capacity}</Text>
    <Button
      buttonStyle={{
        backgroundColor: "#53a9d6",
        borderRadius: 0,
        marginLeft: 0,
        marginRight: 0,
        marginBottom: 0,
      }}
      icon={<Icon name="check" size={30} color="white" />}
      title="Book now"
    />
  </Card>)}
  </View>
  )
}
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: "column",
    padding: 15,
    backgroundColor: "white",
  },
  button: {
    borderRadius: 0,
    marginLeft: 0,
    marginRight: 0,
    marginBottom: 0,
  },
  icon: {
    color: "#ffffff",
  },
  image: {},
  title: {
    fontSize: 20,
    textTransform: "uppercase",
    paddingTop: 5,
  },
  location: {
    textTransform: "uppercase",
    color: "grey",
    paddingBottom: 5,
  },
});

