import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../Services/api.service';

@Component({
  selector: 'app-tourist',
  templateUrl: './tourist.page.html',
  styleUrls: ['./tourist.page.scss'],
})
export class TouristPage implements OnInit {

  questionTitle = '';
  questionNumber = 0;
  buttonName = 'Send';
  score = 0;
  showResults = false;
  searchValue = '';
  checkLocation: any;
  chooseRightArray = [];
  radioValue: any;

  // eslint-disable-next-line @typescript-eslint/naming-convention
  Questions = [];



  constructor(public router: Router, public apiService: ApiService) {
    this.checkLocation = localStorage.getItem('streets');
    this.chooseRightArray = [];
    console.log(this.checkLocation);

  }

  ngOnInit() {
    // this.apiService.getAllQuestions().subscribe(q => {
    //   this.Questions = q.data;
    //   console.log('Got Questions');
    //   console.log(this.Questions[0]);
    //   this.switchArrayQuestions();

    // });
    this.getQuestions();

  }

  async getQuestions() {
    const retrieveData = await this.apiService.getAllQuestions().toPromise();
    this.Questions = retrieveData.data;
    this.switchArrayQuestions();
  }
  switchArrayQuestions() {
    console.log('2');
    this.Questions.forEach(element => {
      console.log(element);
      if (this.checkLocation === element.location) {
        this.chooseRightArray.push(element);
        console.log(this.chooseRightArray[0].location);
      }
    });
    console.log(this.chooseRightArray.length);
  }
  nextQuestion(antwoord: any) {
    console.log(this.radioValue);
    //this.switchArrayQuestions();
    this.radioValue = '';
    this.questionNumber += 1;
    console.log(this.questionNumber, this.chooseRightArray.length);
    if (this.chooseRightArray.length - 1 === this.questionNumber) {
      this.buttonName = 'Save and End';
      this.questionTitle = '(Last Question)';
    }
    if (this.chooseRightArray[this.questionNumber - 1].type !== 'radio') {
      console.log('not equal');
      if (antwoord.toLowerCase() === this.chooseRightArray[this.questionNumber - 1].answer.toLowerCase()) {
        console.log(this.chooseRightArray[this.questionNumber - 1].answer.toLowerCase());
        this.score += 1;
        console.log('score', this.score);
      }
    }
    else if (antwoord === this.chooseRightArray[this.questionNumber - 1].answer) {

      this.score += 1;
    }
    if (this.questionNumber === this.chooseRightArray.length) {
      console.log('i am here');
      document.getElementById('questionsDiv').style.display = 'none';
      this.questionNumber = 0;
      console.log(this.showResults);
      this.showResults = true;
      this.buttonName = 'Exit';
      console.log(this.buttonName);
    }
    this.searchValue = null;
  }
  goTo() {
    console.log('hmm u here ');
    this.apiService.lobbyNaam = 'Tourist';
    this.router.navigate(['map-screen']);
  }
}
