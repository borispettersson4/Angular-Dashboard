import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ServerMessage } from '../models/server-message';
import { Server } from '../models/server';

@Injectable({
  providedIn: 'root'
})
export class ServerService {

  constructor(private _http: HttpClient) {
    this.headers = new Headers({
      'Content-Type' : 'application/json',
      'Accept' : 'q=0.8;application/json;q=0.9'
    });
   }

   headers!: Headers;
   options = {
    headers: new HttpHeaders().append('key', 'value'),
    params: new HttpParams().append('key', 'value')
  }

  getServers(): Observable<Server[]> {
    return this._http.get('https://localhost:44350/api/server/').pipe(map((res:any) => res));
  }

  handleServerMessage(msg:ServerMessage) : Observable<Response> {
    const url = 'https://localhost:44350/api/server/' + msg.id;
    return this._http.put(url,msg,this.options).pipe(map((res:any) => res));
  }
}
