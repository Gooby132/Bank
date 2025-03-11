import {
  Box,
  Button,
  Combobox,
  Container,
  Group,
  LoadingOverlay,
  ScrollArea,
  Stack,
  Text,
  TextInput,
  Title,
  useCombobox,
} from "@mantine/core";
import { useSelector } from "react-redux";
import { RootState } from "../../Redux/store";
import { useForm } from "@mantine/form";
import { useMakeTransaction } from "../../Hooks/User/useMakeTransaction";
import { UserMakeTransactionRequest } from "../../Services/UserClient/userContracts";
import { useNavigate } from "react-router-dom";
import { LOGIN_ROUTE } from "../Routing/Routes";

type Props = {};

export const Personal = (props: Props) => {
  const navigate = useNavigate();
  const operationTypes = ["הפקדה", "משיכה"];
  const user = useSelector((state: RootState) => state.userSlice).user;
  const form = useForm<UserMakeTransactionRequest>({
    initialValues: {
      sendTo: 0,
      amount: 0,
      operationType: 0,
      user: {
        tz: "",
        fullName: "",
        englishFullName: "",
        birthDateInUtc: "",
        account: { currency: 0, id: 0, operations: [] },
        ...user,
      },
    },
  });
  const combobox = useCombobox();
  const [_, isLoading, makeTransaction] = useMakeTransaction();

  const options = operationTypes.map((value, index) => (
    <Combobox.Option key={index} value={index.toString()}>
      {value}
    </Combobox.Option>
  ));

  if (!user) {
    navigate(`/${LOGIN_ROUTE}`);
    return <></>;
  }

  return (
    <Container>
      <Stack gap={"md"}>
        <Title order={4}>איזור אישי</Title>

        <Group>
          <Text>שלום</Text>
          <Text>{user?.fullName}</Text>
        </Group>

        {/* New Transaction */}
        <Title order={5}>העברת כסף</Title>
        <Group>
          <Text>סכום בחשבון</Text>
          <Text>{user.account.currency}$</Text>
        </Group>
        <Box
          component="form"
          onSubmit={form.onSubmit((values) => makeTransaction(values))}
        >
          <LoadingOverlay visible={isLoading} />

          <Stack gap={"md"} p={"md"}>
            <TextInput
              key={form.key("sendTo")}
              label="העבר לחשבון"
              {...form.getInputProps("sendTo")}
            />

            <TextInput
              key={form.key("amount")}
              label="סכום"
              {...form.getInputProps("amount")}
            />

            <Combobox
              store={combobox}
              onOptionSubmit={(value) => {
                form.setFieldValue("operationType", Number.parseInt(value));
                combobox.closeDropdown();
              }}
            >
              <Combobox.Target>
                <Button
                  variant="light"
                  onClick={() => combobox.toggleDropdown()}
                >
                  {operationTypes[form.values.operationType]}
                </Button>
              </Combobox.Target>

              <Combobox.Dropdown>
                <Combobox.Options>{options}</Combobox.Options>
              </Combobox.Dropdown>
            </Combobox>
          </Stack>

          <Button color="green" type="submit">
            שלח
          </Button>
        </Box>

        {/* History Table */}
        <Title order={5}>היסטוריית פעולות</Title>

        <Stack>
          <ScrollArea scrollbars="y" h={200}>
            {user!.account.operations.map((operation, index) => (
              <Group key={index}>
                <Text>סוג: {operationTypes[operation.type]}</Text>
                <Text>סכום: {operation.value}</Text>
                <Text>לחשבון: {operation.sentTo}</Text>
              </Group>
            ))}
          </ScrollArea>
        </Stack>
      </Stack>
    </Container>
  );
};
