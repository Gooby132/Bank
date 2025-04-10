import { FaUser } from "react-icons/fa";
import { CiLogin, CiLogout } from "react-icons/ci";
import { darkTheme } from "../../Themes/themes";
import {
  AppShell,
  Button,
  DirectionProvider,
  Group,
  MantineProvider,
  Text,
} from "@mantine/core";
import { Notifications } from "@mantine/notifications";
import { Link, Outlet } from "react-router-dom";
import { CiBank } from "react-icons/ci";
import { LANDING_ROUTE, LOGIN_ROUTE, PERSONAL_ROUTE } from "../Routing/Routes";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../../Redux/store";
import { userActions } from "../../Redux/features/User/userSlice";

export const App = () => {
  const HEADER_SIZE = 64;
  const dispatcher = useDispatch();
  const user = useSelector((state: RootState) => state.userSlice);

  return (
    <DirectionProvider initialDirection="rtl">
      <MantineProvider theme={darkTheme}>
        <Notifications />

        <AppShell
          header={{
            height: HEADER_SIZE,
          }}
        >
          <AppShell.Header>
            <Group p={10} justify="space-between" h={HEADER_SIZE}>
              <Button component={Link} to={LANDING_ROUTE}>
                <CiBank />
                <Text ps={8}>Bank</Text>
              </Button>
              {user.user && (
                <Group>
                  <Button component={Link} to={PERSONAL_ROUTE}>
                    <FaUser />
                  </Button>
                  <Button onClick={() => {dispatcher(userActions.logoutUser())}}>
                    <CiLogout />
                  </Button>
                </Group>
              )}
              {!user.user && (
                <Button component={Link} to={LOGIN_ROUTE}>
                  <CiLogin />
                </Button>
              )}
            </Group>
          </AppShell.Header>
          <AppShell.Main>
            <Outlet />
          </AppShell.Main>
        </AppShell>
      </MantineProvider>
    </DirectionProvider>
  );
};
