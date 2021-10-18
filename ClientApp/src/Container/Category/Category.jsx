import React, { useState, useEffect } from "react";
import Select from "react-select";
import categoryApi from "../../Api/categoryApi";
import jobApi from "../../Api/jobApi";
import tagJobApi from "../../Api/tagJobApi";
import {
    checkTime,
    getTagId,
    getTagIdEdit,
    getTagObject,
    messageShowErr,
    messageShowSuccess,
} from "../function";
import { storage } from "../../firebase";
import Spinner from "../Spinner/Spinner";
import { deletes, edit, fileIcon } from "../icons/iconSvg";
export default function Category({ categoryIdLoadPage, tagIdLoadPage }) {
    const [addJob, setAddJob] = useState(false);
    const [editJob, setEditJob] = useState(false);
    const [state, setState] = useState({
        nameFile: "",
        file: "",
        fileEdit: "",
        name: "",
        tagId: "",
        idJob: "",
        time: "",
        categoryid: "",
        userid: localStorage.getItem("userId"),
        important: 0,
        load: false,
    });
    const {
        name,
        time,
        categoryid,
        load,
        userid,
        tagId,
        important,
        idJob,
        nameFile,
        file,
        fileEdit,
    } = state;
    const [category, setCategory] = useState(null);
    const [tag, setTag] = useState(null);
    const [loadingSpinner, setloadingSpinner] = useState(false);
    const [job, setJob] = useState(null);
    const [valueCategory, setValueCategory] = useState([]);
    const [valueTag, setValueTag] = useState([]);
    const hangdleOnChange = (e) => {
        const { name, value } = e.target;
        setState({
            ...state,
            [name]: value,
        });
    };
    const hangdleOnChangeCheckbox = (e) => {
        const { name } = e.target;
        setState({
            ...state,
            [name]: important === 0 ? 1 : 0,
        });
    };
    useEffect(() => {
        setTimeout(() => {
            jobApi
                .getAll({
                    id: localStorage.getItem("userId"),
                    tag: tagIdLoadPage,
                    category: categoryIdLoadPage,
                })
                .then((data) => setJob(data));
        }, 300);
    }, [load, tagIdLoadPage, categoryIdLoadPage]);
    useEffect(() => {
        categoryApi.getAll().then((data) => {
            setCategory(data);
        });
        categoryApi.getTag().then((data) => {
            setTag(data);
        });
    }, []);
    const hangldeSubmit = async (e) => {
        e.preventDefault();
        if (
            time === "" ||
            name === "" ||
            categoryid === "" ||
            tagId === "" ||
            tagId.length === 0
        ) {
            messageShowErr("chưa nhập đủ thông tin!");
        } else {
            if (!checkTime(time)) {
                messageShowErr("Ngày tháng không được quá hạn!");
            } else {
                setloadingSpinner(true);
                if (!editJob) {
                    let file1 = "";
                    if (file === "") {
                        file1 = "";
                    } else {
                        await storage.ref(`fileJob/${file.name}`).put(file);
                        file1 = await storage
                            .ref("fileJob")
                            .child(nameFile)
                            .getDownloadURL();
                    }
                    jobApi
                        .postJob({
                            name,
                            time,
                            categoryid,
                            userid,
                            file: file1,
                            important,
                        })
                        .then((data) => {
                            if (data) {
                                let idJob = data.id;
                                for (let i = 0; i < tagId.length; i++) {
                                    const element = tagId[i];
                                    tagJobApi.postTagJob({
                                        TagId: element,
                                        JobId: idJob,
                                    });
                                }
                                messageShowSuccess(
                                    "Thêm công việc thành công!"
                                );
                                setState({
                                    ...state,
                                    load: !load,
                                    name: "",
                                    time: "",
                                    nameFile: "",
                                    file: "",
                                    important: 0,
                                });
                                setAddJob(!addJob);
                                setloadingSpinner(false);
                            }
                        });
                } else {
                    let file1 = "";
                    if (file !== "") {
                        await storage.ref(`fileJob/${file.name}`).put(file);
                        file1 = await storage
                            .ref("fileJob")
                            .child(nameFile)
                            .getDownloadURL();
                    } else {
                        file1 = fileEdit;
                    }
                    jobApi.putJob({
                        id: idJob,
                        name,
                        time,
                        file: file1,
                        categoryid,
                        important,
                    });
                    tagJobApi.delateTag(idJob);
                    setTimeout(() => {
                        for (let i = 0; i < tagId.length; i++) {
                            const element = tagId[i];
                            tagJobApi.postTagJob({
                                TagId: element,
                                JobId: idJob,
                            });
                        }
                    }, 200);
                    setTimeout(() => {
                        messageShowSuccess("Sửa công việc thành công!");
                        setState({ ...state, load: !load });
                        setloadingSpinner(false);
                    }, 500);
                }
            }
        }
    };
    const hangldeAddJob = () => {
        setAddJob(!addJob);
        setState({
            ...state,
            name: "",
            time: "",
            nameFile: "",
            file: "",
            categoryid: "",
            important: 0,
            tagId: "",
        });
        setValueTag([]);
        setValueCategory([]);
        setEditJob(false);
    };
    const optionsTag = (tag) => {
        let arr = [];
        for (let i = 0; i < tag.length; i++) {
            const element = tag[i];
            arr.push({ value: element.id, label: element.name });
        }
        return arr;
    };
    const optionsCategory = (category) => {
        let arr = [];
        for (let i = 0; i < category.length; i++) {
            const element = category[i];
            arr.push({ value: element.id, label: element.name });
        }
        return arr;
    };
    const onchangeCategory = (e) => {
        setState({
            ...state,
            categoryid: e.value,
        });
        setValueCategory(e);
    };
    const onchangeTag = (e) => {
        let arr = [];
        for (let i = 0; i < e.length; i++) {
            const element = e[i].value;
            arr.push(element);
        }
        setState({
            ...state,
            tagId: arr,
        });
        setValueTag(e);
    };
    const hangldeDelete = (e) => {
        jobApi.deletejob(e).then((data) => {
            if (data === "Success") {
                messageShowSuccess("Xoá thành công!");
                setState({ ...state, load: !load });
            }
        });
    };
    const hangdleEdit = (e) => {
        setEditJob(true);
        jobApi.getOne(e).then((data) => {
            let ok = data[0];
            setState({
                ...state,
                name: ok.name,
                time: ok.time,
                idJob: e,
                fileEdit: ok.file,
                tagId: getTagIdEdit(ok.tag),
                important: ok.important,
                categoryid: ok.categoryId,
            });
            setAddJob(true);
            const dataCategory = category.filter(
                (state) => state.id === ok.categoryId
            );
            setValueCategory({
                value: dataCategory[0].id,
                label: dataCategory[0].name,
            });
        });
        tagJobApi.getAll(e).then((data) => {
            setValueTag(getTagObject(tag, getTagId(data)));
        });
    };
    const hangdleEditImportant = (id, valueImportant) => {
        jobApi
            .putJobImportant({ id, important: valueImportant === 0 ? 1 : 0 })
            .then((data) => {
                if (data === "Success") {
                    messageShowSuccess("Sửa việc quan trọng thành công!");
                    setState({ ...SVGPatternElement, load: !load });
                }
            });
    };
    const hangdleOnchangeFile = (e) => {
        setState({
            ...state,
            nameFile: e.target.files[0].name,
            file: e.target.files[0],
        });
    };
    const chuyentrang = (url) => {
        window.open(url);
    };
    return (
        <div className="category">
            {loadingSpinner ? <Spinner /> : ""}

            <div className="header-job">
                <h3>Danh sách công việc</h3>
                <div className="hr"></div>
            </div>
            <div className="container">
                <div className="btn-add">
                    <button onClick={hangldeAddJob}>
                        {addJob ? "Huỷ bỏ" : "Thêm công việc"}{" "}
                    </button>
                </div>
                {addJob ? (
                    <div className="form-submit">
                        <form action="" onSubmit={hangldeSubmit}>
                            <div className="btn-form-input">
                                <div>
                                    <label htmlFor="">Tên công việc</label>
                                </div>
                                <input
                                    type="text"
                                    value={name}
                                    name="name"
                                    onChange={hangdleOnChange}
                                />
                            </div>
                            <div className="btn-form-input">
                                <div>
                                    <label htmlFor="">Thời gian</label>
                                </div>
                                <input
                                    type="date"
                                    value={time}
                                    name="time"
                                    onChange={hangdleOnChange}
                                />
                            </div>
                            <div className="btn-form-input">
                                <div>
                                    <label htmlFor="">Thẻ</label>
                                </div>
                                <Select
                                    isMulti
                                    value={valueTag}
                                    closeMenuOnSelect={false}
                                    onChange={onchangeTag}
                                    options={optionsTag(tag)}
                                />
                            </div>
                            <div className="btn-form-input">
                                <div>
                                    <label htmlFor="">Danh mục</label>
                                </div>
                                <Select
                                    value={valueCategory}
                                    onChange={onchangeCategory}
                                    options={optionsCategory(category)}
                                />
                            </div>
                            <div className="btn-form-input-file">
                                <label htmlFor="">Đính kèm file</label>
                                <div className="file">
                                    <label htmlFor="ok">
                                        <div className="icon">{fileIcon}</div>
                                    </label>
                                    <input
                                        type="file"
                                        onChange={hangdleOnchangeFile}
                                        hidden
                                        name="time"
                                        id="ok"
                                    />
                                    {nameFile ? nameFile : ""}
                                </div>
                            </div>
                            <div className="btn-form-input-checkbox">
                                <label htmlFor="checkOk">Việc quan trọng</label>
                                <input
                                    type="checkbox"
                                    checked={important === 0 ? false : true}
                                    name="important"
                                    id="checkOk"
                                    onChange={hangdleOnChangeCheckbox}
                                />
                            </div>
                            <div className="submit">
                                <button type="submit">
                                    {!editJob ? "Thêm mới" : "Sửa công việc"}
                                </button>
                            </div>
                        </form>
                    </div>
                ) : (
                    ""
                )}
                <table>
                    <thead>
                        <tr className="header">
                            <th>
                                <div className="title-header">Công việc</div>
                            </th>
                            <th>
                                <div className="title-header">Thời gian</div>
                            </th>
                            <th>
                                <div className="title-header">
                                    Thẻ công việc
                                </div>
                            </th>
                            <th>
                                <div className="title-header">Quan trọng</div>
                            </th>
                            <th>
                                <div className="title-header">File</div>
                            </th>
                            <th>
                                <div className="title-header">Action</div>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {job?.map((data) => (
                            <tr className="content" key={data.id}>
                                <td>{data.name}</td>
                                <td
                                    className={`${
                                        !checkTime(data.time)
                                            ? "color-red"
                                            : "color-green"
                                    }`}
                                >
                                    {data.time}
                                </td>
                                <td>
                                    <div className="tags">
                                        {data.tag?.map((ok, index) => (
                                            <div
                                                key={index}
                                                className="tag"
                                                style={{ background: ok.color }}
                                            >
                                                {ok.name}
                                            </div>
                                        ))}
                                    </div>
                                </td>
                                <td className="important">
                                    <input
                                        type="checkbox"
                                        readOnly
                                        onClick={() =>
                                            hangdleEditImportant(
                                                data.id,
                                                data.important
                                            )
                                        }
                                        checked={
                                            data.important === 0 ? false : true
                                        }
                                        className="checkbox"
                                    />
                                </td>
                                <td style={{ padding: 0 }}>
                                    {data.file ? (
                                        <div
                                            className="icon"
                                            onClick={() =>
                                                chuyentrang(data.file)
                                            }
                                        >
                                            {data.file ? fileIcon : ""}
                                        </div>
                                    ) : (
                                        ""
                                    )}
                                </td>
                                <td className="action">
                                    <div
                                        style={{
                                            display: "flex",
                                            justifyContent: "center",
                                        }}
                                    >
                                        <div
                                            className="edit"
                                            onClick={() => hangdleEdit(data.id)}
                                        >
                                            <div className="icon">{edit}</div>
                                        </div>
                                        <div
                                            className="delete"
                                            onClick={() =>
                                                hangldeDelete(data.id)
                                            }
                                        >
                                            <div className="icon">
                                                {deletes}
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
