import type { UserModel } from '@/models/UserModel';
import api from './api';

export const Login = async (username: string, password: string): Promise<string> => {
  try {
    const res = await api.post('/auth/login', { username, password });
    return res.data.accessToken;
  } catch (error) {
    throw error;
  }
};

export const getMe = async (): Promise<UserModel> => {
  try {
    const res = await api.get('/auth/me');
    return res.data;
  } catch (error) {
    throw error;
  }
};
