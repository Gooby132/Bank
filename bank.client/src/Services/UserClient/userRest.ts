import { UserClient } from "./userContracts";
import { restClient } from "../Rest/restClient";
import axios from "axios";

const LOGIN_URI = "/api/Users/login";
const REGISTER_URI = "/api/Users/register";
const MAKE_TRANSACTION_URI = "/api/Users/make-transaction";

export const userRestClient: UserClient = {
  makeTransaction: async (request) => {
    try {
      const response = await restClient.post(MAKE_TRANSACTION_URI, request);
      return await response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.status === axios.HttpStatusCode.NotFound)
          return {
            errors: [{ message: "user not found", code: 404 }],
          };

        if (error.status === axios.HttpStatusCode.BadRequest)
          return {
            errors: error.response!.data.errors,
          };
      }
      return {
        errors: [{ message: "unknown", code: 999 }],
      };
    }
  },
  login: async (request) => {
    try {
      const response = await restClient.post(LOGIN_URI, request);
      return await response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.status === axios.HttpStatusCode.NotFound)
          return {
            errors: [{ message: "user not found", code: 404 }],
          };

        if (error.status === axios.HttpStatusCode.BadRequest)
          return {
            errors: error.response!.data.errors,
          };
      }
      return {
        errors: [{ message: "unknown", code: 999 }],
      };
    }
  },
  register: async (request) => {
    try {
      const response = await restClient.post(REGISTER_URI, request);
      return await response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.status === axios.HttpStatusCode.NotFound)
          return {
            errors: [{ message: "user not found", code: 404 }],
          };

        if (
          error.status === axios.HttpStatusCode.BadRequest ||
          error.status === axios.HttpStatusCode.UnprocessableEntity
        )
          return {
            errors: error.response!.data.errors,
          };
      }
      return {
        errors: [{ message: "unknown", code: 999 }],
      };
    }
  },
};
