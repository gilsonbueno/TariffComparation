import { OnInit, Component, OnDestroy } from '@angular/core';
import { ProductItemModel } from 'src/app/models/productitem.model';
import { ProductListService } from 'src/app/services/productlist.service';
import { ProductEditComponent } from '../product-edit/product-edit.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-main-list',
  templateUrl: './product-main-list.component.html',
  styleUrls: ['./product-main-list.component.scss']
})
export class ProductMainListComponent implements OnInit, OnDestroy {
  private itemsChanged: Subscription;
  public productItems: ProductItemModel[] = [];

  constructor(private productListService: ProductListService, private modalService: NgbModal) { }

  ngOnInit() {
    this.productItems = this.productListService.productItems;

    this.itemsChanged = this.productListService.itemsChanged.subscribe((productItems) => {
      this.productItems = productItems;
    });
  }

  ngOnDestroy(): void {
    this.itemsChanged.unsubscribe();
  }

  public onEdit(productItemId: number): void {
    this.openEditPopup(productItemId);
  }

  public onAdd(): void {
    this.openEditPopup(null);
  }

  public onDelete(productItemId: number): void {
    this.productListService.delete(productItemId);
  }
  
  private openEditPopup(productItemId: number) {
    const modalRef = this.modalService.open(ProductEditComponent);
    const component = <ProductEditComponent>modalRef.componentInstance;
    component.registerToEdit.next(productItemId);
  }
}
