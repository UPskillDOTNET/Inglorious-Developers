import axios from "axios";

// export function makeRequestToAPI(url, request, success, failure) {
//   axios
//     .get(url, request)
//     .then((res) => res.json())
//     .then((res) => success(res))
//     .catch((err) => failure(err.message));
// }

export function makeRequestToAPI(url, request, success, failure) {
  fetch(url, request)
    .then((res) => res.json())
    .then((res) => success(res))
    .catch((err) => failure(err.message));
}
