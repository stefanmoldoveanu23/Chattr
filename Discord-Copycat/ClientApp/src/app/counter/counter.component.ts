import { Component, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent implements OnDestroy {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }

  ngOnDestroy() {
    console.log('counter destroyed.');
  }
}
