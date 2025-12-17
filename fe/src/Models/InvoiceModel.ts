type CreateInvoiceDetailDto = {
  productId: string;
  quantity: number;
};

export type CreateInvoiceDto = {
  userId: string;
  invoiceDate: Date;
  totalAmount: number;
  invoiceDetails: CreateInvoiceDetailDto[];
};
export type InvoiceModel = {
  id: string;
  userId: string;
  dateCreated: Date;
  totalAmount: number;
};
