import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../employee.service';
import { SharedManagerService } from '../shared-manager.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})

export class EmployeeListComponent implements OnInit {
  employees: any[] = [];
  managers: any[] = [];
  selectedManager: string = '';

  constructor(private employeeService: EmployeeService, private sharedManagerService: SharedManagerService) {}

  ngOnInit(): void {
    this.sharedManagerService.selectedManager$.subscribe((managerId) => {
      this.selectedManager = managerId;
      this.loadEmployees();
    });

    this.loadManagers();
  }

  // ngOnInit(): void {
  //   this.loadManagers();
  //   this.loadEmployees();
  // }

  loadManagers() {
    this.employeeService.getManagers()?.subscribe({
      next: (data) => {
        this.managers = data;
      },
      error: () => {
        this.managers = [];
      }
    });
  }

  loadEmployees() {
    if (this.selectedManager) {
      this.employeeService.getEmployeesByManager(this.selectedManager)?.subscribe({
        next: (data) => {
          this.employees = data;
        },
        error: () => {
          this.employees = [];
        }
      });
    } else {
      this.employeeService.getEmployees()?.subscribe({
        next: (data) => {
          this.employees = data;
        },
        error: () => {
          this.employees = [];
        }
      });
    }
  }

  onManagerChange() {
    this.sharedManagerService.setSelectedManager(this.selectedManager);
    this.loadEmployees();
  }
}

