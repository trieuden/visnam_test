import { Box, Stack, IconButton } from '@mui/material';
import { Remove, Clear, Add } from '@mui/icons-material';

import type { CartModel } from '..';

type CartItemProps = {
  cartItem: CartModel;
  handleChangeQuantity: (productId: string, quantity: number) => void;
  handleDeleteItem: (productId: string) => void;
};

export const CartItem = ({ cartItem, handleChangeQuantity, handleDeleteItem }: CartItemProps) => {
  const handleIncrement = () => {
    handleChangeQuantity?.(cartItem.product.id, cartItem.quantity + 1);
  };
  const handleDecrement = () => {
    const newCount = cartItem.quantity > 1 ? cartItem.quantity - 1 : cartItem.quantity;
    handleChangeQuantity?.(cartItem.product.id, newCount);
  };
  return (
    <Stack spacing={2} direction={'row'} alignItems={'center'} className="text-black bg-[white] rounded-lg p-4 relative">
      <Stack spacing={2} direction={'row'} alignItems={'center'} flex={1}>
        <Box component="img" src={cartItem.product.imageUrl} alt={cartItem.product.name} className="w-[70px] h-[100px] object-cover rounded-lg" />
        <Stack>
          <span className="font-semibold text-[20px]">{cartItem.product.name}</span>
          <span className="text-sm">
            Giá: <span>{Number(cartItem.product.price).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</span>
          </span>
        </Stack>
      </Stack>
      <Box sx={{ display: 'flex', alignItems: 'center' }}>
        <IconButton disabled={cartItem.quantity <= 1} onClick={handleDecrement} size="small">
          <Remove fontSize="small" />
        </IconButton>
        <span>{cartItem.quantity}</span>
        <IconButton onClick={handleIncrement} size="small">
          <Add fontSize="small" />
        </IconButton>
      </Box>
      <span className="justify-end">
        {(cartItem.quantity * cartItem.product.price).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}
      </span>

      <IconButton className="absolute top-0 right-0" onClick={() => handleDeleteItem(cartItem.product.id)} size="small">
        <Clear className="text-red-500" fontSize="small" />
      </IconButton>
    </Stack>
  );
};
