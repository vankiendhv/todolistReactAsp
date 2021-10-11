import axiosClient from "./axiosClient";
class TagJobApi {
    getAll = (id) => {
        const url = `/tagJobs/${id}`;
        console.log(id);
        return axiosClient.get(url);
    };
    delateTag = (id) => {
        const url = `/tagJobs/${id}`;
        return axiosClient.delete(url);
    };
    postTagJob = (params) => {
        const url = "/tagJobs";
        return axiosClient.post(url, {
            JobId: params.JobId,
            TagId: params.TagId,
        });
    };
}
const tagJobApi = new TagJobApi();
export default tagJobApi;
