import { TestBed } from '@angular/core/testing';

import { StorybotService } from './storybot.service';

describe('StorybotService', () => {
  let service: StorybotService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StorybotService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
