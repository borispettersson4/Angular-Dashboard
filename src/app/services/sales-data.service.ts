import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class SalesDataService {

  constructor(private _http: HttpClient) { }

  getOrders(pageIndex: number, pageSize: number) {
    console.log("Fetching Orders");
    return this._http.get('https://localhost:44350/api/order/' + pageIndex + '/' + pageSize)
      .pipe(map((res:any) => res));
  }

  getOrdersByCustomer(n: number) {
    console.log("Fetching Orders");
    return this._http.get('https://localhost:44350/api/order/bycustomer/' + n)
      .pipe(map((res:any) => res));
  }

  getOrdersByState() {
    console.log("Fetching Orders");
    return this._http.get('https://localhost:44350/api/order/bystate')
      .pipe(map((res:any) => res));
  }

}