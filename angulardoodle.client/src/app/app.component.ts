import { Component, OnInit } from '@angular/core';
import { Unit } from './units/unit.model';
import { MessageService, Message } from './message.service';


@Component({
  selector: 'app-root',
  templateUrl: `./app.component.html`,
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  // Naming of title
  title = 'AngularDoodle.client';


  // Initiating variable message
  message: string = '';

  // Constructor to set message from service
  constructor(private messageService: MessageService) { }


  ngOnInit() {
    this.getMessage();
  }

  // Function to set message to message from service
  getMessage() {
    this.messageService.getMessageFromServer().subscribe(
      (response: Message) => {
        this.message = response.text;
      },
      (error) => {
        console.error('Error fetching message from server', error)
      }
    );
  }

  // Dummy data
  units: Unit[] = [
    { id: 1, name: 'Formaldehyde', casNumber: '50-00-0', amount: 10, location: 'Location 1' },
    { id: 2, name: 'Ethanol', casNumber: '64-17-5', amount: 20, location: 'Location 2' },
    { id: 3, name: 'Methanol', casNumber: '67-56-1', amount: 30, location: 'Location 3' },
    { id: 4, name: 'Acetone', casNumber: '67-64-1', amount: 40, location: 'Location 4' },
    { id: 5, name: 'Benzene', casNumber: '71-43-2', amount: 50, location: 'Location 5' },
    { id: 6, name: 'Toluene', casNumber: '108-88-3', amount: 60, location: 'Location 6' },
    { id: 7, name: 'Chloroform', casNumber: '67-66-3', amount: 70, location: 'Location 7' },
    { id: 8, name: 'Hexane', casNumber: '110-54-3', amount: 80, location: 'Location 8' },
    { id: 9, name: 'Phenol', casNumber: '108-95-2', amount: 90, location: 'Location 9' },
    { id: 10, name: 'Acetic Acid', casNumber: '64-19-7', amount: 100, location: 'Location 10' },
    { id: 11, name: 'Formaldehyde', casNumber: '50-00-0', amount: 15, location: 'Location 11' },
    { id: 12, name: 'Ethanol', casNumber: '64-17-5', amount: 25, location: 'Location 12' },
    { id: 13, name: 'Methanol', casNumber: '67-56-1', amount: 35, location: 'Location 13' },
    { id: 14, name: 'Acetone', casNumber: '67-64-1', amount: 45, location: 'Location 14' },
    { id: 15, name: 'Benzene', casNumber: '71-43-2', amount: 55, location: 'Location 15' },
    { id: 16, name: 'Toluene', casNumber: '108-88-3', amount: 65, location: 'Location 16' },
    { id: 17, name: 'Chloroform', casNumber: '67-66-3', amount: 75, location: 'Location 17' },
    { id: 18, name: 'Hexane', casNumber: '110-54-3', amount: 85, location: 'Location 18' },
    { id: 19, name: 'Phenol', casNumber: '108-95-2', amount: 95, location: 'Location 19' },
    { id: 20, name: 'Acetic Acid', casNumber: '64-19-7', amount: 105, location: 'Location 20' },
    { id: 21, name: 'Formaldehyde', casNumber: '50-00-0', amount: 10, location: 'Location 1' },
    { id: 22, name: 'Ethanol', casNumber: '64-17-5', amount: 20, location: 'Location 2' },
    { id: 23, name: 'Methanol', casNumber: '67-56-1', amount: 30, location: 'Location 3' },
    { id: 24, name: 'Acetone', casNumber: '67-64-1', amount: 40, location: 'Location 4' },
    { id: 25, name: 'Benzene', casNumber: '71-43-2', amount: 50, location: 'Location 5' }
  ];

  // Sorting of units
  sortColumn: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  sortUnits(column: string) {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    this.units.sort((a, b) => {
      const valueA = a[column as keyof Unit];
      const valueB = b[column as keyof Unit];

      if (valueA < valueB) {
        return this.sortDirection === 'asc' ? -1 : 1;
      } else if (valueA > valueB) {
        return this.sortDirection === 'asc' ? 1 : -1;
      } else {
        return 0;
      }
    });
  }
}
