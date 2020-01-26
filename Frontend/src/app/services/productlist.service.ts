import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Subject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ProductItemModel } from '../models/productitem.model';

@Injectable({
    providedIn: 'root'
})
export class ProductListService {
    public itemsChanged = new Subject<ProductItemModel[]>();
    public productItems: ProductItemModel[] = [];

    constructor(private httpClient: HttpClient) {
        this.reload();
    }

    public reload(){
        this.httpClient.get<ProductItemModel[]>(`${environment.apiUrl}/api/Product`).subscribe((list) => {
            this.productItems = list;
            this.itemsChanged.next(list);
        });
    }

    public get(productItemId: number): ProductItemModel {
        return this.productItems.find(x => x.id === productItemId);
    }

    public save(productItem: ProductItemModel): Observable<ProductItemModel> {
        const baseSaveUrl = `${environment.apiUrl}/api/Product`;

        if (productItem.id) {
            return this.httpClient.put<ProductItemModel>(`${baseSaveUrl}/${productItem.id}`, productItem)
                .pipe(tap(updatedProductItem => {
                    productItem = updatedProductItem;
                }));
        } else {
            return this.httpClient.post<ProductItemModel>(`${baseSaveUrl}`, productItem)
                .pipe(tap(insertedProductItem => {
                    this.productItems.push(insertedProductItem);
                }));
        }
    }

    public delete(productItemId: number) {
        return this.httpClient.delete<ProductItemModel[]>(`${environment.apiUrl}/api/Product/${productItemId}`).subscribe((list) => {
            const productItemToRemove = this.get(productItemId);
            this.productItems.splice(this.productItems.indexOf(productItemToRemove), 1);
        });
    }
}
