import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AddressRequest } from '../../models/request/address-request.model';
import { CustomerResponse } from '../../models/response/customer-response.model';
import { CustomerService } from '../../services/customer.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { AddressEditComponent } from '../address-edit/address-edit.component';
import { AddressResponse } from '../../models/response/address-response.model';
import { EAddressType } from '../../../../shared/enums/EAddressType';

@Component({
  selector: 'app-customer-addresses',
  templateUrl: './customer-addresses.component.html',
  styleUrls: ['./customer-addresses.component.css']
})
export class CustomerAddressesComponent implements OnInit {
  customer: CustomerResponse;
  displayedColumns: string[] = ['street', 'number', 'complement', 'neighborhood', 'city', 'state', 'zipCode', 'addressType', 'actions'];
  dataSource: MatTableDataSource<AddressRequest>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private customerService: CustomerService,
    private dialog: MatDialog
  ) {
    this.customer = data.customer;
    this.dataSource = new MatTableDataSource<AddressRequest>(this.customer.addresses || []);
  }

  ngOnInit(): void {
    this.loadAddresses();
  }

  loadAddresses() {
    this.dataSource.data = this.customer.addresses || [];
  }

  deleteAddress(address: AddressRequest) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: { name: 'address' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const updatedAddresses = this.customer.addresses?.filter(a => a !== address) || [];

        const customerRequest = { ...this.customer, addresses: updatedAddresses };

        this.customerService.updateCustomer(this.customer.id, customerRequest).subscribe(() => {
          this.customer.addresses = updatedAddresses;
          this.dataSource.data = updatedAddresses;
        });
      }
    });
  }

  editAddress(address: AddressRequest) {
    const dialogRef = this.dialog.open(AddressEditComponent, {
      width: '400px',
      data: { address: address, customerId: this.customer.id }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.dataSource.data = this.dataSource.data.map(addr =>
          addr.id === result.id ? result : addr
        );

        this.refreshCustomerData();
      }
    });
  }

  refreshCustomerData() {
    this.customerService.getCustomerById(this.customer.id).subscribe(updatedCustomer => {
      this.customer = updatedCustomer;
      this.dataSource.data = this.mapAddressResponseToRequest(this.customer.addresses);
    });
  }

  private mapAddressResponseToRequest(addresses?: AddressResponse[]): AddressRequest[] {
    return addresses ? addresses.map(addr => ({
      id: addr.id,
      zipCode: addr.zipCode,
      street: addr.street,
      number: addr.number,
      complement: addr.complement,
      neighborhood: addr.neighborhood,
      city: addr.city,
      state: addr.state,
      addressType: addr.addressType
    })) : [];
  }

  getAddressTypeLabel(type: EAddressType): string {
    return EAddressType[type];
  }
}
