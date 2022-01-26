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
  Questions = [{
    question: 'Wat is de straat naam van deze kerk ?',
    answer: 'Bredabaan',
    type: 'input',
    location: 'Kerk'
  },
  {
    question: 'Is er een tramhalte voor de kerk?',
    answer: 'JA',
    type: 'radio',
    option1: 'JA',
    option2: 'NEE',
    location: 'Kerk'
  },
  {
    question: 'Wat is de  naam van dit kerk ?',
    answer: 'Sint-Bartholomeuskerk',
    type: 'input',
    location: 'Kerk'
  },
  {
    question: 'Hoe heet de school naast dit kerk ?',
    answer: 'Sint Eduardus',
    type: 'input',
    location: 'Kerk'
  },
  {
    question: 'Wat is de naam van het restaurant aan de rechterkant van de kerk?',
    answer: 'Mila Palace',
    type: 'radio',
    option1: 'Kebab King',
    option2: 'Mila Palace',
    location: 'Kerk'
  },
  {
    question: 'Heeft deze Districthuis een gehandicapte parkeerplaats?',
    answer: 'JA',
    type: 'radio',
    option1: 'JA',
    option2: 'NEE',
    location: 'Gemeentehuis'
  },
  {
    question: 'Hoe noemt de tramhalte voor dit Districthuis',
    answer: 'Burgermmeesternolf',
    type: 'input',
    location: 'Gemeentehuis'
  },
  {
    question: 'Is er een politie kantoor naast dit Districthuis?',
    answer: 'JA',
    type: 'radio',
    option1: 'JA',
    option2: 'NEE',
    location: 'Gemeentehuis'
  },
  {
    question: 'Wat is het nummer van de Medisch Huis ?',
    type: 'radio',
    answer: '03 644 00 24',
    option1: '03 644 00 12',
    option2: '03 644 00 24',
    location: 'Gemeentehuis'
  },
  {
    question: 'Kan je hier in de buurt een corona test doen ?',
    answer: 'JA',
    type: 'radio',
    option1: 'NEE',
    option2: 'JA',
    location: 'Gemeentehuis'
  }
  ];

  constructor(public router: Router, public apiService: ApiService) {
    this.checkLocation = localStorage.getItem('streets');
    this.chooseRightArray = [];
    console.log(this.checkLocation);
    this.switchArrayQuestions();
  }

  ngOnInit() {

  }

  switchArrayQuestions() {
    this.Questions.forEach(element => {
      if (this.checkLocation === element.location) {
        this.chooseRightArray.push(element);
        console.log(this.chooseRightArray[1]);
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
