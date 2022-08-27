import { Component, Input, OnInit } from '@angular/core';
import { ChartType, Chart, ChartDataset } from 'chart.js';
import * as _ from 'lodash';
import { THEME_COLORS } from 'src/app/models/theme.colors';

const theme = 'Default';

const SAMPLE_PIECHART_LABELS : string[] = ["Bass Pro Shops","Cabelas","Walmart"];

const data = {
  labels: [
    'Bass Pro Shops',
    'Cabelas',
    'Walmart'
  ],
  datasets: [{
    label: 'PieChart',
    data: [300, 112, 100],
    backgroundColor: [
      'rgb(255, 99, 132)',
      'rgb(54, 162, 235)',
      'rgb(255, 205, 86)'
    ],
    hoverOffset: 4
  }],
  options: {
    responsive: true
  }
};




@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {

  constructor() { }

  @Input() inputData: any;
  @Input() limit! : number;

  public pieChartData: any = data;
  public pieChartLabels: string[] = SAMPLE_PIECHART_LABELS;
  public pieChartType: ChartType = "pie";

  ngOnInit(): void {
    //this.parseChartData(this.inputData, this.limit);
  }

  parseChartData(res : any, limit?:number){
    const allData = res.slice(0, limit);
    this.pieChartData = allData.map((x:any)=>_.values(x)[1]);
    this.pieChartLabels = allData.map((x:any)=>_.values(x)[0]);
  }

  themeColors(setname: string): string[] {
    const c = THEME_COLORS.slice(0).find(set => set.name === setname)?.colorSet;

    if (c == null)
      return [];
    else
      return c;
  }
}
