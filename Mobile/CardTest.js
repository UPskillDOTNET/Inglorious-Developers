import { Text, StyleSheet } from "react-native";
import { Card, Button, Icon, ThemeProvider } from "react-native-elements";
import React from "react";

export default function CardTest(props) {
  return (
    <Card containerStyle={styles.container}>
      <Card.Image source={require("./assets/parque.jpeg")} />
      <Text style={styles.title}>{props.name}</Text>
      <Text style={styles.location}>{props.city}</Text>
      <Text style={styles.location}>? Lugares disponíveis</Text>
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
}
// const CardTest = (props) => {
//   return (
//     <Card containerStyle={styles.container}>
//       <Card.Image source={require("./assets/parque.jpeg")} />
//       <Text text={styles.title}>{this.props.name}</Text>
//       <Text text>{this.props.city}</Text>
//       <Button
//         buttonStyle={{
//           backgroundColor: "#53a9d6",
//           borderRadius: 0,
//           marginLeft: 0,
//           marginRight: 0,
//           marginBottom: 0,
//         }}
//         icon={<Icon name="check" size={30} color="white" />}
//         title="Book now"
//       />
//     </Card>
//   );
// };

// export default CardTest;

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
