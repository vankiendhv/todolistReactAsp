import React, { useState } from "react";
import { Link } from "react-router-dom";
import userApi from "../../Api/userApi";
import { useHistory } from "react-router-dom";
import { checkEmail, messageShowErr, messageShowSuccess } from "../function";

export default function Register() {
    const [state, setState] = useState({
        Name: "",
        UserName: "",
        Password: "",
        loading: false,
    });
    const { Name, UserName, Password, loading } = state;
    const hangdleOnChange = (e) => {
        const { value, name } = e.target;
        setState({
            ...state,
            [name]: value,
        });
    };
    const history = useHistory();
    const hangldeOnSubmit = (e) => {
        e.preventDefault();
        if (Name === "" || UserName === "" || Password === "") {
            messageShowErr("Bạn chưa nhập đầy đủ!");
        } else {
            if (checkEmail(UserName)) {
                setState({ ...state, loading: true });
                userApi.postUser({ Name, UserName, Password }).then((data) => {
                    if (data === "Success") {
                        messageShowSuccess(
                            "Đăng ký thành công vui lòng vào kiểm tra email để tiến hành đăng nhập!"
                        );
                        setState({ ...state, loading: false });
                    } else {
                        messageShowErr("Đăng ký thất bại!");
                        setState({ ...state, loading: false });
                    }
                });
            } else {
                messageShowErr("Email không hợp lệ!");
            }
        }
    };
    return (
        <div className="login-form">
            <div className="header-job">
                <h3>Đăng ký</h3>
                <div className="hr"></div>
            </div>
            <div className="content-login">
                <form action="" onSubmit={hangldeOnSubmit}>
                    <div className="btn-form-input-login">
                        <div>
                            <label htmlFor="">Tên người dùng</label>
                        </div>
                        <input
                            type="text"
                            onChange={hangdleOnChange}
                            name="Name"
                        />
                    </div>
                    <div className="btn-form-input-login">
                        <div>
                            <label htmlFor="">Tài khoản Email</label>
                        </div>
                        <input
                            type="text"
                            onChange={hangdleOnChange}
                            name="UserName"
                        />
                    </div>
                    <div className="btn-form-input-login">
                        <div>
                            <label htmlFor="">Mật khẩu</label>
                        </div>
                        <input
                            type="password"
                            onChange={hangdleOnChange}
                            name="Password"
                        />
                    </div>
                    <div className="register">
                        <Link to="/login">Đăng nhập</Link>
                    </div>
                    {loading ? (
                        <div className="btn">
                            <p>Vui lòng đợi</p>
                        </div>
                    ) : (
                        <div className="btn">
                            <button>Đăng ký</button>
                        </div>
                    )}
                </form>
            </div>
        </div>
    );
}
