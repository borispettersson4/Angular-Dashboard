import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Server } from '../models/server';
import { ServerMessage } from '../models/server-message';

@Component({
  selector: 'app-server',
  templateUrl: './server.component.html',
  styleUrls: ['./server.component.css']
})
export class ServerComponent implements OnInit {

  constructor() { }

  color!: string;
  buttonText!: string;
  displayText!: string;
  isLoading!:boolean;

  @Input() serverInput! : Server;
  @Output() serverAction = new EventEmitter<ServerMessage>();

  ngOnInit(): void {
    this.toggleStatus(this.serverInput.isOnline)
  }

  toggleStatus(status: boolean) {
    this.serverInput.isOnline = !status;

    if(!status){
      this.color = "#66BB6A";
      this.buttonText = "Shut Down";
      this.displayText = "Online";
    }
    else {
      this.color = "#FF6B6B";
      this.buttonText = "Power On";
      this.displayText = "Offline";
    }
  }

  makeloading() {
    this.color = '#FFCA28';
    this.buttonText = 'Working...';
    this.isLoading = true;
    this.displayText = 'Loading';
  }

  sendServerAction(isOnline:boolean) {
    this.makeloading();
    const payload = this.buildPayload(isOnline);
    this.serverAction.emit(payload);
  }

  buildPayload(isOnline : boolean) : ServerMessage {
    if(isOnline){
      return {
        id:this.serverInput.id,
        payload: 'deactivate'
      };
    }
    else{
      return {
        id:this.serverInput.id,
        payload: 'activate'
      };
    }
  }

}
