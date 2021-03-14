import { makeRequestToAPI } from "../Services/Service";

export const URL_API = "http://192.168.1.64:90/";

export const FETCH_RESERVATIONS_STARTED = "FETCH_RESERVATIONS_STARTED";
export const FETCH_RESERVATIONS_SUCCESS = "FETCH_RESERVATIONS_SUCCESS";
export const FETCH_RESERVATIONS_FAILURE = "FETCH_RESERVATIONS_FAILURE";
export const FETCH_PARKINGLOTS_STARTED = "FETCH_PARKINGLOTS_STARTED";
export const FETCH_PARKINGLOTS_SUCCESS = "FETCH_PARKINGLOTS_SUCCESS";
export const FETCH_PARKINGLOTS_FAILURE = "FETCH_PARKINGLOTS_FAILURE";

export function fetchReservations(url, request, dispatch) {
  const success = (res) => dispatch(fetchReservationsSuccess(res));

  const failure = (err) => dispatch(fetchReservationsFailure(err.message));
  makeRequestToAPI(url, request, success, failure);
}

export function fetchReservationsStarted() {
  return {
    type: FETCH_RESERVATIONS_STARTED,
  };
}

export function fetchReservationsSuccess(reservations) {
  return {
    type: FETCH_RESERVATIONS_SUCCESS,
    payload: {
      data: [...reservations],
    },
  };
}

export function fetchReservationsFailure(message) {
  return {
    type: FETCH_RESERVATIONS_FAILURE,
    payload: {
      error: message,
    },
  };
}

export function fetchParkingLots(url, request, dispatch) {
  const success = (res) => dispatch(fetchParkingLotsSuccess(res));

  const failure = (err) => dispatch(fetchParkingLotsFailure(err.message));
  makeRequestToAPI(url, request, success, failure);
}

export function fetchParkingLotsStarted() {
  return {
    type: FETCH_PARKINGLOTS_STARTED,
  };
}

export function fetchParkingLotsSuccess(reservations) {
  return {
    type: FETCH_PARKINGLOTS_SUCCESS,
    payload: {
      data: [...reservations],
    },
  };
}

export function fetchParkingLotsFailure(message) {
  return {
    type: FETCH_PARKINGLOTS_FAILURE,
    payload: {
      error: message,
    },
  };
}
