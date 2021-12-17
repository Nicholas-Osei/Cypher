import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cli',
  templateUrl: './cli.page.html',
  styleUrls: ['./cli.page.scss'],
})
export class CliPage implements OnInit {

  constructor(public router: Router) { }

  ngOnInit() {
  }
  
  cliOutput : string = 'Run CLI.startup... CLI started!';
  commandInput : string = ' ';
  activeCLI = false;
  public commands = ['Run', 'List'];

  // @ViewChild('command-output') commandoutput:ElementRef;

  toggleCLI(){
    this.activeCLI = !this.activeCLI;
  }

  CommandInput(command:string){
    this.cliOutput += "\n " + this.commandInput;
    this.commandInput = ' ';
    // this.commandoutput.nativeElement.setAttribute(this.commandoutput.nativeElement);
  }

  goToMapscreen() {
    this.router.navigate(['map-screen']);
  }

}
