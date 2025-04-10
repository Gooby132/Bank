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
import { UserRegisterRequest } from "../../Services/UserClient/userContracts";
import { LOGIN_ROUTE } from "../Routing/Routes";
import { Link } from "react-router-dom";
import { DateInput } from "@mantine/dates";
import { useRegisterUser } from "../../Hooks/User/useRegisterUser";
import { rules } from "../../rules";

type Props = {};

export const Register = (props: Props) => {
  const [response, isLoading, register] = useRegisterUser();

  const form = useForm<UserRegisterRequest & { confirmPassword: string }>({
    initialValues: {
      user: {
        tz: "",
        fullName: "",
        englishFullName: "",
        birthDateInUtc: "",
        account: { currency: 0, id: 0, operations: [] },
      },
      password: "",
      confirmPassword: "",
    },
    validate: {
      user: {
        fullName: rules.user.fullName,
        englishFullName: rules.user.englishFullName,
        tz: rules.user.tz,
      },
      password: (value, values) =>
        rules.user.password(value, values.confirmPassword),
    },
  });

  return (
    <Container>
      <Box
        component="form"
        onSubmit={form.onSubmit((values) => register(values))}
      >
        <LoadingOverlay visible={isLoading} />
        <Stack gap={"md"} p={"md"}>
          <TextInput
            key={form.key("user.tz")}
            label="תעודת זהות"
            description="9 ספרות בלבד"
            {...form.getInputProps("user.tz")}
          ></TextInput>

          <TextInput
            key={form.key("user.fullName")}
            label="שם מלא"
            description="20 תווים עברית בלבד, תווים בלבד, ללא סימנים מיוחדים וספרות."
            {...form.getInputProps("user.fullName")}
          />

          <TextInput
            key={form.key("user.englishFullName")}
            label="שם מלא באנגלית"
            description="15 תווים, אנגלית בלבד, תווים בלבד, ללא סימנים מיוחדים וספרות. 
"
            {...form.getInputProps("user.englishFullName")}
          />

          <DateInput
            key={form.key("user.birthDateInUtc")}
            label="תאריך לידה"
            maxDate={new Date()}
            {...form.getInputProps("user.birthDateInUtc")}
          />

          <PasswordInput
            key={form.key("password")}
            label="סיסמא"
            {...form.getInputProps("password")}
          />
          <PasswordInput
            key={form.key("confirmPassword")}
            label="אימות סיסמא"
            {...form.getInputProps("confirmPassword")}
          />
          <Group>
            <Button type="submit">הרשמה</Button>
            <Button variant="light" component={Link} to={LOGIN_ROUTE}>
              התחברות
            </Button>
          </Group>
        </Stack>
      </Box>
    </Container>
  );
};
