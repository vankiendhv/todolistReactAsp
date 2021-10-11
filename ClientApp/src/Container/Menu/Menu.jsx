import React, { useState, useEffect, useRef } from "react";
import { Link } from "react-router-dom";
import categoryApi from "../../Api/categoryApi";
import { toggleMenu } from "./menujs.js";
export default function Menu({ onchangeCategory, onchangeTag }) {
    const [category, setCategory] = useState(null);
    const [tag, setTag] = useState(null);
    const CategoryEl = useRef(null);
    const TagEl = useRef(null);
    useEffect(() => {
        categoryApi.getAll().then((data) => {
            setCategory(data);
        });
        categoryApi.getTag().then((data) => {
            setTag(data);
        });
        setTimeout(() => {
            toggleMenu(CategoryEl.current, TagEl.current);
        }, 300);
    }, []);

    const onchangeCategorys = (e) => {
        onchangeCategory(e);
    };
    const onchangeTags = (e) => {
        onchangeTag(e);
    };
    return (
        <div className="menu">
            <div className="header">Danh mục công việc</div>
            <ul ref={CategoryEl}>
                {category?.map((data) => (
                    <li key={data.id}>
                        <Link to="" onClick={() => onchangeCategorys(data.id)}>
                            {data.name}
                        </Link>
                    </li>
                ))}
                <li
                    style={{ cursor: "pointer" }}
                    onClick={() => onchangeCategorys(0)}
                    className="active"
                >
                    <Link to="">Tất cả</Link>
                </li>
            </ul>
            <div className="header">Thẻ công việc</div>
            <ul ref={TagEl}>
                {tag?.map((data) => (
                    <li key={data.id}>
                        <Link to="" onClick={() => onchangeTags(data.id)}>
                            {data.name}
                        </Link>
                    </li>
                ))}
                <li
                    style={{ cursor: "pointer" }}
                    onClick={() => onchangeTags(0)}
                    className="active"
                >
                    <Link to="">Tất cả</Link>
                </li>
            </ul>
        </div>
    );
}
