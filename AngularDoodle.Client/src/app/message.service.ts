import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  message: string = '';
  constructor(private httpService: HttpService) { }

  getMessageFromServer(): Observable<Message> {
    return this.httpService.getFromServer<Message>('message/');
  }
}

export interface Message {
  text: string;
}
