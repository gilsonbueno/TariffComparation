import { Component, OnInit, OnDestroy } from '@angular/core';
import { ProductListService } from 'src/app/services/productlist.service';
import { BehaviorSubject } from 'rxjs';
import { ProductItemModel } from 'src/app/models/productitem.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html'
})
export class ProductEditComponent implements OnInit, OnDestroy {
  public editForm: FormGroup;
  public registerToEdit = new BehaviorSubject<number>(null);
  public editItem: ProductItemModel;

  constructor(public activeModal: NgbActiveModal, private productListService: ProductListService) { }

  ngOnInit() {
    this.initEditForm();

    this.registerToEdit.subscribe((id: number) => {
      if (id) {
        this.editItem = this.productListService.get(id);
        this.editForm.setValue({
          'name': this.editItem.name,
          'limitPackage': this.editItem.limitPackage,
          'priceKwh': this.editItem.priceKwh,
          'pricePackage': this.editItem.pricePackage,
          'pricePeriod': this.editItem.pricePeriod,
          'consumeType': this.editItem.consumeType
        });
      } else {
        this.editForm.reset();
        this.editItem = new ProductItemModel();
      }
    });
  }

  ngOnDestroy(): void {
    this.registerToEdit.unsubscribe();
  }

  private initEditForm(): void {
    this.editForm = new FormGroup({
      'name': new FormControl(null, [Validators.required]),
      'priceKwh': new FormControl(null, [Validators.required]),
      'consumeType': new FormControl(null, [Validators.required]),
      'pricePeriod': new FormControl(null, [Validators.required]),
      'pricePackage': new FormControl(null, [Validators.required]),
      'limitPackage': new FormControl(null, [Validators.required])
    });
  }

  public onSave(): void {
    if (this.editForm.invalid) {
      return;
    }

    this.editItem.name = this.editForm.value.name;
    this.editItem.limitPackage = this.editForm.value.limitPackage;
    this.editItem.consumeType = this.editForm.value.consumeType;
    this.editItem.priceKwh = this.editForm.value.priceKwh;
    this.editItem.pricePackage = this.editForm.value.pricePackage;
    this.editItem.pricePeriod = this.editForm.value.pricePeriod;

    this.productListService.save(this.editItem).subscribe(() => {
      this.editForm.reset();
      this.activeModal.close();
    });
  }
}
