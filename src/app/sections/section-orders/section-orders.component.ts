import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/order';
import { SalesDataService } from 'src/app/services/sales-data.service';

@Component({
  selector: 'app-section-orders',
  templateUrl: './section-orders.component.html',
  styleUrls: ['./section-orders.component.css']
})
export class SectionOrdersComponent implements OnInit {

  constructor(private _salesData: SalesDataService) { }

  orders!: Order[];
  total = 0;
  page = 1;
  limit = 10;
  loading : boolean = false;

  ngOnInit() {
    this.getOrders();
  }

  getOrders(): void {
    this._salesData.getOrders(this.page, this.limit)
      .subscribe(res => {
        console.log('Result from getOrders: ', res);
        this.orders = res['page']['data'];
        this.total = res['page'].total;
        this.loading = false;
      });
  }

  goToPrevious(): void {
    this.page--;
    this.getOrders();
  }
  goToNext(): void {
    this.page++;
    this.getOrders();
  }

  goToPage(n:number): void {
    this.page = n;
    this.getOrders();
  }

}