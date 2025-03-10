import { useState } from "react"
import { UserLoginRequest, UserLoginResponse } from "../../Services/UserClient/userContracts"
import { userRestClient } from "../../Services/UserClient/userRest"
import { notifications } from "@mantine/notifications"

export const useLoginUser = () : [
  UserLoginResponse | undefined,
  boolean,
  (request: UserLoginRequest) => Promise<void>
] => {
  const [response, setResponse] = useState<UserLoginResponse>()
  const [isLoading, setIsLoading] = useState(false) 

  const login = async (request: UserLoginRequest) => {
    setResponse(undefined)
    setIsLoading(true)
    
    const res =  await userRestClient.login(request)

    if(res.errors) {
      notifications.show({
        message: "התחברות נכשלה",
        title: "שגיאה",
        color: "red",
      })
    }
    
    setResponse(res)
    setIsLoading(false)
  }

  return [
    response,
    isLoading,
    login
  ]
}