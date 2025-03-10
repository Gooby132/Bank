import { GeneralResponse } from "../Commons/dtos";

export type UserClient = {
  login: (request: UserLoginRequest) => Promise<UserLoginResponse>;
  register: (request: UserRegisterRequest) => Promise<UserRegisterResponse>;
}

export type UserRegisterRequest = {
  user: UserDto;
  password: string;
}

export type UserRegisterResponse = {
  user?: UserDto;
} & GeneralResponse

export type UserLoginRequest = {
  tz: string;
  password: string;
}

export type UserLoginResponse = {
  user?: UserDto;
} & GeneralResponse

export type AccountDto = {
  id: string;
}

export type UserDto = {
  tz: string;
  fullName: string;
  englishFullName: string;
  birthDate: string;
  accounts: AccountDto[];
}