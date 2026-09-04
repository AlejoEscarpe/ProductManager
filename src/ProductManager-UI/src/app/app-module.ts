import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { ProductList } from './components/product-list/product-list';

@NgModule({
  declarations: [
    App // <-- Únicamente el componente principal de la app
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ProductList // <-- MOVER AQUÍ (por ser un Standalone Component)
  ],
  providers: [
    provideHttpClient()
  ],
  bootstrap: [App]
})
export class AppModule { }
