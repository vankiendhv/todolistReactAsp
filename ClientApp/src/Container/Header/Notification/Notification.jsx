import React, { useEffect, useState } from "react";
import notificationApi from "../../../Api/notificationApi";

export default function Notification({
    loadNumber,
    onloadNumber,
    showModal,
    initSelect,
}) {
    const [data, setData] = useState(null);
    const [load, setLoad] = useState(false);
    useEffect(() => {
        notificationApi
            .getAll(localStorage.getItem("loginTodolist"))
            .then((ok) => {
                setData(ok);
            });
    }, [loadNumber]);
    const onclickNotification = (e, n) => {
        if (n === 0) {
            notificationApi.putNotification({ id: e, status: 1 });
        }
        setTimeout(() => {
            setLoad(!load);
            onloadNumber(!loadNumber);
        }, 300);
    };
    return (
        <div
            className={`notification-select ${
                !showModal ? "hident-select" : ""
            }`}
            style={{ display: initSelect }}
        >
            <ul>
                {data?.map((ok) => (
                    <li
                        key={ok.id}
                        className={`${ok.status === 0 ? "active" : ""}`}
                        onClick={() => onclickNotification(ok.id, ok.status)}
                    >
                        {ok.content}
                    </li>
                ))}
            </ul>
        </div>
    );
}
