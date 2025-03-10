import {
  Box,
  Button,
  Container,
  Group,
  LoadingOverlay,
  PasswordInput,
  Stack,
  TextInput,
} from "@mantine/core";
import { useForm } from "@mantine/form";
import React from "react";
import { UserLoginRequest } from "../../Services/UserClient/userContracts";
import { Link } from "react-router-dom";
import { REGISTER_ROUTE } from "../Routing/Routes";
import { useLoginUser } from "../../Hooks/User/useLoginUser";

type Props = {};

export const Login = (props: Props) => {
  const [response, isLoading, login] = useLoginUser();
  const form = useForm<UserLoginRequest & {
    confirmPassword: string;
  }>({
    initialValues: {
      tz: "",
      password: "",
      confirmPassword: ""
    },
  });


  return (
    <Container>
      <Box
        component="form"
        onSubmit={form.onSubmit((values) => login(values))}
      >
        <LoadingOverlay visible={isLoading} />

        <Stack gap={"md"} p={"md"}>
          <TextInput
            key={form.key("tz")}
            label="תעודת זהות"
            description="9 ספרות בלבד"
            {...form.getInputProps("tz")}
          ></TextInput>

          <PasswordInput
            key={form.key("password")}
            label="סיסמא"
            {...form.getInputProps("password")}
          ></PasswordInput>

          <Group>
            <Button type="submit">התחבר</Button>
            <Button variant="light" component={Link} to={REGISTER_ROUTE}>
              הרשמה
            </Button>
          </Group>
        </Stack>
      </Box>
    </Container>
  );
};
