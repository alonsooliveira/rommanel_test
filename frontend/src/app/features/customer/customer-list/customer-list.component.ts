import { AfterViewInit, Component } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerService } from '../../../services/customer.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-customer-list',
  imports: [CommonModule],
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.css'
})
export class CustomerListComponent implements AfterViewInit {

  customers: any[] = [];

  constructor(
    private customerService: CustomerService,
    private router: Router) {
    
  }

  ngAfterViewInit() {
    this.getAllCustomers();
  }


  getAllCustomers() {
    this.customerService.getAll().subscribe((data) => {
      console.log(data);
      this.customers = data;
    });
  }

  edit(id: string) {
    console.log(id);
    this.router.navigate([`/new/${id}`])
  }

  remove(id: string): void {
    console.log(id);
    this.customerService.delete(id).subscribe({
      complete: () => { this.getAllCustomers(); },
      error: (err: any) => { console.log(err) },
      next: () => { },
    });
  }

  new() {
    this.router.navigate(["/new"])
  }
}
