import { createSlice } from "@reduxjs/toolkit";
import { UserDto } from "../../../Services/UserClient/userContracts";

type UserState = {
  token?: string;
  user?: UserDto;
};

const initialState: UserState = {
  token: undefined,
  user: undefined,
};

export const user = createSlice({
  name: "user",
  initialState: initialState,
  reducers: {
    loginUser: (state, action) => {
      state.token = action.payload.token;
      state.user = action.payload.user;
    },
    logoutUser: (state) => {
      state.token = undefined;
      state.user = undefined;
    },
  },
});

export const userReducers = user.reducer;
export const userActions = user.actions;
