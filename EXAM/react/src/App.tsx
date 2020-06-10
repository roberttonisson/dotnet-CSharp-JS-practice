import React, { useState } from "react";
import Header from "./components/shared/Header";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Home from "./components/Home";
import { AppContextProvider, AppContextInitialState, IAppContext } from "./context/AppContext";
import Login from "./components/account/Login";
import Register from "./components/account/Register";
import CourseCreate from "./components/views/course/CourseCreate";
import CourseIndex from "./components/views/course/CourseIndex";
import TeacherIndex from "./components/views/users/TeacherIndex";
import TeacherCourseIndex from "./components/views/course/TeacherIndex";
import HomeworkTeacherIndex from "./components/views/homework/HomeworkTeacherIndex";
import CreateHomework from "./components/views/homework/CreateHomework";
import EditHomework from "./components/views/homework/EditHomework";
import StudentHomeworkTeacherIndex from "./components/views/studenthomework/TeacherIndex";
import StudentHwDetails from "./components/views/studenthomework/Details";
import StudentCourseIndex from "./components/views/studentcourse/StudentCourseIndex";
import HomeworkStudentIndex from "./components/views/homework/HomeworkStudentIndex";
import StudentHwStudentIndex from "./components/views/studenthomework/StudentIndex";
import HomeStudent from "./components/HomeStudent";
import TeacherStudentsIndex from "./components/views/studentcourse/TeacherStudentsIndex";
import StudentHomeworkDetails from "./components/views/studenthomework/Details";

const App = () => {
    const setJwt = (jwt: string | null) => {
        setAppState({...appState, jwt: jwt});
    }

    const setUserName = (userName: string) => {
        setAppState({...appState, userName: userName});
    }

    const setUserRole = (userRole: string) => {
        setAppState({...appState, userRole: userRole});
    }

    const initialAppState = {
        ...AppContextInitialState,
        setJwt,
        setUserName,
        setUserRole,
    } as IAppContext;
    const [appState, setAppState] = useState(initialAppState);

    return (
    <AppContextProvider value={appState}>
        <Router>
            <Header />
            <Switch>
                <Route exact path="/">
                    <Home />
                </Route>
                <Route path="/home">
                    <Home />
                </Route>
                <Route path="/homestudent">
                    <HomeStudent />
                </Route>
                <Route path="/courses/create">
                    <CourseCreate />
                </Route>
                <Route path="/courses">
                    <CourseIndex />
                </Route>
                <Route path="/login">
                    <Login />
                </Route>
                <Route path="/register">
                    <Register />
                </Route>
                <Route path="/teachers">
                    <TeacherIndex />
                </Route>
                <Route path="/teacherscourses">
                    <TeacherCourseIndex />
                </Route>
                <Route path="/studentcourseindex/:courseid">
                    <StudentCourseIndex />
                </Route>
                <Route path="/teacherstudentsindex/:courseid">
                    <TeacherStudentsIndex />
                </Route>
                <Route path="/homeworkteacherindex/:id">
                    <HomeworkTeacherIndex />
                </Route>
                <Route path="/createhomework/:id">
                    <CreateHomework />
                </Route>
                <Route path="/edithomework/:id">
                    <EditHomework />
                </Route>
                <Route path="/studenthomeworkteacher/:id">
                    <StudentHomeworkTeacherIndex />
                </Route>
                <Route path="/studenthomeworkdetails/:id">
                    <StudentHomeworkDetails />
                </Route>
                <Route path="/studentcourses/:email">
                    <StudentCourseIndex />
                </Route>
                <Route path="/HomeworkStudentIndex/:id">
                    <HomeworkStudentIndex />
                </Route>
                <Route path="/homeworkstudentdetails/:id">
                    <StudentHwStudentIndex />
                </Route>
                <h1>Page not found 404</h1>
            </Switch>
        </Router>
    </AppContextProvider>
    );
};

export default App;