import React from "react";
import { spinner } from "../icons/iconSvg";

export default function Spinner() {
    return (
        <div className="spinner">
            <div className="blur"></div>
            <div className="icon">{spinner}</div>
        </div>
    );
}
