import axiosClient from "./axiosClient";
class UserApi {
    getAll = (params) => {
        const url = "/users";
        return axiosClient.get(url, { params });
    };
    login = (params) => {
        const url = `users/login`;
        return axiosClient.post(url, {
            UserName: params.UserName,
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
        return axiosClient.post(url, params);
    };
}
const userApi = new UserApi();
export default userApi;
