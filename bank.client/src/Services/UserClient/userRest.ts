import { UserClient } from "./userContracts";
import { restClient } from "../Rest/restClient";

const LOGIN_URI = "/user/login";
const REGISTER_URI = "/user/register";

export const userRestClient: UserClient = {
  login: async (request) => {
    try {
      const response = await restClient.post(LOGIN_URI, request);
      return await response.data;
    } catch (error) {
      console.log(error);
      return {
        errors: [{ message: "unknown", code: 999 }],
      }
    }
  },
  register: async (request) => {
    try {
      const response = await restClient.post(REGISTER_URI, request);
      return await response.data;
    } catch (error) {
      console.log(error);
      return {
        errors: [{ message: "unknown", code: 999 }],
      }
    }
  },
};
