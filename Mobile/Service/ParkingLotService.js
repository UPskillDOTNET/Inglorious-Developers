const getParksFromApi = () => {
  return fetch("https://localhost:44381/central/parkinglots", {
    mode: "cors",
    method: "GET",
    headers: {
      Accept: "application/json",
    },
  })
    .then((response) => response.json())
    .then((json) => {
      console.log(JSON.stringify({}));
      return json.ParkingLots;
    })
    .catch((error) => {
      console.error(error);
    });
};

// var parkingLots = getParksFromApi();
// console.log(parkingLots);

export default getParksFromApi;
