import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import notificationApi from "../../Api/notificationApi";
import { loadHeader, NumberJobNoActive } from "../function";
import { login, notification } from "../icons/iconSvg";
import Notification from "./Notification/Notification";
export default function Header({ loadName }) {
    const [showModal, setshowModal] = useState(false);
    const [initSelect, setinitSelect] = useState("none");
    const [numNotification, setNumNotification] = useState(0);
    const [load, setLoad] = useState(false);
    useEffect(() => {
        setTimeout(() => {
            notificationApi
                .getNUmberNotificationNoActive(
                    localStorage.getItem("loginTodolist")
                )
                .then((data) => {
                    setNumNotification(NumberJobNoActive(data));
                });
        }, 200);
    }, [load]);
    const onLoad = (e) => {
        setLoad(e);
    };
    loadHeader(load, onLoad);
    return (
        <div className="header-nav">
            <div className="notification">
                <div
                    className="icon"
                    title="Thông báo"
                    onClick={() => {
                        setshowModal(!showModal);
                        setinitSelect("flex");
                    }}
                >
                    {notification}
                    {numNotification === 0 ? (
                        ""
                    ) : (
                        <div className="number">
                            {JSON.stringify(numNotification)}
                        </div>
                    )}
                </div>
                <Notification
                    loadNumber={load}
                    onloadNumber={onLoad}
                    showModal={showModal}
                    initSelect={initSelect}
                />
            </div>
            <div className="login">
                <Link
                    to="/login"
                    onClick={() => {
                        localStorage.removeItem("loginTodolist");
                        localStorage.removeItem("nameTodolist");
                    }}
                    title="Login"
                >
                    <div className="icon" style={{ marginRight: "2rem" }}>
                        {login}
                    </div>
                </Link>
            </div>
        </div>
    );
}
