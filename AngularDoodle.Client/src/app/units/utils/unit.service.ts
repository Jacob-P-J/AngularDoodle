import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { HttpService } from '../../http.service';
import { Unit } from '../types/unit.type';


@Injectable({
  providedIn: 'root'
})
export class UnitService {
  constructor(private httpService: HttpService) { }

  getUnits(sortColumn: string = 'id', sortDirection: string = 'asc'): Observable<Unit[]> {
    return this.httpService.getFromServer<Unit[]>(`unit?sortColumn=${sortColumn}&sortDirection=${sortDirection}`);
  }
}
