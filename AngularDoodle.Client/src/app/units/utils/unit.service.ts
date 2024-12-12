import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { HttpService } from '../../http.service';
import { Unit } from '../types/unit.type';


@Injectable({
  providedIn: 'root'
})
export class UnitService {
  constructor(private httpService: HttpService) { }
  getUnits(page: number, pageSize: number): Observable<Unit[]> {
    return this.httpService.getFromServer<Unit[]>(`https://localhost:7010/unit?page=${page}&pageSize=${pageSize}`);
  }
}
