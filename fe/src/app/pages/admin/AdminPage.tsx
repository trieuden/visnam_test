import { useEffect, useState } from 'react';
import { Stack } from '@mui/material';
import { HubConnectionBuilder } from '@microsoft/signalr';

import { GetAllInvoices } from '@/services/InvoiceServices';
import type { InvoiceModel } from '@/models/InvoiceModel';
import { timeFormatter } from '@/utils/TimeFormat';

export const AdminPage = () => {
  const [orders, setOrders] = useState<InvoiceModel[]>([]);
  const [oldInvoice, setOldInvoice] = useState<InvoiceModel[]>();

  const BE_URL = import.meta.env.VITE_WS_URL || 'http://localhost:8080';

  useEffect(() => {
    const connection = new HubConnectionBuilder().withUrl(`${BE_URL}/orders`).withAutomaticReconnect().build();

    connection.start().catch((err) => console.error('🔴 Lỗi kết nối SignalR: ', err));

    connection.on('ReceiveNewOrder', (data) => {
      setOrders((prev) => [data, ...prev]);
    });

    return () => {
      connection.stop();
    };
  }, []);

  useEffect(() => {
    const fetchOldInvoices = async () => {
      const res = await GetAllInvoices();
      if (res) {
        setOldInvoice(res);
      }
    };
    fetchOldInvoices();
  }, []);

  return (
    <Stack spacing={2} className="p-5 text-black">
      <h1 className="text-2xl text-white font-bold mb-4">Danh sách đơn hàng trực tiếp</h1>
      {orders.length === 0 && <p className="text-gray-500 italic px-2">Chưa có đơn hàng mới nào!</p>}
      <Stack spacing={2} className="p-2">
        {orders.map((order, index) => (
          <Stack spacing={1} key={index} className="p-4 border rounded bg-green-300">
            <Stack direction={'row'} spacing={2} alignItems={'end'}>
              <p className="font-bold">Đơn hàng: {order.id}</p>
              <span className="italic text-[14px] ">{timeFormatter.format(new Date(order.dateCreated))}</span>
            </Stack>
            <p className="font-bold">
              Tổng tiền: <span className="font-normal">{order.totalAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</span>
            </p>
          </Stack>
        ))}
      </Stack>

      <Stack className="p-3 border border-white rounded-lg mt-6">
        <h1 className="text-2xl text-white font-bold mb-4">Danh sách đơn hàng</h1>
        <Stack spacing={2} className="p-2">
          {oldInvoice?.map((order, index) => (
            <Stack direction={'row'} spacing={2} alignItems={'start'} key={index} className="p-4 border rounded bg-[#d9d9d9]">
              <span className="text-[18px] font-bold">{index + 1}</span>
              <Stack spacing={1}>
                <Stack direction={'row'} spacing={2} alignItems={'end'}>
                  <p className="font-bold">Đơn hàng: {order.id}</p>
                  <span className="italic text-[14px] ">{timeFormatter.format(new Date(order.dateCreated))}</span>
                </Stack>
                <p className="font-bold">
                  Tổng tiền: <span className="font-normal">{order.totalAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</span>
                </p>
              </Stack>
            </Stack>
          ))}
        </Stack>
      </Stack>
    </Stack>
  );
};
