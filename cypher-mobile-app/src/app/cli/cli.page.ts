import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cli',
  templateUrl: './cli.page.html',
  styleUrls: ['./cli.page.scss'],
})
export class CliPage implements OnInit {

  cliOutput = 'Run CLI.startup... CLI started!';
  commandInput = ' ';
  activeCLI = false;
  public commands = ['Run', 'List'];
  constructor(public router: Router) { }

  ngOnInit() {
  }



  // @ViewChild('command-output') commandoutput:ElementRef;

  toggleCLI() {
    this.activeCLI = !this.activeCLI;
  }

  // eslint-disable-next-line @typescript-eslint/naming-convention
  CommandInput(command?: string) {
    this.cliOutput += '\n ' + this.commandInput;
    this.commandInput = ' ';
    // this.commandoutput.nativeElement.setAttribute(this.commandoutput.nativeElement);
  }

  goToMapscreen() {
    this.router.navigate(['map-screen']);
  }

}
