import React, { useEffect, useState } from "react";
import Menu from "../Menu/Menu.jsx";
import { Switch, Route, Link } from "react-router-dom";
import Category from "../Category/Category";
import Login from "../Login/Login";
import Register from "../Register/Register";
import notificationApi from "../../Api/notificationApi";
import jobApi from "../../Api/jobApi";
import userApi from "../../Api/userApi.jsx";
import VerifyEmail from "../VerifyEmail/VerifyEmail.jsx";
import firebase from "firebase";
import tokenNotificationApi from "../../Api/tokenNotificationApi.js";
export default function Content() {
    const [load, setLoad] = useState(false);
    const [user, setUser] = useState(false);
    const [userId, setUserId] = useState(localStorage.getItem("userId"));
    const [categoryIdLoadPage, setCategoryIdLoadPage] = useState(0);
    const [tagIdLoadPage, setTagIdLoadPage] = useState(0);
    useEffect(() => {
        if (localStorage.getItem("tokenTodolist")) {
            userApi.checkUser().then((data) => {
                if (data === "Success") {
                    setUser(true);

                    const messaging = firebase.messaging();
                    messaging
                        .requestPermission()
                        .then(() => {
                            return messaging.getToken();
                        })
                        .then((token) => {
                            localStorage.setItem("tokenDrive", token);
                            if (token && userId) {
                                tokenNotificationApi.postTokenNotification({
                                    token,
                                    userId,
                                });
                            }
                        })
                        .catch((Err) => {
                            console.log("oki");
                            console.log(Err);
                        });
                }
            });
        } else {
            setUser(false);
        }
        // if (localStorage.getItem("loginTodolist")) {
        // } else {
        //     setUser(false);
        // }
    }, [load]);
    const getJobDate = async () => {
        return await jobApi
            .getJobDate(localStorage.getItem("loginTodolist"))
            .then((data) => {
                return data;
            });
    };
    const getNotification = async () => {
        return await notificationApi
            .getAll(localStorage.getItem("loginTodolist"))
            .then((data) => {
                return data;
            });
    };

    useEffect(() => {
        // Promise.all([getJobDate(), getNotification()]).then(function (data) {
        //     checkNotification(data);
        // });
    }, []);
    const onchangeDataCategory = (e) => {
        setCategoryIdLoadPage(e);
    };
    const onchangeDataTag = (e) => {
        setTagIdLoadPage(e);
    };
    const onloads = (e) => {
        setLoad(e);
    };
    return (
        <div>
            <Menu
                onchangeCategory={onchangeDataCategory}
                onchangeTag={onchangeDataTag}
            />
            <div className="content">
                <Switch>
                    <Route exact path="/login">
                        <Login load={load} onload={onloads} />
                    </Route>
                    <Route exact path="/verifyEmail" component={VerifyEmail} />
                    <Route
                        exact
                        path="/"
                        render={() => {
                            return user ? (
                                <Category
                                    categoryIdLoadPage={categoryIdLoadPage}
                                    tagIdLoadPage={tagIdLoadPage}
                                />
                            ) : (
                                <Login load={load} onload={onloads} />
                            );
                        }}
                    />
                    <Route exact path="/register">
                        <Register />
                    </Route>
                </Switch>
            </div>
        </div>
    );
}
