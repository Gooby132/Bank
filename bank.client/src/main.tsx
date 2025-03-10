import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "@mantine/core/styles.css";
import '@mantine/dates/styles.css';
import '@mantine/notifications/styles.css';
import { RoutingContext } from "./Components/Routing/RoutingContext";
import { Provider } from "react-redux";
import store from "./Redux/store";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <Provider store={store}>
      <RoutingContext />
    </Provider>
  </StrictMode>
);
