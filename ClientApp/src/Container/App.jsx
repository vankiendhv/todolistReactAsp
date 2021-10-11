import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import "../scss/App.scss";
import Content from "./Content/Content";
import Header from "./Header/Header";
export default function App() {
    return (
        <Router>
            <div className="App">
                <Header />
                <Content />
            </div>
        </Router>
    );
}
