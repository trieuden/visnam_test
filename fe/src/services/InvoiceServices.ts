import type { CreateInvoiceDto } from '@/models';
import api from './api';

export const CreateInvoice = async (invoice: CreateInvoiceDto): Promise<void> => {
  try {
    const response = await api.post('/invoices', invoice);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const GetAllInvoices = async () => {
  try {
    const response = await api.get('/invoices');
    return response.data;
  } catch (error) {
    console.log(error);
  }
};
