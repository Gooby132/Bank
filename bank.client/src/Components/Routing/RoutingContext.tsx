import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { LANDING_ROUTE, LOGIN_ROUTE, PERSONAL_ROUTE, REGISTER_ROUTE } from "./Routes";
import { Landing } from "../Pages/Landing";
import { Personal } from "../Pages/Personal";
import { PropsWithChildren } from "react";
import { App } from "../App/App";
import { Login } from "../Pages/Login";
import { Register } from "../Pages/Register";

type Props = {};

export const RoutingContext = (props: PropsWithChildren) => {
  const router = createBrowserRouter([
    {
      element: <App />,
      children: [
        {
          path: LANDING_ROUTE,
          element: <Landing />,
        },
        {
          path: PERSONAL_ROUTE,
          element: <Personal />,
        },
        {
          path: LOGIN_ROUTE,
          element: <Login />,
        },
        {
          path: REGISTER_ROUTE,
          element: <Register />,
        },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
};
