
 import React from "react";
 import { StyleSheet, Text, View, Image, TextInput } from "react-native";
 import { SafeAreaProvider } from "react-native-safe-area-context";
 import { useNavigation } from '@react-navigation/native';
 import { Button, ThemeProvider } from "react-native-elements";
 
 
 const theme = {
     Button: {
       titleStyle: {
         color: "snow",
       },
       borderRadius:{
           borderRadius:20,
       }
     },
   };
   
 
 function Register() {
     const navigation = useNavigation();
   return (
     <SafeAreaProvider>
       <ThemeProvider theme={theme}>
         <View style={styles.container}>
           <Image
             style={{ width: 250, height: 250 }}
             source={require("../assets/Park_Around.png")}
           />
 
           <Text style={styles.login}>USERNAME </Text>
           <TextInput style={styles.input} />
           <Text Text style={styles.login}>
             PASSWORD{" "}
           </Text>
           <TextInput secureTextEntry={true} style={styles.input} />
           <Text Text style={styles.login}>
            NIF
           </Text>
           <TextInput style={styles.input} />
           <Text style={styles.login}>FULL NAME</Text>
           <TextInput style={styles.input} />
         
           <View style={styles.test}>
             <Button style={styles.test2} title="Submit" />
         <Button style={styles.test2} onPress={() => navigation.goBack()} title="Cancel" />
       </View>
       </View>
       </ThemeProvider>
     </SafeAreaProvider>
     );
   }
 
 
 export default Register;
  
 
 
 
 
 const styles = StyleSheet.create({
   container: {
     flex: 1,
     backgroundColor: "#24305e",
     // backgroundColor: "#2e789e",
     alignItems: "center",
     justifyContent: "center",
   },
   input: {
     backgroundColor: "white",
     borderRadius: 25,
     padding: 8,
     margin: 10,
     width: 300,
   },
   login: {
     color: "white",
     fontWeight: "bold",
     textAlign: "center",
   },
   test: {
     flexDirection: "row",
     justifyContent: "space-between",
   },
   test2:{
       width:200,
       margin:5,
   }
 });