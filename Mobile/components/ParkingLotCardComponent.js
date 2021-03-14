import React, { Component } from "react";
import { Text, View, StyleSheet, TouchableOpacity, Modal } from "react-native";
import { Card, Button, Icon } from "react-native-elements";
import GetParkingLots from "../Services/ParkingLotServices";

export default class ParkingLotCardComponent extends Component {
  constructor() {
    super();
    this.state = {
      parkingLots: [],
      modalVisible: false,
    };
  }

  componentDidMount() {
    this.getParkinglots();
  }

  async getParkinglots() {
    let parkingLot = await GetParkingLots();
    this.setState({ parkingLots: parkingLot });
  }

  setModalVisible = (visible) => {
    this.setState({ modalVisible: visible });
  };

  render() {
    const { modalVisible } = this.state;
    return (
      <View>
        <Modal
          animationType="slide"
          transparent={true}
          visible={modalVisible}
          onRequestClose={() => {
            this.setModalVisible(!modalVisible);
          }}
          onBackdropPress={() => this.setState({ modal: false })}
        >
          <View
            style={{
              flex: 1,
              height: 180,
              flexDirection: "column",
              justifyContent: "space-around",
              alignItems: "center",
            }}
          >
            <View style={styles.modalView}>
              <TouchableOpacity
                onPress={() => this.setModalVisible(!modalVisible)}
                style={[styles.ola, { backgroundColor: "#29ae19" }]}
              >
                <Text>Editar</Text>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.ola, { backgroundColor: "#FF0000" }]}
              >
                <Text>Eliminar</Text>
              </TouchableOpacity>
            </View>
          </View>
        </Modal>
        {this.state.parkingLots.map((parkingLot, index) => (
          <Card Style={styles.container} key={index}>
            <Card.Image source={{ uri: parkingLot.imageURL }} />
            <Text style={styles.title}>{parkingLot.name}</Text>
            <Text style={styles.location}>{parkingLot.location}</Text>
            <Text style={styles.location}>{parkingLot.capacity}</Text>
            <Button
              onPress={() => this.setModalVisible(true)}
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
        ))}
      </View>
    );
  }
}

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
  ola: {
    alignItems: "center",
    justifyContent: "center",
    padding: 15,
    borderRadius: 7,
  },
  modalView: {
    backgroundColor: "white",
    borderRadius: 20,
    height: "40%",
    width: "100%",
    marginTop: "auto",
    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
  },
  input: {
    backgroundColor: "white",
    borderRadius: 25,
    padding: 8,
    margin: 10,
    width: 300,
    textAlign: "center",
    textTransform: "uppercase",
  },
});
