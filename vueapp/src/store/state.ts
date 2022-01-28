
export interface RootState {
    backendPath: string,
    redirectRoute: string,
    redirected: boolean,
    messageVariable: string,
    logged: boolean
}

export const rootState : RootState =  
{
    backendPath: "http://localhost:44305/",
    redirectRoute: "",
    redirected:false,
    messageVariable: "",
    logged: false
}
