import axios from 'axios';

const API = axios.create({
    baseURL:"https://localhost:44381/",
    mode: "cors"

})

export default API;