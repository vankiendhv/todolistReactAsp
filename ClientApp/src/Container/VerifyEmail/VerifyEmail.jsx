import React, { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router";
import { Link } from "react-router-dom";
import userApi from "../../Api/userApi";

export default function VerifyEmail() {
    const location = useLocation();
    const [loading, setLoading] = useState(true);
    const [checkEmail, setCheckEmail] = useState(
        <p>
            Xác thực thành công tiến hành đăng nhập tại đây{" "}
            <Link to="/login">Đăng nhập</Link>
        </p>
    );
    let getCode = location.search.split("?code=");
    useEffect(() => {
        userApi
            .verifyEmailUser({ userId: getCode[1], code: getCode[2] })
            .then((data) => {
                if (data === "VerifySuccess") {
                    setLoading(false);
                } else {
                    setLoading(false);
                    setCheckEmail(<p>Xác thực thất bại!</p>);
                }
            });
    }, []);
    return (
        <div className="verify-email">
            {loading ? <p>Đang xác thực</p> : checkEmail}
        </div>
    );
}
