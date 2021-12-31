import { ThrowStmt } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { randomInt } from 'crypto';


@Injectable({
  providedIn: 'root'
})
export class StorybotService {

  constructor() { }

  StoryOutput : string;
  //Password crack
  Story1 = ["You browse the internet for a second but discover noticable network lag. After doing a quick search you discover "];
  Story1Answers = ["",];
  Story1Succes =[""];
  Story1Failure =[""];

  //Social engineering
  Story2 = ["Someone in your surroundings left a note on the local cyber blog that they discovered a local cache they hacked with critical info of local infastructure. Inside they found a cashe of hackingtools. As a secret hint she wanted to resite all star together",
  "",
  ""];
  Story2Answers = ["",];
  Story2Succes =[""];
  Story2Failure =[""];
  
  //Ridllebox
  Story3 = [""];
  Story3Answers = ["",];
  Story3Succes =[""];
  Story3Failure =[""];
  
  //
  Story4 = [];
  Story4Answers = [];
  Story4Succes =[""];
  Story4Failure =[""];
  
  Story5 = ["Template: Here the player is sketched a scene of where they are and what they can do. You find yourself in a library, in front of you is a book. Open as if someone had been reading it.", "What do you do? (read further, walk away)", "What do you do? (take the usb)"];
  Story5Answers = ["","read further", "take the usb"]; //In the sketch of the scene no answer is required
  Story5Succes =["","As you read this story further more storybeats unfold. In this case you find a USB in the book.", "You gained a usb!"];
  Story5Failure =["","You didn't read further and walked away", "You keep on reading and put the book away again."];

  ActiveStory : any;
  ActiveStoryAnswers : any;
  ActiveStorySucces : any;
  ActiveStoryFailure : any;
  StoryBeat = 0;

  RandomStory(){
    this.PickStory(Math.floor(Math.random()*6)+1);
  }

  PickStory(id : number){
    switch (id) {
      case 1:
        this.ActiveStory = this.Story1;
        this.ActiveStoryAnswers = this.Story1Answers;
        this.ActiveStorySucces = this.Story1Succes;
        this.ActiveStoryFailure = this.Story1Failure;
        break;
      case 2:
        this.ActiveStory = this.Story2;
        this.ActiveStoryAnswers = this.Story2Answers;
        this.ActiveStorySucces = this.Story2Succes;
        this.ActiveStoryFailure = this.Story2Failure;
        break;
      case 3:
        this.ActiveStory = this.Story3;
        this.ActiveStoryAnswers = this.Story3Answers;
        this.ActiveStorySucces = this.Story3Succes;
        this.ActiveStoryFailure = this.Story3Failure;
        break;
      case 4:
        this.ActiveStory = this.Story4;
        this.ActiveStoryAnswers = this.Story4Answers;
        this.ActiveStorySucces = this.Story4Succes;
        this.ActiveStoryFailure = this.Story4Failure;
        break;
      case 5:
        this.ActiveStory = this.Story5;
        this.ActiveStoryAnswers = this.Story5Answers;
        this.ActiveStorySucces = this.Story5Succes;
        this.ActiveStoryFailure = this.Story5Failure;
        break;
                  
      default:
        this.ActiveStory = this.Story5;
        this.ActiveStoryAnswers = this.Story5Answers;
        this.ActiveStorySucces = this.Story5Succes;
        this.ActiveStoryFailure = this.Story5Failure;
        break;
    }
    this.StoryBeat = 0;
    this.StoryOutput = this.ActiveStory[this.StoryBeat];
  }

  Progress(input : string){
    
    if (input == this.ActiveStoryAnswers[this.StoryBeat]) {
      if(input!="")
        this.StoryOutput = this.ActiveStorySucces[this.StoryBeat];
      this.StoryBeat++;
      console.log('updated storybeat to:' + this.StoryBeat);
    }else{
      this.StoryOutput = this.ActiveStoryFailure[this.StoryBeat] + '~';
      this.StoryBeat = 0;
    }
    
    if (input.includes('exit')){
      console.log('test');
      this.StoryOutput = '~';
    }
    console.log(this.StoryOutput);
  }

  SetStory(){
    this.StoryOutput = this.ActiveStory[this.StoryBeat];
    
    if(this.StoryBeat == this.ActiveStory.length){
      console.log('entered end of story')
      console.log(this.ActiveStory.length);
      this.StoryOutput = '~';
      this.StoryBeat = 0;
    }
  }
}
