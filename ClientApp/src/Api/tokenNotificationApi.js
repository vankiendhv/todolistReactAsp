import axiosClient from "./axiosClient";
class TokenNotificationApi {
    getAll = () => {
        const url = "/tokenNotifications";
        return axiosClient.get(url);
    };
    postTokenNotification = (params) => {
        const url = "/tokenNotifications";
        return axiosClient.post(url, {
            Token: params.token,
            UserId: parseInt(params.userId),
        });
    };
    deleteTokenNotification = (params) => {
        const url = "/tokenNotifications/delete";
        return axiosClient.post(url, {
            Token: params.token,
            UserId: parseInt(params.userId),
        });
    };
}
const tokenNotificationApi = new TokenNotificationApi();
export default tokenNotificationApi;
