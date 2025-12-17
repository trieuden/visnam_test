import { Box, Button } from '@mui/material';
import type { ProductModel } from '../../../../Models/ProductModel';
import { AddOutlined } from '@mui/icons-material';

type ProductProps = {
  product: ProductModel;
  handleAddToCart: (product: ProductModel, quantity: number) => void;
};
export const Product = ({ product, handleAddToCart }: ProductProps) => {
  return (
    <Box
      sx={{ boxShadow: '2px 2px 10px rgba(117, 26, 255, 0.7)' }}
      className="border border-[#751aff] rounded-lg p-4 flex flex-col gap-2 min-w-[200px] w-[200px]"
    >
      <Box component="img" src={product.imageUrl} alt={product.name} className="w-full h-[150px] object-cover rounded-lg" />
      <span className="font-semibold text-[18px]">{product.name}</span>
      <span>
        Giá: <span>{Number(product.price).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</span>
      </span>
      <Button
        startIcon={<AddOutlined className="text-white" />}
        variant="contained"
        className="text-[12px] bg-primary hover:bg-primary-700 mt-auto"
        color="primary"
        onClick={() => handleAddToCart(product, 1)}
      >
        Thêm
      </Button>
    </Box>
  );
};
