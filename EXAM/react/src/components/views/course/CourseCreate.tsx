import React, { useState, FormEvent, useContext } from "react";
import { BaseService } from "../../../base/BaseService";
import { useHistory } from "react-router-dom";
import { ICourse } from "../../../domain/ICourse";
import { AppContext } from "../../../context/AppContext";


const CourseCreate = () => {
    const history = useHistory();
    const [props, setState] = useState({} as ICourse);
    const appContext = useContext(AppContext);
    const handleChange = (target: EventTarget & HTMLInputElement) => {
        setState({ ...props, [target.name]: target.value });
    }

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {

        BaseService
            .createEntity(props, 'courses', appContext.jwt!)
            .then(data => history.push('/courses'));

        e.preventDefault();
    }

    return (
        <form onSubmit={handleSubmit}>
            <div className="form-group">
                dumbo
                <input value={props.name} name="name" onChange={(e) => handleChange(e.target)} />
            </div>
            <button type="submit">Submit</button>
        </form>
    );
}

export default CourseCreate;