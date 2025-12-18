import api from './api';
import type { ProductModel } from '@/models';

export const getAllProducts = async (): Promise<ProductModel[]> => {
  try {
    const res = await api.get('/products');
    return res.data;
  } catch (error) {
    throw error;
  }
};
