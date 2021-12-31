import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CliService } from '../Services/cli.service';
import { ElementRef } from '@angular/core';


@Component({
  selector: 'app-cli',
  templateUrl: './cli.page.html',
  styleUrls: ['./cli.page.scss'],
})
export class CliPage implements OnInit {
  Output = 'Run CLI.startup... CLI started!';
  commandInput = '';
  selection : string;
  activeCLI = false;
  storyActive = false;
  public options = ['run', 'list', 'decrypt', 'scout', 'clear'];
  
  constructor(public router: Router, private cli: CliService) { }
  
  ngOnInit() {
  }

  @ViewChild('cliOutput') cliOutput: ElementRef;

  resize() {
      this.cliOutput.nativeElement.style.height = this.cliOutput.nativeElement.scrollHeight + 'px';
      // this.cliOutput.nativeElement.style.scrollTop = this.cliOutput.nativeElement.style.scrollHeight;
  }

  toggleCLI() {
    this.activeCLI = !this.activeCLI;
  }
  
  //cliOutput = 'Run CLI.startup... CLI started!';

  // eslint-disable-next-line @typescript-eslint/naming-convention
  CommandInput(command?: string) {
    //Command check
    if(this.commandInput.includes(' '))
      this.ProcesCommand(this.commandInput.split(' ')[0].toLowerCase());
    else
      this.ProcesCommand(this.commandInput.toLowerCase());
  }

  onChange(event) {
    if (this.selection === event) return;
    this.selection = event;
    console.log(this.selection);
    this.ProcesCommand(this.selection);
  }


  ProcesCommand(value : string){
    switch (value) {
      case 'run':
        
        break;
      case 'list':
        this.cli.ListPlayerItems();
        this.ReadOutput()
        break;
      case 'decrypt':
        this.cli.DecryptItem();
        this.ReadOutput();
        break;
      case 'scout':
        this.cli.SelectStory();
        this.ReadOutput();
        this.cli.ProgresStory("");
        this.cli.SetStory();
        this.ReadOutput();
        this.storyActive = true;
        break;
      case 'clear':
        this.Output = '';
        break;
  
      default:
        this.Output += '\n ' + '> '+ this.commandInput;
        if (this.storyActive) {
          console.log(this.commandInput);
          this.cli.ProgresStory(this.commandInput);
          this.ReadOutput()
          this.cli.SetStory();
          this.ReadOutput();
        }
        break;
      }
      
    this.commandInput = '';  
  }

  ReadOutput(){
    if (this.Output.slice(this.Output.length-1)=='~') {
      this.storyActive = false;
    }
    else{
      this.Output += '\n' + this.cli.Output;
    }

    console.log(this.Output.slice(this.Output.length-1))
  }

  goToMapscreen() {
    this.router.navigate(['map-screen']);
  }

  // eslint-disable-next-line @typescript-eslint/naming-convention
  GoTo(page: string) {
    console.log('Called open ' + page);
    this.router.navigate([page]).then(() => window.location.reload());
  }

}
