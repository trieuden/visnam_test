import { Stack, Divider, Button, Dialog, DialogTitle } from '@mui/material';
import { useEffect, useState } from 'react';
import Cookies from 'js-cookie';

import { Product, CartItem } from '@/app/pages/user/components';
import { useAuth } from '@/context/AuthContext';

import type { CreateInvoiceDto, ProductModel } from '@/models';
import { CreateInvoice } from '@/services/InvoiceServices';
import { getAllProducts } from '@/services/ProductServices';

export type CartModel = {
  userId: string;
  product: ProductModel;
  quantity: number;
};

export const HomePage = () => {
  const [products, setProducts] = useState<ProductModel[]>([]);
  const [cart, setCart] = useState<CartModel[]>([]);
  const [refreshCart, setRefreshCart] = useState(new Date());
  const { user } = useAuth();

  const [openPaymentConfirm, setOpenPaymentConfirm] = useState(false);

  useEffect(() => {
    const fetchProducts = async () => {
      const res = await getAllProducts();
      setProducts(res);
    };
    fetchProducts();
  }, []);

  useEffect(() => {
    const fetchCart = async () => {
      const cartJson = Cookies.get('cart');
      const cart = cartJson ? JSON.parse(cartJson) : [];
      if (cart) {
        const userCart = cart.filter((item: CartModel) => item.userId === user?.id);
        setCart(userCart);
      }
    };
    fetchCart();
  }, [refreshCart]);

  const handleAddToCart = (product: ProductModel, quantity: number) => {
    const existingItem = cart.find((item) => item.product.id === product.id);
    if (existingItem) {
      existingItem.quantity += quantity;
    } else {
      cart.push({ userId: user?.id || '', product, quantity });
    }
    Cookies.set('cart', JSON.stringify(cart), { expires: 7 });
    alert(`Đã thêm ${product.name} vào giỏ hàng`);
    setRefreshCart(new Date());
  };

  const handleChangeQuantity = (productId: string, quantity: number) => {
    const itemIndex = cart.findIndex((item) => item.product.id === productId && item.userId === user?.id);
    if (itemIndex !== -1) {
      cart[itemIndex].quantity = quantity;
      Cookies.set('cart', JSON.stringify(cart), { expires: 7 });
      setRefreshCart(new Date());
    }
  };

  const handleDeleteItem = (productId: string) => {
    const newCart = cart.filter((item) => item.product.id !== productId && item.userId === user?.id);
    Cookies.set('cart', JSON.stringify(newCart), { expires: 7 });
    setCart(newCart);
  };

  const handlePayment = async () => {
    if (cart.length === 0) {
      alert('Chưa có sản phẩm trong giỏ hàng');
      return;
    }
    const data: CreateInvoiceDto = {
      userId: user?.id || '',
      invoiceDate: new Date(),
      totalAmount: cart.reduce((total, item) => total + item.product.price * item.quantity, 0),
      invoiceDetails: cart.map((item) => ({
        productId: item.product.id,
        quantity: item.quantity,
      })),
    };
    await CreateInvoice(data);
    Cookies.remove('cart');
    setCart([]);
    alert('Thanh toán thành công');
  };

  return (
    <Stack direction={'row'} spacing={1} className="p-4 h-[100vh]">
      {/* Cart */}
      <Stack spacing={4} sx={{ boxShadow: '0px 0px 10px #ffffff' }} className="rounded-lg p-4 min-w-[450px] w-[450px]">
        <h1 className="text-2xl font-semibold">Giỏ Hàng</h1>
        <Stack className="px-3">
          {cart.length === 0 ? (
            <span className="italic text-gray-500">Chưa có sản phẩm trong giỏ hàng</span>
          ) : (
            cart.map((item) => (
              <CartItem key={item.product.id} cartItem={item} handleChangeQuantity={handleChangeQuantity} handleDeleteItem={handleDeleteItem} />
            ))
          )}
        </Stack>
        {cart.length > 0 && (
          <Stack direction={'row'} justifyContent="space-around" alignItems="center">
            <Button
              className="w-[160px] bg-red-500 hover:bg-red-700"
              variant="contained"
              onClick={() => {
                if (confirm('Bạn có chắc chắn muốn xóa giỏ hàng?')) {
                  Cookies.remove('cart');
                  setCart([]);
                }
              }}
            >
              Xóa Giỏ Hàng
            </Button>
            <Button className="w-[160px]" variant="contained" color="primary" onClick={() => setOpenPaymentConfirm(true)}>
              Thanh Toán
            </Button>
          </Stack>
        )}
      </Stack>
      <Divider />
      {/* Products */}
      <Stack spacing={2} flex={2} className="rounded-lg " sx={{ boxShadow: '0px 0px 10px #ffffff' }}>
        <h1 className="text-2xl font-semibold p-4">Danh Sách Sản Phẩm</h1>
        <Stack direction={'row'} flexWrap="wrap" gap={3} className="px-3 overflow-y-auto custom-scrollbar">
          {products.map((product) => (
            <Product product={product} key={product.id} handleAddToCart={handleAddToCart} />
          ))}
        </Stack>
      </Stack>
      {/* Confirm Payment */}
      <Dialog open={openPaymentConfirm} onClose={() => setOpenPaymentConfirm(false)}>
        <DialogTitle sx={{ p: 1 }}>Xác Nhận Thanh Toán</DialogTitle>
        <DialogTitle sx={{ px: 1, py: 0, fontSize: '16px' }}>Bạn có chắc chắn muốn thanh toán giỏ hàng không?</DialogTitle>
        <Stack direction="row" spacing={2} justifyContent="center" sx={{ p: 2 }}>
          <Button variant="outlined" onClick={() => setOpenPaymentConfirm(false)}>
            Hủy
          </Button>
          <Button
            variant="contained"
            color="primary"
            onClick={async () => {
              await handlePayment();
              setOpenPaymentConfirm(false);
            }}
          >
            Xác Nhận
          </Button>
        </Stack>
      </Dialog>
    </Stack>
  );
};
