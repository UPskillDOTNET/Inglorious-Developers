
import * as React from 'react';
import { Text, View } from 'react-native';
import { Divider } from 'react-native-elements';
import { ThemeProvider, Header } from 'react-native-elements';

const theme = {
  Header:{
    backgroundColor:'pink',
    placement:'center'
  },
};

export default function UserDasboard() {
  return (
    <ThemeProvider theme={theme}>
    
      <Header><Text>whats</Text></Header>
  
    </ThemeProvider>

  );
}


