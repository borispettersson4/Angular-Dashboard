import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AnonymousSubject } from 'rxjs/internal/Subject';
import { Server } from 'src/app/models/server';
import { ServerMessage } from 'src/app/models/server-message';
import { ServerService } from 'src/app/services/server.service';

const SAMPLE_SERVERS: Server[] = [
  {id:1,name:"dev-web",isOnline:true},
  {id:2,name:"dev-api",isOnline:false},
  {id:3,name:"prod-web",isOnline:false},
  {id:4,name:"prod-api",isOnline:true}
]

@Component({
  selector: 'app-section-health',
  templateUrl: './section-health.component.html',
  styleUrls: ['./section-health.component.css']
})
export class SectionHealthComponent implements OnInit {

  constructor(private _serverService : ServerService) { }

  servers: Server[] = SAMPLE_SERVERS;

  ngOnInit(): void {
    this.refreshData();
    this._serverService.getServers().subscribe(res => {
      this.servers = res;
    });
  }

  refreshData(){
    this._serverService.getServers().subscribe(res=>{
      this.servers = res;
    });
  }

  sendMessage(msg: ServerMessage) {
    this._serverService.handleServerMessage(msg).subscribe(res => console.log(res),err => console.log(err));
  }


}
