import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomerService } from '../../services/customer.service';
import { AddressRequest } from '../../models/request/address-request.model';
import { EAddressType } from '../../../../shared/enums/EAddressType';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-address-edit',
  templateUrl: './address-edit.component.html',
  styleUrls: ['./address-edit.component.css']
})
export class AddressEditComponent {
  addressForm: FormGroup;
  addressTypes: { value: number; label: string }[];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddressEditComponent>,
    private customerService: CustomerService,
    @Inject(MAT_DIALOG_DATA) public data: { address: AddressRequest, customerId: number },
    private http: HttpClient
  ) {
    // Initialize the form with the existing address data
    this.addressForm = this.fb.group({
      zipCode: [this.data.address.zipCode, Validators.required],
      street: [this.data.address.street, Validators.required],
      number: [this.data.address.number, Validators.required],
      complement: [this.data.address.complement],
      neighborhood: [this.data.address.neighborhood],
      city: [this.data.address.city, Validators.required],
      state: [this.data.address.state, Validators.required],
      addressType: [this.data.address.addressType, Validators.required]
    });

    // Initialize address types from the enum
    this.addressTypes = Object.keys(EAddressType)
      .filter(key => isNaN(Number(key)))
      .map(key => ({ value: EAddressType[key as keyof typeof EAddressType], label: key }));
  }

  onSubmit() {
    if (this.addressForm.valid) {
      const updatedAddress: AddressRequest = {
        ...this.addressForm.value,
        addressType: Number(this.addressForm.value.addressType),
        id: this.data.address.id
      };

      this.customerService.getCustomerById(this.data.customerId).subscribe(customer => {
        const updatedAddresses = (customer.addresses ?? []).map(addr =>
          addr.id === updatedAddress.id ? updatedAddress : addr
        );

        const customerRequest = { ...customer, addresses: updatedAddresses };

        this.customerService.updateCustomer(customer.id, customerRequest).subscribe(() => {
          this.dialogRef.close(updatedAddress);
        });
      });
    }
  }

  onCancel() {
    this.dialogRef.close();
  }

  fetchAddress() {
    const zipCode = this.addressForm.get('zipCode')?.value;
    if (zipCode) {
      this.http.get<any>(`https://viacep.com.br/ws/${zipCode}/json/`).subscribe(data => {
        if (!data.erro) {
          this.addressForm.patchValue({
            street: data.logradouro,
            neighborhood: data.bairro,
            city: data.localidade,
            state: data.uf
          });
        } else {
          console.error('Invalid ZIP code');
        }
      }, error => {
        console.error('Error fetching address', error);
      });
    }
  }
}
