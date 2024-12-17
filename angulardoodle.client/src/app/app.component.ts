import { Component, OnInit } from '@angular/core';

import { Unit } from './units/types/unit.type';
import { UnitService } from './units/utils/unit.service';
import { MessageService, Message } from './message.service';
import { Page } from './pages/types/page.type';


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

  // Initiating variables for unit
  allUnits: Unit[] = [];
  currentUnits: Unit[] = [];
  totalNumberOfUnits: number = 0;
  sortColumn: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  // Initiating variables for pagination
  currentPage: number = 1;
  pageSize: number = 25;
  totalPages: number = 0;
  pages: Page[] = [];

  // Constructor for services
  constructor(
    private messageService: MessageService,
    private unitService: UnitService
  ) { }

  // Gets data from server on init
  ngOnInit() {
    this.currentPage = 1;
    this.pageSize = 25;
    this.getMessage();
    this.getUnits();
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

  // Function to set units to units from service
  getUnits() {
    this.unitService.getUnits(this.sortColumn, this.sortDirection).subscribe(
      (response: Unit[]) => {
        this.allUnits = response;
        this.totalNumberOfUnits = this.allUnits.length;
        this.totalPages = Math.ceil(this.totalNumberOfUnits / this.pageSize);
        this.pages = Array.from({ length: this.totalPages }, (_, i) => ({ pageNumber: i + 1 }));
        this.updateDisplayedUnits();
      },
      (error) => {
        console.error('Error fetching units from server', error)
      }
    );
  }

  sortUnits(column: string) {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    this.getUnits();
  }

  updateDisplayedUnits() {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.currentUnits = this.allUnits.slice(startIndex, endIndex);
  }

  goToPage(page: number) {
    if (page < 1 || page > this.totalPages) {
      return;
    }
    this.currentPage = page;
    this.updateDisplayedUnits();
  }
}
