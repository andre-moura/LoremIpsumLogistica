import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../../services/customer.service';
import { CustomerRequest } from '../../models/request/customer-request.model';
import { CustomerResponse } from '../../models/response/customer-response.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Inject } from '@angular/core';

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.css']
})
export class CustomerEditComponent implements OnInit {
  customerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CustomerEditComponent>,
    private customerService: CustomerService,
    @Inject(MAT_DIALOG_DATA) public data: { customer: CustomerResponse }
  ) {
    this.customerForm = this.fb.group({
      name: [this.data.customer.name, Validators.required],
      dateOfBirth: [this.data.customer.dateOfBirth, Validators.required],
      gender: [this.data.customer.gender, Validators.required]
    });

    console.log(this.customerForm.status);
  }

  ngOnInit(): void { }

  onSubmit() {
    if (this.customerForm.valid) {
      const formValue = this.customerForm.value;
      const customerRequest: CustomerRequest = {
        ...formValue,
        gender: Number(formValue.gender) 
      };

      this.customerService.getCustomerById(this.data.customer.id).subscribe(existingCustomer => {
        const updatedCustomer = {
          ...existingCustomer,
          ...customerRequest
        };

        this.customerService.updateCustomer(this.data.customer.id, updatedCustomer).subscribe(() => {
          this.dialogRef.close(updatedCustomer); 
        });
      });
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
