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

  getUnits(sortColumn: string, sortDirection: string, searchName: string, searchCas: string, searchAmount: number | null, searchLocation: string, pageNumber: number, pageSize: number): Observable<any> {
    let params = new HttpParams()
      .set('sortColumn', sortColumn)
      .set('sortDirection', sortDirection)
      .set('searchName', searchName)
      .set('searchCas', searchCas)
      .set('searchAmount', searchAmount?.toString() || '')
      .set('searchLocation', searchLocation)
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.httpService.getFromServer<any>(this.apiUrl, params);
  }
}
