import React from "react";

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

export default function ParkingLot(props) {
  const displayParkingLot = (props) => {
    const { parkingLots } = props;

    if (parkingLots.length > 0) {
      return parkingLots.map((parkingLot, index) => {
        console.log(parkingLot);
        return (
          <Card containerStyle={styles.container} key={parkingLot.id}>
            <Card.Image source={require("./assets/parque.jpeg")} />
            <Text style={styles.title}>{parkingLot.name}</Text>
            <Text style={styles.location}>{parkingLot.location}</Text>
            <Text style={styles.location}>{parkingLot.capacity}</Text>
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
        );
      });
    } else {
      return <View>No Parking Lots yet</View>;
    }
  };
  return <View>{displayParkingLot(props)}</View>;
}
