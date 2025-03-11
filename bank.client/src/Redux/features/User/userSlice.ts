import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import {
  AccountDto,
  UserDto,
  UserMakeTransactionResponse,
} from "../../../Services/UserClient/userContracts";

type UserState = {
  user?: UserDto;
};

const initialState: UserState = {
  user: undefined,
};

export const user = createSlice({
  name: "user",
  initialState: initialState,
  reducers: {
    updateTransactions: (state, action: PayloadAction<AccountDto>) => {
      if (state.user?.account) state.user.account = action.payload;
    },
    loginUser: (state, action: PayloadAction<UserDto>) => {
      state.user = action.payload;
    },
    logoutUser: (state) => {
      state.user = undefined;
    },
  },
});

export const userReducers = user.reducer;
export const userActions = user.actions;
