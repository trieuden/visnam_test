import type { ReactNode } from 'react';
import { createContext, useContext, useState, useEffect } from 'react';
import Cookies from 'js-cookie';

import type { UserModel } from '@/models';
import { getMe } from '@/services/AuthServices';

interface AuthContextType {
  user: UserModel | null;
  isLoading: boolean;
  refreshUser: () => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<UserModel | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const fetchUser = async () => {
    if (!user) {
      setIsLoading(true);
      try {
        const userData = await getMe();
        setUser(userData);
      } catch (error) {
        console.error('Lỗi lấy thông tin user:', error);
        setUser(null);
      } finally {
        setIsLoading(false);
      }
    }
  };

  useEffect(() => {
    fetchUser();
  }, []);

  const logout = () => {
    Cookies.remove('accessToken');
    setUser(null);
  };

  return <AuthContext.Provider value={{ user, isLoading, refreshUser: fetchUser, logout }}>{children}</AuthContext.Provider>;
};

// Hook custom để dùng nhanh context
export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth phải được sử dụng trong AuthProvider');
  }
  return context;
};
