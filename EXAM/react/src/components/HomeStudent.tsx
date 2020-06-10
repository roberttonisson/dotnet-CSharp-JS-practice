import React, { useEffect, useState, useContext } from "react";
import { IStudentHomework } from "../domain/IStudentHomework";
import { IStudentCourse } from "../domain/IStudentCourseDTO";
import { AppContext } from "../context/AppContext";
import { BaseService } from "../base/BaseService";
import { Link } from "react-router-dom";
import { IHomeworkDTO } from "../domain/IHomeworkDTO";

interface IState {
    studentCourses: IStudentCourse[];
}

const HomeStudent = () => {
    const [courses, setCourses] = useState({ studentCourses: [] } as IState);
    const appContext = useContext(AppContext);
    const data = async () => await BaseService
        .getSingle<IStudentCourse[]>('studentHomework/HomeIndex/homepage', appContext.jwt!)
        .then(data => setCourses({ ...courses, studentCourses: data! }));

    useEffect(() => {
        data();
    }, []);

    function deadlines(hw: IHomeworkDTO, shw: IStudentHomework[], id: string) {
        if (shw.filter(s => s.appUserId! == id).length < 1) {
            return (         
            <Link to={"/hwstudentdetails/" + hw.id}>{hw.title} - {hw.deadline!}</Link>
            );
        }

        return (
            (<></>)
        );
    }

    function feedback(hw: IHomeworkDTO, shw: IStudentHomework, id: string) {

        if (shw.appUserId == id && shw.grade != null) {
            return (         
            <Link to={"/hwstudentdetails/" + hw.id}>{hw.title} - {shw.grade!}</Link>
            );
        }

        return (
            (<></>)
        );
    }

    function grades(s: IStudentCourse) {
        console.log(s)
        if (s.grade != "") {
            return (         
            <h5>{s.course!.name} - {s.grade}</h5>
            );
        }

        return (
            (<></>)
        );
    }

    return (
        <div className="container">
            <h1 className="text-center">Welcome</h1>
            <div className="accordion" id="accordionExample">
                <div className="card">
                    <div className="card-header" id="headingOne">
                        <h2 className="mb-0">
                            <button className="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                Upcoming homework deadlines
                            </button>
                        </h2>
                    </div>

                    <div id="collapseOne" className="collapse show text-center" aria-labelledby="headingOne" data-parent="#accordionExample">
                        {courses.studentCourses.map(x => (
                            <div className="card-body">                            
                                <h5>
                                    {x.course!.name}
                                </h5>
                                {x.course!.homework!.map((y: IHomeworkDTO) => (
                                    <div>{deadlines(y, y.studentHomework!, x.appUserId!)}</div>
                                    
                                ))}

                            </div>

                        ))}
                    </div>
                </div>
                <div className="card">
                    <div className="card-header" id="headingTwo">
                        <h2 className="mb-0">
                            <button className="btn btn-link btn-block text-left collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                Recent homework feedback
                            </button>
                        </h2>
                    </div>
                    <div id="collapseTwo" className="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                    {courses.studentCourses.map(x => (
                            <div className="card-body">                            
                                <h5>
                                    {x.course!.name}
                                </h5>
                                {x.course!.homework!.map((y: IHomeworkDTO) => (
                                    <div>{
                                            y.studentHomework!.map((z: IStudentHomework) => (
                                                <div>{feedback(y, z, x.appUserId!)}</div>
                                            ))                                 
                                        }
                                    </div>
                                    
                                ))}

                            </div>

                        ))}
                    </div>
                </div>
                <div className="card">
                    <div className="card-header" id="headingThree">
                        <h2 className="mb-0">
                            <button className="btn btn-link btn-block text-left collapsed" type="button" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                Latest graded courses
                            </button>
                        </h2>
                    </div>
                    <div id="collapseThree" className="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                    {courses.studentCourses.map(x => (
                            <div className="card-body">                            
                                {grades(x)}
                            </div>

                        ))}
                    </div>
                </div>
            </div>
        </div>);
};

export default HomeStudent;