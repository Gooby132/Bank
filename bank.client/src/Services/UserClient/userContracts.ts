import { GeneralResponse } from "../Commons/dtos";

export type UserClient = {
  login: (request: UserLoginRequest) => Promise<UserLoginResponse>;
  register: (request: UserRegisterRequest) => Promise<UserRegisterResponse>;
  makeTransaction: (
    request: UserMakeTransactionRequest
  ) => Promise<UserMakeTransactionResponse>;
};

export type UserMakeTransactionRequest = {
  user: UserDto;
  sendTo: number;
  amount: number;
  operationType: OperationType;
};

export type UserMakeTransactionResponse = {
  updatedAccount?: AccountDto;
} & GeneralResponse;

// register request

export type UserRegisterRequest = {
  user: UserDto;
  password: string;
};

export type UserRegisterResponse = {
  user?: UserDto;
} & GeneralResponse;

// login request

export type UserLoginRequest = {
  tz: string;
  password: string;
};

export type UserLoginResponse = {
  user?: UserDto;
} & GeneralResponse;

export enum OperationType {
  Deposit = 0,
  Withdraw,
}

export type OperationDto = {
  type: OperationType;
  value: number;
  sentTo: number;
};

export type AccountDto = {
  id: number;
  currency: number;
  operations: OperationDto[];
};

export type UserDto = {
  tz: string;
  fullName: string;
  englishFullName: string;
  birthDateInUtc: string;
  account: AccountDto;
};
