import { useState } from "react";
import {
  UserRegisterRequest,
  UserRegisterResponse,
} from "../../Services/UserClient/userContracts";
import { userRestClient } from "../../Services/UserClient/userRest";
import { notifications } from "@mantine/notifications";
import { useDispatch } from "react-redux";
import { userActions } from "../../Redux/features/User/userSlice";

export const useRegisterUser = (): [
  UserRegisterResponse | undefined,
  boolean,
  (request: UserRegisterRequest) => Promise<void>
] => {
  const dispatch = useDispatch();
  const [response, setResponse] = useState<UserRegisterResponse>();
  const [isLoading, setIsLoading] = useState(false);

  const register = async (request: UserRegisterRequest) => {
    setResponse(undefined);
    setIsLoading(true);

    const res = await userRestClient.register(request);

    if (res.errors) {
      if (res.errors.some((error) => error.code === 8))
        notifications.show({
          message: "המשתמש כבר קיים",
          title: "שגיאה",
          color: "red",
        });
      if (res.errors.some((error) => error.code === 999))
        notifications.show({
          message: "הרשמה נכשלה",
          title: "שגיאה",
          color: "red",
        });
    }

    if (res.user) {
      dispatch(userActions.loginUser(res.user));
      notifications.show({
        message: "הרשמה בוצעה בהצלחה",
        title: "הודעה",
        color: "green",
      });
    }

    setResponse(res);
    setIsLoading(false);
  };

  return [response, isLoading, register];
};
