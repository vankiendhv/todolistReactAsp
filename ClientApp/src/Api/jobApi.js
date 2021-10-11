import axiosClient from "./axiosClient";
class JobApi {
    getAll = (params) => {
        const url = `/jobs/${params.id}/${params.category}/${params.tag}`;
        return axiosClient.get(url);
    };
    getOne = (id) => {
        const url = `/jobs/getOne/${id}`;
        return axiosClient.get(url);
    };
    getJobDate = (id) => {
        const url = `/jobs/getJobToday/${id}`;
        return axiosClient.get(url);
    };
    postJob = (params) => {
        const url = "/jobs";

        return axiosClient.post(url, {
            Name: params.name,
            Time: params.time,
            File: params.file,
            CategoryId: params.categoryid,
            UserId: params.userid,
            Important: params.important,
        });
    };
    putJob = (params) => {
        const url = `/jobs/${params.id}`;
        return axiosClient.put(url, {
            Id: params.id,
            Name: params.name,
            Time: params.time,
            File: params.file,
            CategoryId: params.categoryid,
            Important: params.important,
        });
    };
    putJobImportant = (params) => {
        const url = `/jobs/important/${params.id}`;
        return axiosClient.put(url, {
            Id: params.id,
            Important: params.important,
        });
    };
    deletejob = (id) => {
        const url = `/jobs/${id}`;
        return axiosClient.delete(url);
    };
}
const jobApi = new JobApi();
export default jobApi;
