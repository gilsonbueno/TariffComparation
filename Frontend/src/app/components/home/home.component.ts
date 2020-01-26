import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { TariffService } from 'src/app/services/tariff.service';
import { TariffModel } from 'src/app/models/tariff.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  public searchForm: FormGroup;
  public tariffs: TariffModel[] = [];
  public consumptionSearched = 0;
  private tariffsChanged: Subscription;
  
  constructor(private tariffService: TariffService) { }

  public onSearch(): void {
    if (this.searchForm.invalid || this.searchForm.value.consumption <= 0) {
      return;
    }
    this.consumptionSearched = this.searchForm.value.consumption;
    this.tariffService.get(this.consumptionSearched);
    this.searchForm.reset();
  }
  
  ngOnInit() {
    this.searchForm = new FormGroup({
      'consumption': new FormControl(null, [Validators.required])
    });
    
    this.tariffs = this.tariffService.tariffItems;
    this.tariffsChanged = this.tariffService.tariffItemsChanged.subscribe((items) => {
      this.tariffs = items;
    });
  }

  ngOnDestroy(): void {
    this.tariffsChanged.unsubscribe();
    this.tariffService.tariffItems = [];
  }

}
