import { useState } from "react";
import {
  UserRegisterRequest,
  UserRegisterResponse,
} from "../../Services/UserClient/userContracts";
import { userRestClient } from "../../Services/UserClient/userRest";
import { notifications } from "@mantine/notifications";

export const useRegisterUser = () : [
  UserRegisterResponse | undefined,
  boolean,
  (request: UserRegisterRequest) => Promise<void>
] => {
  const [response, setResponse] = useState<UserRegisterResponse>();
  const [isLoading, setIsLoading] = useState(false);

  const register = async (request: UserRegisterRequest) => {
    setResponse(undefined);
    setIsLoading(true);

    const res = await userRestClient.register(request);

    if(res.errors) {
      notifications.show({
        message: "הרשמה נכשלה",
        title: "שגיאה",
        color: "red",
      })
    }

    setResponse(res);
    setIsLoading(false);
  };

  return [response, isLoading, register];
};
