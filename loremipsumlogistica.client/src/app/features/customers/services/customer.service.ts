import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomerResponse } from '../models/response/customer-response.model';
import { CustomerRequest } from '../models/request/customer-request.model';
import { AddressRequest } from '../models/request/address-request.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = 'https://localhost:7228/api/Customer';

  constructor(private http: HttpClient) { }

  getAllCustomers(): Observable<CustomerResponse[]> {
    return this.http.get<CustomerResponse[]>(this.apiUrl);
  }

  getCustomerById(id: number): Observable<CustomerResponse> {
    return this.http.get<CustomerResponse>(`${this.apiUrl}/${id}`);
  }

  createCustomer(customerRequest: CustomerRequest): Observable<CustomerResponse> {
    return this.http.post<CustomerResponse>(this.apiUrl, customerRequest);
  }

  updateCustomer(id: number, customerRequest: CustomerRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, customerRequest);
  }

  deleteCustomer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  saveAddress(customerId: number, addressRequest: AddressRequest): Observable<void> {
    return addressRequest.id
      ? this.http.put<void>(`${this.apiUrl}/${customerId}/addresses/${addressRequest.id}`, addressRequest)
      : this.http.post<void>(`${this.apiUrl}/${customerId}/addresses`, addressRequest);
  }

  deleteAddress(customerId: number, addressId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${customerId}/addresses/${addressId}`);
  }
}
