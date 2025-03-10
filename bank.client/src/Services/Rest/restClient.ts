import axios from "axios";
import { API_URI } from "../../Consts/api";

export const restClient = axios.create({
  baseURL: API_URI,
})