import React, { useState, useContext, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import { IStudentHomework } from "../../../domain/IStudentHomework";
import { BaseService } from "../../../base/BaseService";
import { AppContext } from "../../../context/AppContext";
import jwt_decode from "jwt-decode";

const StudentHwStudentIndex = () => {
    let { id } = useParams();
    const appContext = useContext(AppContext);

    const [item, setItem] = useState({} as IStudentHomework);
    const data = async () => await BaseService
        .getEntity<IStudentHomework>(id, 'studenthomework/GetStudentIndex/' + jwt_decode<any>(appContext.jwt!)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'], appContext.jwt!)
        .then(data => setItem(data!));

    useEffect(() => {
        data();
    }, []);

    useEffect(() => {
    }, [item.studentAnswer]);

    const handleChange = (target: EventTarget & HTMLTextAreaElement) => {
        setItem({ ...item, [target.name]: target.value });
    }

    const handleSubmit = async () => {
        await BaseService
            .createEntity<IStudentHomework>(item, 'studenthomework/PostStudentHomework', appContext.jwt!)
        
        alert('changes saved')
        data()
    }


    function answer() {
        if (item.id != null) {
            return(<>
            <span>You have already submitted an answer: &nbsp;</span>
            <br/>
            <br/>
            <span>{item.studentAnswer}</span>
            </>);
        }

        return (<>
                <div className="form-group">
                    <label>Student answer</label><br/>
                    <textarea name="studentAnswer" value={item.studentAnswer} onChange={(e) => handleChange(e.target)} className="form-control" style={{width: "600px"}}></textarea>
                </div>
                <div className="form-group">
                    <br></br>
                    <br></br>
                    <br></br>
                    <br></br>
                    <button className="btn btn-primary" onClick={() => handleSubmit()}>Submit</button>
                </div>
        </>);
    }

    return (<>
        <h1 style={{paddingLeft: "200px"}}>{item.homework?.title}</h1>

<div style={{paddingLeft: "200px"}}>
    <hr/>
    <dl className="row">
        <dt className="col-sm-2">
            Description
        </dt>
        <dd className="col-sm-10">
            {item.homework?.description}
        </dd>
        <dt className="col-sm-2">
            Grade
        </dt>
        <dd className="col-sm-10">
            {item.grade}
        </dd>
        <br/>
        <br/>
        {answer()}
    </dl>
</div>
{/* <div style={{paddingLeft: "200px"}}>
    <Link to={'/hwstudentindex/'  + item.homework?.courseid}>Back</Link>
</div> */}
    </>);
}

export default StudentHwStudentIndex;