import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { ProductMainListComponent } from './components/product/product-main-list/product-main-list.component';
import { ProductEditComponent } from './components/product/product-edit/product-edit.component';
import { ErrorComponent } from './components/error/error.component';
import { ErrorInterceptor } from './services/error.interceptor';
import { ProductListService } from './services/productlist.service';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    ProductMainListComponent,
    ProductEditComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ProductListService
  ],
  entryComponents: [ProductEditComponent, ErrorComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }