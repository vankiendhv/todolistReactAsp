import axiosClient from "./axiosClient";
class SendMailApi {
    sendmail = (params) => {
        const url = `/sendMail`;
        return axiosClient.put(url, {
            From: params.from,
            Subject: params.subject,
            To: params.to,
            Gmail: params.gmail,
            Password: params.password,
            Body: params.body,
        });
    };
}
const sendMailApi = new SendMailApi();
export default sendMailApi;
