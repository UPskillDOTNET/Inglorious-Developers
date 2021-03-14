import API from "./ApiHelper";

export default async function GetParkingLots() {
  let data = await API.get("central/parkinglots").then(({ data }) => data);
  console.log(data);
  return data;
}
