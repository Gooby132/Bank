import { Button, Center, Container, Stack, Text } from "@mantine/core";
import { Link } from "react-router-dom";
import { LOGIN_ROUTE, REGISTER_ROUTE } from "../Routing/Routes";

type Props = {};

export const Landing = (props: Props) => {
  return (
    <Container>
      <Center>
        <Stack gap={"md"}>
          <Text>ברוכים הבאים</Text>
          <Button component={Link} to={LOGIN_ROUTE}>
            התחברות
          </Button>
          <Button component={Link} to={REGISTER_ROUTE}>
            הרשמה
          </Button>
        </Stack>
      </Center>
    </Container>
  );
};
