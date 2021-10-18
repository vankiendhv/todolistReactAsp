import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";
import userApi from "../../Api/userApi";
import { messageShowErr, messageShowSuccess } from "../function";
export default function Login({ load, onload }) {
    const [UserName, setUserName] = useState("");
    const [Password, setPassword] = useState("");
    const history = useHistory();

    const hangdldeSubmit = (e) => {
        e.preventDefault();
        userApi.login({ UserName, Password }).then((data) => {
            if (data === "Invalid") {
                messageShowErr("Tài khoản hoặc mật khẩu không chính xác!");
            } else if (data === "Lock") {
                messageShowErr(
                    "Bạn nhập sai quá nhiều lần tài khoản hiện đã bị khoá!"
                );
            } else {
                messageShowSuccess("Đăng nhập thành công!");
                localStorage.setItem("tokenTodolist", data.accessToken);
                localStorage.setItem("userId", data.id);
                setTimeout(() => {
                    onload(!load);
                    history.push("/");
                }, 200);
            }
        });
    };
    return (
        <div className="login-form">
            <div className="header-job">
                <h3>Đăng nhập</h3>
                <div className="hr"></div>
            </div>
            <div className="content-login">
                <form action="" onSubmit={hangdldeSubmit}>
                    <div className="btn-form-input-login">
                        <div>
                            <label htmlFor="">Tài khoản</label>
                        </div>
                        <input
                            type="text"
                            value={UserName}
                            onChange={(e) => {
                                setUserName(e.target.value);
                            }}
                        />
                    </div>
                    <div className="btn-form-input-login">
                        <div>
                            <label htmlFor="">Mật khẩu</label>
                        </div>
                        <input
                            type="password"
                            value={Password}
                            onChange={(e) => {
                                setPassword(e.target.value);
                            }}
                        />
                    </div>
                    <div className="register">
                        <Link to="/register">Đăng ký</Link>
                    </div>
                    <div className="btn">
                        <button>Đăng nhập</button>
                    </div>
                </form>
            </div>
        </div>
    );
}
