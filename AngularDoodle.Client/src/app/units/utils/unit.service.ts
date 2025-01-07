import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { HttpService } from '../../http.service';
import { Unit } from '../types/unit.type';
import { HttpParams } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class UnitService {
  private apiUrl = 'unit';
  constructor(private httpService: HttpService) { }

  getUnits(sortColumn: string = 'id', sortDirection: string = 'asc', searchName: string, searchCas: string, searchAmount: number | null, searchLocation: string): Observable<Unit[]> {
    let params = new HttpParams()
      .set('sortColumn', sortColumn)
      .set('sortDirection', sortDirection)
      .set('searchName', searchName)
      .set('searchCas', searchCas)
      .set('searchLocation', searchLocation);

    if (searchAmount != null) {
      params = params.set('searchAmount', searchAmount.toString());
    }

    return this.httpService.getFromServer<Unit[]>(this.apiUrl, params);
  }
}
