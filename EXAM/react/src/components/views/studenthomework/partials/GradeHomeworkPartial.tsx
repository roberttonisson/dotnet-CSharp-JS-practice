import React, { useState, useEffect, FormEvent, ChangeEvent, useCallback, useContext } from "react";
import { BaseService } from "../../../../base/BaseService";
import { IStudentHomework } from "../../../../domain/IStudentHomework";
import { AppContext } from "../../../../context/AppContext";


interface IPropss {
    item: IStudentHomework
}

const GradeHomework = (stuff: IPropss) => {
    const [state, setState] = useState({id: stuff.item.id, grade: stuff.item.grade, studentAnswer: stuff.item.studentAnswer} as IStudentHomework);

    const appContext = useContext(AppContext);
    const handleChange = (target: EventTarget & HTMLSelectElement) => {
        setState({ ...state, [target.name]: target.value });
    }

    const handleSubmit = async () => {
        console.log(state.grade)
        await BaseService.updateEntity<IStudentHomework>(state, 'StudentHomework/PutStudentHomework', appContext.jwt!)
        return alert("changes saved");
    }

    useEffect(() => {
        setState({...state, ...stuff.item})
    }, [stuff.item]);

    useEffect(() => {
    }, [state.grade]);


    let grades = ['0', '1', '2', '3', '4', '5']

    return (<>

            {/* <input type="number" min="0" max="5" value={state.grade} name="grade" onChange={(e) => handleChange(e.target)} /> */}
            <select value={state.grade} name="grade" onChange={(e) => handleChange(e.target)}>
                {grades.map(g => <option value={g}>{g}</option>)}
            </select>
            <button onClick={() => handleSubmit()} type="submit">Submit</button>

    </>);
}

export default GradeHomework;