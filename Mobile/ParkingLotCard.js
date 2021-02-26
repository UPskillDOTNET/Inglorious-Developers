import { Text,View ,StyleSheet } from "react-native";
import { Card, Button, Icon, ThemeProvider } from "react-native-elements";
import React from "react";
import Parent from './Service/ParkingLotTesting'


// const styles = StyleSheet.create({
//   container: {
//     flex: 1,
//     flexDirection: "column",
//     padding: 15,
//     backgroundColor: "white",
//   },
//   button: {
//     borderRadius: 0,
//     marginLeft: 0,
//     marginRight: 0,
//     marginBottom: 0,
//   },
//   icon: {
//     color: "#ffffff",
//   },
//   image: {},
//   title: {
//     fontSize: 20,
//     textTransform: "uppercase",
//     paddingTop: 5,
//   },
//   location: {
//     textTransform: "uppercase",
//     color: "grey",
//     paddingBottom: 5,
//   },
// });




export default function ParkingLot() {
  const ParkingLotList = Parent();
    if (ParkingLotList.length = 0) {
      console.log(ParkingLotList)
      return ParkingLotList.map((ParkingLotList) => {
        console.log(ParkingLotList);
        return (
          <Card containerStyle={styles.container} key={Parent().ParkingLotList.id}>
            <Card.Image source={require("./assets/parque.jpeg")} />
            <Text style={styles.title}>{Parent().ParkingLot.name}</Text>
            <Text style={styles.location}>{Parent().ParkingLotList.location}</Text>
            <Text style={styles.location}>{Parent().ParkingLotList.capacity}</Text>
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
          </Card>
        )})
      
    }
    //  else {
    //   return (<View>No Parking Lots yet</View>)
    // }
  };
