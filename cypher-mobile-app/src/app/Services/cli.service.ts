import { Injectable } from '@angular/core';
import * as internal from 'stream';
import { Player, ApiService } from './api.service';
import { InventoryService } from './inventory.service';
import { LoginService } from './login.service';
import { StorybotService } from './storybot.service';

@Injectable({
  providedIn: 'root'
})
export class CliService {
  constructor(private api: ApiService, public loginservice : LoginService, public inventory : InventoryService, public storybot : StorybotService) { }
  
  teller = 0;
  Output : String;
  Inventory : any;
  Player : any;
  
  ngOnInit() {
    this.api.getAllPlayers().subscribe(p => {
      this.api.players = p; console
        .log('Got all players');
      this.api.players.data.forEach(m => {
        if (m.name === this.loginservice.displayName) {
          console.log(m.name);
          // this.id = m.id;
          this.api.getPlayerById(m.id).subscribe(
            u => {
              this.Inventory = this.Player.data.inventory.items;
              // this.speler.inventory = this.playerbyId.data.inventory;
              // this.speler.messages = m.messages;
              // this.lobbies = m.playerLobbies;
            });
        }
        // else if (this.speler.players.data.length === this.teller) {
        //   console.log('ik ben hier');
        // };
      });
    });
  }

  ListPlayerItems(){
    this.teller = 0;
    this.Output = "";
    this.inventory.GetInventory().forEach(o => {
      this.Output += '\n' + this.teller + ' ---\t';
      this.Output += o.name;
      for (let index = 21; index > o.name.length; index-=4) {
        this.Output +='\t';
      }
      this.Output += '---\t' + o.itemType;
    });
  }

  DecryptItem(){

  }

  SelectStory(){
    this.storybot.PickStory(5);
    this.Output = this.storybot.StoryOutput;
    console.log(this.Output);
  }
  
  ProgresStory(input : string){
    this.storybot.Progress(input);
    this.Output = this.storybot.StoryOutput;
    console.log(this.Output);
  }
  
  SetStory(){
    this.storybot.SetStory();
    this.Output = this.storybot.StoryOutput;
  }
}
