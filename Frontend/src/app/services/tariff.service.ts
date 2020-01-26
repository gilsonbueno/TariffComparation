import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Subject} from 'rxjs';
import { TariffModel } from '../models/tariff.model';

@Injectable({
    providedIn: 'root'
})
export class TariffService {
    public tariffItemsChanged = new Subject<TariffModel[]>();
    public tariffItems: TariffModel[] = [];

    constructor(private httpClient: HttpClient)
    {
    }

    public get(consumption: number){
        this.httpClient.get<TariffModel[]>(`${environment.apiUrl}/api/compare/${consumption}`).subscribe((list) => {
            this.tariffItems = list;
            this.tariffItemsChanged.next(list);
        });
    }
}
