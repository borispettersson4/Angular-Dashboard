import { Component, ModuleWithComponentFactories, OnInit } from '@angular/core';
import { ChartType } from 'chart.js';
import { SalesDataService } from 'src/app/services/sales-data.service';
import * as moment from 'moment';

const SAMPLE_BARCHART_DATA: any[] = [
  { data: [65, 34, 88, 82, 55, 53], label: 'Fall Sales' },
  { data: [90, 96, 88, 82, 22, 11], label:'Winter Sales'}
];

const SAMPLE_BARCHART_LABELS : string[] = ["W1","W2","W3","W4","W5","W6","W7",];

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent implements OnInit {

  constructor(private _salesDataService: SalesDataService) { }

  orders: any;
  orderLabels!: string[];
  orderData!: number[];

  public barChartData!: any[];
  public barChartLabels!: string[];
  public barChartType: ChartType = "bar";
  public barChartLegend: boolean = false;
  public barChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  ngOnInit(): void {
    this._salesDataService.getOrders(1,100).subscribe(res=> {
      const localChartData = this.getChartData(res);
      this.barChartLabels = localChartData.map((x:any) => x[0]).reverse();
      this.barChartData = [{'data' : localChartData.map((x:any)=>x[1]), 'label':'Sales'}]
    })
  }

  getChartData(res:any) {
    this.orders = res['page']['data'];
    const data = this.orders.map((o:any)=>o.total);
    const formattedOrders = this.orders.reduce((r: any,e : any) => {
      r.push([moment(e.placed).format('YY-MM-DD'),e.total]);
      return r;
    },[]);

    const p: any[] = [];

    const chartData = formattedOrders.reduce((r:any,e:any)=>{
      const key = e[0];
      if(!p[key]){
        p[key] = e;
        r.push(p[key]);
      } else {
        p[key][1] += e[1];
      }
      return r;
    },[]);

    return chartData;

  }

}
