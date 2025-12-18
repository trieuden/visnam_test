import { useState } from 'react';
import { Box, Button, Input, Stack, Typography } from '@mui/material';
import Cookies from 'js-cookie';
import { useAuth } from '@/context/AuthContext';
import { Login } from '@/services/AuthServices';

export const LoginPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const { refreshUser } = useAuth();

  const handleLogin = async () => {
    if (!username || !password) {
      alert('Vui lòng nhập đầy đủ thông tin đăng nhập');
      return;
    }
    Cookies.set('accessToken', await Login(username, password));

    alert('Đăng nhập thành công');
    await refreshUser();
    window.location.href = '/';
  };

  return (
    <Box className="flex items-center justify-center min-h-screen bg-[#0d1224]">
      <Box className="w-[400px] bg-white rounded-xl shadow-md">
        <Stack spacing={2} className="shadow-2xl rounded-xl p-6 backdrop-blur-sm">
          <h1 className="text-2xl font-semibold text-black">Đăng nhập</h1>
          <Stack spacing={2}>
            <Stack>
              <Typography className="text-gray-800 text-sm">Tài khoản</Typography>
              <Input
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                className="rounded-lg border-black p-2 bg-[#d9d9d999]"
                disableUnderline
              />
            </Stack>
            <Stack>
              <Typography className="text-gray-800 text-sm ">Mật khẩu</Typography>
              <Input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="rounded-lg border-black p-2 bg-[#d9d9d999]"
                disableUnderline
              />
            </Stack>

            <Button variant="contained" className="w-[180px] self-center" onClick={handleLogin}>
              Đăng nhập
            </Button>
          </Stack>
        </Stack>
      </Box>
    </Box>
  );
};
