import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Layout from './components/Layout';
import LoginPage from './pages/LoginPage';
import ProfilePage from './pages/ProfilePage';
import HomePage from './pages/HomePage';
import CreateWorkoutPage from './pages/CreateWorkoutPage';
import WorkoutPlanPage from './pages/WorkoutPlanPage';
import SessionPage from './pages/SessionPage';
import MyWorkoutsPage from './pages/MyWorkoutsPage';

import type React from 'react';
import { useAuth } from './context/AuthContext';

function ProtectedRoute ({children}:{children:React.ReactNode}){
  const {isLoggedIn, isInitializing}=useAuth();
  if (isInitializing) return null;
  return isLoggedIn?children:<Navigate to="/login" />;
}

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route element={<Layout />}>
          <Route path="/" element={<HomePage />} />
          <Route path="/profile" element={<ProtectedRoute><ProfilePage/></ProtectedRoute>}/>
          <Route path="/workout" element={<CreateWorkoutPage/>}/>
          <Route path="/workout/plan" element={<WorkoutPlanPage/>}/>
          <Route path="/my-workouts" element={<ProtectedRoute><MyWorkoutsPage/></ProtectedRoute>}/>
          <Route path="/session" element={<SessionPage />} />
        </Route>
        <Route path="*" element={<Navigate to="/login" />} />
      </Routes>
    </BrowserRouter>
  );
}