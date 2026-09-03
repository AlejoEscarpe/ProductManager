export interface Product {
  id: number;
  name: string;
  price: number;
  stock: number;
}

export interface CreateProduct {
  name: string;
  price: number;
  stock: number;
}
