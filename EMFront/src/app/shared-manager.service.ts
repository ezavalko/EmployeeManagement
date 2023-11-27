import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedManagerService {
  private selectedManagerSource = new BehaviorSubject<string>('');
  selectedManager$ = this.selectedManagerSource.asObservable();

  setSelectedManager(managerId: string): void {
    this.selectedManagerSource.next(managerId);
  }
}
