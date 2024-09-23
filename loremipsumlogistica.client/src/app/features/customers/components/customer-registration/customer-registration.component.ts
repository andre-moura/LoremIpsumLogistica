import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../../services/customer.service';
import { CustomerRequest } from '../../models/request/customer-request.model';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent {
  customerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CustomerRegistrationComponent>,
    private customerService: CustomerService
  ) {
    this.customerForm = this.fb.group({
      name: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      gender: [null, Validators.required]
    });
  }

  onSubmit() {
    if (this.customerForm.valid) {
      const formValue = this.customerForm.value;
      const customerRequest: CustomerRequest = {
        ...formValue,
        gender: Number(formValue.gender)
      };
      this.customerService.createCustomer(customerRequest).subscribe(response => {
        this.dialogRef.close(response);
      });
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
