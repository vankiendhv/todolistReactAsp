import axiosClient from "./axiosClient";
class CategoryApi {
    getAll = () => {
        const url = "/categories";
        return axiosClient.get(url);
    };
    getTag = () => {
        const url = "/tags";
        return axiosClient.get(url);
    };
}
const categoryApi = new CategoryApi();
export default categoryApi;
