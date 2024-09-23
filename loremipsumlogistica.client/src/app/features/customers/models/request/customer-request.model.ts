import { EGender } from '../../../../shared/enums/EGender';
import { AddressRequest } from './address-request.model';

export interface CustomerRequest {
  name: string;
  dateOfBirth: Date;
  gender: EGender;
  addresses?: AddressRequest[];
}
