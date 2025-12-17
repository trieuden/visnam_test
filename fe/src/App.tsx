import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './context/AuthContext';
import { CircularProgress, Box } from '@mui/material';
import { useEffect, useState } from 'react';
import { LoginPage } from './app/pages/Login';
import { HomePage } from './app/pages/user';
import { AdminPage } from './app/pages/admin/AdminPage';
import type { UserModel } from './Models/UserModel';

const MainRouter = () => {
  const { user, isLoading } = useAuth();

  const [currentUser, setCurrentUser] = useState<Partial<UserModel>>(user || {});

  useEffect(() => {
    setCurrentUser(user || {});
  }, [user]);

  if (isLoading) {
    return (
      <Box className="flex items-center justify-center h-screen">
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Routes>
      {!currentUser.id ? (
        <>
          <Route path="/login" element={<LoginPage />} />
          <Route path="*" element={<Navigate to="/login" replace />} />
        </>
      ) : currentUser.role?.name === 'Admin' ? (
        <>
          <Route path="/admin" element={<AdminPage />} />
          <Route path="*" element={<Navigate to="/admin" replace />} />
        </>
      ) : (
        <>
          <Route path="/" element={<HomePage />} />
          <Route path="*" element={<Navigate to="/" replace />} />
        </>
      )}
    </Routes>
  );
};

function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <MainRouter />
      </AuthProvider>
    </BrowserRouter>
  );
}

export default App;
