import { Component, HostListener, ElementRef } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Unit } from './units/types/unit.type';
import { UnitService } from './units/utils/unit.service';
import { MessageService, Message } from './message.service';
import { Page } from './units/types/page.type';


@Component({
  selector: 'app-root',
  templateUrl: `./app.component.html`,
  styleUrls: ['./app.component.css'],
  imports: [FormsModule],
})
export class AppComponent {
  // Naming of title
  title = 'Kemibrug';


  // Initiating variable message
  message: string = '';

  // Initiating variables for unit
  currentUnits: Unit[] = [];
  sortColumn: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  // Initiating variables for pagination
  currentPage: number = 1;
  pageSize: number = 25;
  totalPages: number = 0;
  pages: Page[] = [];

  // Initiating variables for search
  searchName: string = '';
  searchCas: string = '';
  searchAmount: number | null = null;
  searchLocation: string = '';

  // Constructor for services
  constructor(
    private messageService: MessageService,
    private unitService: UnitService
  ) {}

  // Gets data from server on init
  ngOnInit() {
    this.setViewportHeight();
  }


  @HostListener('window:resize')
  onResize() {
    this.setViewportHeight();    
  }

  setViewportHeight() {
    const vh = window.innerHeight;
    document.documentElement.style.setProperty('--vh', `${vh}px`);
  }

  // Function to set units to units from service
  getUnits() {
    this.unitService.getUnits(this.sortColumn, this.sortDirection, this.searchName, this.searchCas, this.searchAmount, this.searchLocation, this.currentPage, this.pageSize).subscribe(
      (response) => {
        console.log('API Response:', response);
        this.currentUnits = response.units;
        this.totalPages = response.totalPages;
        this.currentPage = response.pageNumber;
        this.pages = Array.from({ length: this.totalPages }, (_, i) => ({ pageNumber: i + 1 }));
        console.log('Units fetched successfully', response);
      },
      (error) => {
        console.error('Error fetching units from server', error);
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

  applyFilters() {
    this.getUnits();
  }

  clearFilters() {
      this.searchName = '',
      this.searchCas = '',
      this.searchAmount = null,
      this.searchLocation = ''
    

    this.sortColumn = '';
    this.sortDirection = 'asc';

    this.currentPage = 1;

    this.getUnits();
  }

  goToPage(pageNumber: number) {
    this.currentPage = pageNumber;
    this.getUnits();
  }
}
