import axiosClient from "./axiosClient";
class NotificationApi {
    getAll = (id) => {
        const url = `/notification/${id}`;
        return axiosClient.get(url);
    };
    getNUmberNotificationNoActive = (id) => {
        const url = `/notification/getNumberNoActive/${id}`;
        return axiosClient.get(url);
    };
    postNotification = (params) => {
        const url = "/notification";
        return axiosClient.post(url, {
            Content: params.content,
            Status: params.status,
            userId: params.userId,
        });
    };
    putNotification = (params) => {
        const url = `/notification/${params.id}`;
        return axiosClient.put(url, { Status: params.status });
    };
}
const notificationApi = new NotificationApi();
export default notificationApi;
