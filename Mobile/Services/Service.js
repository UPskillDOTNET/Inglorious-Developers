export function makeRequestToAPI(url, request, success, failure) {
  axios
    .get(url)
    .then((res) => res.json())
    .then((res) => success(res))
    .catch((err) => failure(err.message));
}
