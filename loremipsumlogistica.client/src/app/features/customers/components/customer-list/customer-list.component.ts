import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CustomerResponse } from '../../models/response/customer-response.model';
import { CustomerService } from '../../services/customer.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { CustomerAddressesComponent } from '../customer-addresses/customer-addresses.component';
import { CustomerRegistrationComponent } from '../customer-registration/customer-registration.component';
import { AddressRegistrationComponent } from '../address-registration/address-registration.component';
import { CustomerEditComponent } from '../customer-edit/customer-edit.component';
import { EGender } from '../../../../shared/enums/EGender';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  customers: CustomerResponse[] = [];
  displayedColumns: string[] = ['id', 'name', 'dateOfBirth', 'gender', 'actions'];

  constructor(
    private customerService: CustomerService,
    private dialog: MatDialog,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers() {
    this.customerService.getAllCustomers().subscribe(
      (data) => {
        console.log('Loaded customers:', data);
        this.customers = data;
      },
      (error) => {
        console.error('Error loading customers', error);
      }
    );
  }

  addCustomer() {
    const dialogRef = this.dialog.open(CustomerRegistrationComponent, {
      width: '600px',
      maxWidth: '95vw'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadCustomers();
      }
    });
  }

  addAddress(customer: CustomerResponse) {
    const dialogRef = this.dialog.open(AddressRegistrationComponent, {
      width: '600px',
      maxWidth: '95vw',
      data: { customer }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadCustomers(); 
      }
    });
  }

  viewAddresses(customer: CustomerResponse) {
    this.customerService.getCustomerById(customer.id).subscribe(updatedCustomer => {
      this.dialog.open(CustomerAddressesComponent, {
        width: '1200px',
        maxWidth: '95vw',
        data: { customer: updatedCustomer }
      });
    });
  }

  editCustomer(customer: CustomerResponse) {
    const dialogRef = this.dialog.open(CustomerEditComponent, {
      width: '1000px',
      maxWidth: '95vw',
      data: { customer }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadCustomers();
      }
    });
  }

  deleteCustomer(customer: CustomerResponse) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      maxWidth: '95vw',
      data: { name: customer.name }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.customerService.deleteCustomer(customer.id).subscribe(() => {
          this.loadCustomers();
        });
      }
    });
  }

  getGenderLabel(gender: EGender): string {
    return EGender[gender];
  }
}
