import { Component, OnInit } from '@angular/core';

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

  churchQuestions = [{
    question: 'Wat is de straat naam van deze kerk ?',
    answer: 'Bredabaan'
  },
  {
    question: 'Wat is de  naam van deze kerk ?',
    answer: 'Sint-Bartholomeuskerk'
  },
  {
    question: 'Welke kleur heeft het staanbeeld die naast de kerk staat ?',
    answer: 'Wit'
  }
  ];
  constructor() {

  }

  ngOnInit() {
  }

  nextQuestion(antwoord: string) {
    console.log('hmm', antwoord);
    this.questionNumber += 1;

    console.log(this.questionNumber, this.churchQuestions.length);
    if (this.churchQuestions.length - 1 === this.questionNumber) {
      this.buttonName = 'Save and End';
      this.questionTitle = '(Last Question)';
    }
    if (antwoord.toLowerCase() === this.churchQuestions[this.questionNumber - 1].answer.toLowerCase()) {
      console.log(this.churchQuestions[this.questionNumber - 1].answer.toLowerCase());
      this.score += 1;
      console.log('score', this.score);
    }
    if (this.questionNumber === this.churchQuestions.length) {
      console.log('i am here');
      document.getElementById('questionsDiv').style.display = 'none';
      this.questionNumber = 0;
      console.log(this.showResults);
      this.showResults = true;
      this.buttonName = 'Exit';
    }

    this.searchValue = null;
  }
}
