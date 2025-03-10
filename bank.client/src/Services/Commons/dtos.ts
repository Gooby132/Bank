export type ErrorDto = {
  message: string;
  code: number;
}

export type GeneralResponse = {
  errors?: ErrorDto[];
}