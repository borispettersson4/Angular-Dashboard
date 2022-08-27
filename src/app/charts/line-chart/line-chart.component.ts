import { Component, OnInit } from '@angular/core';
import { ChartType } from 'chart.js';

const SAMPLE_LINECHART_DATA: any[] = [
  { data: [65, 34, 88, 82, 55, 53], label: 'Fall Sales' },
  { data: [90, 53, 22, 12, 53, 11], label:'Winter Sales'},
  { data: [11, 96, 55, 11, 22, 71], label:'Winter Sales'}
];

const SAMPLE_LINECHART_LABELS : string[] = ["Jan","Feb","Mar","Apr","May","Jun"];

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent implements OnInit {

  constructor() { }

  public lineChartData: any[] = SAMPLE_LINECHART_DATA;
  public lineChartLabels: string[] = SAMPLE_LINECHART_LABELS;
  public lineChartType: ChartType = "line";
  public lineChartLegend: boolean = true;
  public lineChartOptions: any = {
    responsive: true
  };

  ngOnInit(): void {
  }

}
