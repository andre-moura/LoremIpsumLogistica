import { EGender } from '../../../../shared/enums/EGender';
import { AddressResponse } from './address-response.model';

export interface CustomerResponse {
  id: number;
  name: string;
  dateOfBirth: Date;
  gender: EGender;
  addresses?: AddressResponse[];
}
