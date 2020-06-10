import React, { useState, useEffect, FormEvent, ChangeEvent, useCallback, useContext } from "react";
import { IStudentCourse } from "../../../../domain/IStudentCourseDTO";
import { BaseService } from "../../../../base/BaseService";
import { AppContext } from "../../../../context/AppContext";


interface IProps {
    item: IStudentCourse
}

const GradeCourse = (stuff: IProps) => {
    const [state, setState] = useState(stuff.item as IStudentCourse);

    const appContext = useContext(AppContext);
    const handleChange = (target: EventTarget & HTMLSelectElement) => {
        setState({ ...state, [target.name]: target.value });
    }

    const handleSubmit = async () => {
        await BaseService.updateEntity<IStudentCourse>(state, 'studentcourse/edit', appContext.jwt!)
        return alert("changes saved");
    }

    let grades = ['0', '1', '2', '3', '4', '5']

    return (<>
{/* 
            <input type="number" min="0" max="5" value={state.grade} name="grade" onChange={(e) => handleChange(e.target)} /> */}
            <select value={state.grade} name="grade" onChange={(e) => handleChange(e.target)} >
                {grades.map(g => <option value={g}>{g}</option>)}
            </select>
            <button className="btn btn-primary btn-sm" onClick={() => handleSubmit()} type="submit">Save</button>

    </>);
}

export default GradeCourse;