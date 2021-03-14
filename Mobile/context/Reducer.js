import {
  FETCH_RESERVATIONS_FAILURE,
  FETCH_RESERVATIONS_STARTED,
  FETCH_RESERVATIONS_SUCCESS,
} from "./Actions";

function reducer(state, action) {
  switch (action.type) {
    case FETCH_RESERVATIONS_STARTED:
      return {
        ...state,
        reservations: {
          loading: true,
          error: null,
          data: [],
        },
      };

    case FETCH_RESERVATIONS_SUCCESS:
      return {
        ...state,
        reservations: {
          loading: false,
          error: null,
          data: [...action.payload.data],
        },
      };
    case FETCH_RESERVATIONS_FAILURE:
      return {
        ...state,
        reservations: {
          loading: false,
          error: action.payload.error,
          data: [],
        },
      };
    case FETCH_PARKINGLOTS_STARTED:
      return {
        ...state,
        parkinglots: {
          loading: true,
          error: null,
          data: [],
        },
      };

    case FETCH_PARKINGLOTS_SUCCESS:
      return {
        ...state,
        parkinglots: {
          loading: false,
          error: null,
          data: [...action.payload.data],
        },
      };
    case FETCH_PARKINGLOTS_FAILURE:
      return {
        ...state,
        parkinglots: {
          loading: false,
          error: action.payload.error,
          data: [],
        },
      };
    default:
      return state;
  }
}

export default reducer;
