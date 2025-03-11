import { useState } from "react";
import {
  UserLoginRequest,
  UserLoginResponse,
} from "../../Services/UserClient/userContracts";
import { userRestClient } from "../../Services/UserClient/userRest";
import { notifications } from "@mantine/notifications";
import { useDispatch } from "react-redux";
import { userActions } from "../../Redux/features/User/userSlice";

export const useLoginUser = (): [
  UserLoginResponse | undefined,
  boolean,
  (request: UserLoginRequest) => Promise<void>
] => {
  const dispatch = useDispatch();
  const [response, setResponse] = useState<UserLoginResponse>();
  const [isLoading, setIsLoading] = useState(false);

  const login = async (request: UserLoginRequest) => {
    setResponse(undefined);
    setIsLoading(true);

    const res = await userRestClient.login(request);

    if (res.errors) {
      if (res.errors.some((e) => e.code === 404))
        notifications.show({
          message: "לקוח לא נמצא",
          color: "red",
        });

      if (res.errors.some((e) => e.code === 999))
        notifications.show({
          message: "שגיאת שרת",
          color: "red",
        });

      if (res.errors.some((e) => e.code === 1 || e.code === 2))
        notifications.show({
          message: "תעדות זהות או סיסמא שגויים",
          color: "red",
        });
    }

    if (res.user) {
      dispatch(userActions.loginUser(res.user));
      notifications.show({
        message: "התחברות בוצעה בהצלחה",
        color: "green",
      });
    }

    setResponse(res);
    setIsLoading(false);
  };

  return [response, isLoading, login];
};
