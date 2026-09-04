import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { Product, CreateProduct } from '../../models/product.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.css']
})
export class ProductList implements OnInit {
  private productService = inject(ProductService);
  private cdr = inject(ChangeDetectorRef); // <-- 1. Inyectar ChangeDetectorRef

  products: Product[] = [];
  isLoading = false;
  errorMessage = '';

  newProduct: CreateProduct = {
    name: '',
    price: 0,
    stock: 0
  };

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading = true;
    this.productService.getProducts().subscribe({
      next: (data) => {
        this.products = data;
        this.isLoading = false;
        this.cdr.detectChanges(); // <-- 2. Forzar a Angular a refrescar el HTML inmediatamente
      },
      error: (err) => {
        this.errorMessage = 'Error al cargar los productos.';
        this.isLoading = false;
        this.cdr.detectChanges(); // <-- 3. Refrescar también en caso de error
        console.error(err);
      }
    });
  }

  onSubmit(): void {
    const priceNum = Number(this.newProduct.price);
    const stockNum = Number(this.newProduct.stock);

    if (!this.newProduct.name.trim() || isNaN(priceNum) || priceNum <= 0) {
      alert('Por favor ingrese datos válidos');
      return;
    }

    const payload: CreateProduct = {
      name: this.newProduct.name,
      price: priceNum,
      stock: stockNum
    };

    this.productService.createProduct(payload).subscribe({
      next: () => {
        this.loadProducts();
        this.resetForm();
      },
      error: (err) => {
        alert('Error al crear el producto');
        console.error(err);
      }
    });
  }

  deleteProduct(id: number): void {
    if (confirm('¿Está seguro de eliminar este producto?')) {
      this.productService.deleteProduct(id).subscribe({
        next: () => this.loadProducts(),
        error: (err) => console.error(err)
      });
    }
  }

  resetForm(): void {
    this.newProduct = { name: '', price: 0, stock: 0 };
  }
}
