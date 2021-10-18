import Toastify from "toastify-js";
import "toastify-js/src/toastify.css";
import notificationApi from "../Api/notificationApi";
import sendMailApi from "../Api/sendMailApi";
export const messageShowErr = (e) => {
    return Toastify({
        text: e,
        duration: 3000,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        backgroundColor: "linear-gradient(to right, #ffd000, #ff8300)",
        stopOnFocus: true, // Prevents dismissing of toast on hover
        onClick: function () {}, // Callback after click
    }).showToast();
};
export const messageShowSuccess = (e) => {
    return Toastify({
        text: e,
        duration: 3000,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        backgroundColor: "linear-gradient(to right, #00b09b, #96c93d)",
        stopOnFocus: true, // Prevents dismissing of toast on hover
        onClick: function () {}, // Callback after click
    }).showToast();
};
export const getTagId = (e) => {
    let arr = [];
    for (let i = 0; i < e.length; i++) {
        const element = e[i];
        arr.push(element.tagId);
    }
    return arr;
};
export const getTagIdEdit = (e) => {
    var arr = [];
    for (let i = 0; i < e.length; i++) {
        const element = e[i].id;
        arr.push(element);
    }
    return arr;
};
export const getTagObject = (e, n) => {
    let arr = [];
    for (let i = 0; i < n.length; i++) {
        const element = n[i];
        arr.push(e.filter((a) => a.id === element)[0]);
    }
    let newArr = [];
    for (let i = 0; i < arr.length; i++) {
        const element = arr[i];
        newArr.push({ value: element.id, label: element.name });
    }
    return newArr;
};
export const checkTime = (e) => {
    let timeToday = new Date();
    let today =
        timeToday.getFullYear() +
        "-" +
        (timeToday.getMonth() + 1) +
        "-" +
        timeToday.getDate();
    if (new Date(e) >= new Date(today)) {
        return true;
    } else {
        return false;
    }
};
export const checkEmail = (e) => {
    var filter =
        /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(e)) {
        return false;
    } else {
        return true;
    }
};
const getJobImportant = (e) => {
    let important = 0;
    let notImportant = 0;
    for (let i = 0; i < e.length; i++) {
        const element = e[i];
        if (element.important === 1) {
            important += 1;
        } else {
            notImportant += 1;
        }
    }
    return [important, notImportant];
};
export const NumberJobNoActive = (e) => {
    return e.length;
};
// let timeReset = "9:50:0";
// export const loadNotification = (load, onload) => {
//     setInterval(() => {
//         let timeToday = new Date();
//         let time =
//             timeToday.getHours() +
//             ":" +
//             timeToday.getMinutes() +
//             ":" +
//             timeToday.getSeconds();

//         if (time == timeReset) {
//             setTimeout(() => {
//                 onload(!load);
//             }, 200);
//         }
//     }, 1000);
// };
// export const loadHeader = (load, onload) => {
//     setInterval(() => {
//         let timeToday = new Date();
//         let time =
//             timeToday.getHours() +
//             ":" +
//             timeToday.getMinutes() +
//             ":" +
//             timeToday.getSeconds();
//         if (time == timeReset) {
//             setTimeout(() => {
//                 onload(!load);
//             }, 200);
//         }
//     }, 1000);
// };
// export const checkNotification = (e) => {
//     let NumberJob = NumberJobNoActive(e[0]);
//     let jobImportant = getJobImportant(e[0])[0];
//     let jobNotImportant = getJobImportant(e[0])[1];
//     setInterval(() => {
//         let timeToday = new Date();
//         let time =
//             timeToday.getHours() +
//             ":" +
//             timeToday.getMinutes() +
//             ":" +
//             timeToday.getSeconds();
//         if (time == timeReset) {
//             notificationApi.postNotification({
//                 content: `Hôm nay bạn có ${NumberJob} việc, ${jobImportant} quan trọng và ${jobNotImportant} không quan trọng`,
//                 status: 0,
//                 userId: localStorage.getItem("loginTodolist"),
//             });
//             sendMailApi.sendmail({
//                 from: "vankienars98@gmail.com",
//                 to: "chjkien9x@gmail.com",
//                 subject: "Thông báo việc làm hôm nay",
//                 gmail: "vankienars98@gmail.com",
//                 password: "vankienars98",
//                 body: `Hôm nay bạn có ${NumberJob} việc, ${jobImportant} quan trọng và ${jobNotImportant} không quan trọng. Truy cập ngay website để biết thêm chi tiết: http://localhost:5000`,
//             });
//         }
//     }, 1000);
// };
