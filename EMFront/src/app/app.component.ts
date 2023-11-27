import { Component, OnInit } from '@angular/core';
import { SharedManagerService } from './shared-manager.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})


// export class AppComponent implements OnInit {

//   constructor() {}

//   ngOnInit() {  }

//   title = 'Employee Management';
// }


export class AppComponent implements OnInit {
  selectedManager: string = '';

  constructor(private sharedManagerService: SharedManagerService) {}

  ngOnInit(): void {
    this.sharedManagerService.selectedManager$.subscribe((managerId) => {
      this.selectedManager = managerId;
    });
  }
}
