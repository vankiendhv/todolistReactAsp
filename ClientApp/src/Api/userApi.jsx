import axiosClient from "./axiosClient";
class UserApi {
    getAll = (params) => {
        const url = "/users";
        return axiosClient.get(url, { params });
    };
    logOut = () => {
        const url = "/users/logout";
        return axiosClient.get(url);
    };
    login = (params) => {
        const url = `users/login`;
        return axiosClient.post(url, {
            Email: params.UserName,
            Password: params.Password,
        });
    };
    getOne = (params) => {
        const url = `/users/${params}`;
        return axiosClient.get(url).then((data) => {
            return data.data;
        });
    };
    postUser = (params) => {
        const url = "/users";
        return axiosClient.post(url, {
            Name: params.Name,
            Email: params.UserName,
            Password: params.Password,
        });
    };
    checkUser = () => {
        const url = "/users/checkUser";
        return axiosClient.get(url);
    };
    verifyEmailUser = (params) => {
        const url = `/users/verifyEmail`;
        return axiosClient.post(url, {
            userFe: `${params.userId}`,
            codeFe: params.code,
        });
    };
}
const userApi = new UserApi();
export default userApi;
