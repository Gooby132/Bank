import { useState } from "react";
import {
  UserLoginRequest,
  UserLoginResponse,
  UserMakeTransactionRequest,
  UserMakeTransactionResponse,
} from "../../Services/UserClient/userContracts";
import { userRestClient } from "../../Services/UserClient/userRest";
import { notifications } from "@mantine/notifications";
import { useDispatch, useSelector } from "react-redux";
import { userActions } from "../../Redux/features/User/userSlice";
import { RootState } from "../../Redux/store";

export const useMakeTransaction = (): [
  UserMakeTransactionResponse | undefined,
  boolean,
  (request: UserMakeTransactionRequest) => Promise<void>
] => {
  const user = useSelector((state: RootState) => state.userSlice).user;
  const dispatch = useDispatch();
  const [response, setResponse] = useState<UserMakeTransactionResponse>();
  const [isLoading, setIsLoading] = useState(false);

  const makeTransaction = async (
    request: Omit<UserMakeTransactionRequest, "user">
  ) => {
    if (!user) {
      notifications.show({
        message: "לקוח לא מחובר",
        color: "red",
      });
      return;
    }

    setResponse(undefined);
    setIsLoading(true);

    const res = await userRestClient.makeTransaction({
      ...request,
      user: user!,
    });

    if (res.errors) {
      if (res.errors.some((e) => e.code === 9))
        notifications.show({
          message: "אין מספיק כסף בחשבון",
          color: "red",
        });

      if (res.errors.some((e) => e.code === 11))
        notifications.show({
          message: "חשבון בנק לא חוקי",
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

    if (res.updatedAccount) {
      dispatch(userActions.updateTransactions(res.updatedAccount));
      notifications.show({
        message: "עסקה בוצעה בהצלחה",
        color: "green",
      });
    }

    setResponse(res);
    setIsLoading(false);
  };

  return [response, isLoading, makeTransaction];
};
