import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../../services/customer.service';
import { AddressRequest } from '../../models/request/address-request.model';
import { EAddressType } from '../../../../shared/enums/EAddressType';
import { CustomerResponse } from '../../models/response/customer-response.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-address-registration',
  templateUrl: './address-registration.component.html',
  styleUrls: ['./address-registration.component.css']
})
export class AddressRegistrationComponent {
  addressForm: FormGroup;
  addressTypes: { value: number; label: string }[];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddressRegistrationComponent>,
    private customerService: CustomerService,
    @Inject(MAT_DIALOG_DATA) public data: { customer: CustomerResponse },
    private http: HttpClient
  ) {
    this.addressForm = this.fb.group({
      zipCode: ['', Validators.required],
      street: ['', Validators.required],
      number: ['', [Validators.required, Validators.pattern('^[0-9]+$')]], // Number validation
      complement: [''],
      neighborhood: [''],
      city: ['', Validators.required],
      state: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(2)]], // UF validation
      addressType: [EAddressType.Residential, Validators.required]
    });

    this.addressTypes = Object.keys(EAddressType)
      .filter(key => isNaN(Number(key)))
      .map(key => ({ value: EAddressType[key as keyof typeof EAddressType], label: key }));
  }

  onSubmit() {
    if (this.addressForm.valid) {
      const newAddress: AddressRequest = {
        id: 0,
        ...this.addressForm.value,
        addressType: Number(this.addressForm.value.addressType)
      };

      if (this.data.customer && this.data.customer.addresses) {
        this.data.customer.addresses.push(newAddress);
      } else {
        this.data.customer.addresses = [newAddress];
      }

      this.customerService.updateCustomer(this.data.customer.id, this.data.customer).subscribe(() => {
        this.dialogRef.close(true);
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
