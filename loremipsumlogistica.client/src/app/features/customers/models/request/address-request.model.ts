import { EAddressType } from "../../../../shared/enums/EAddressType";

export interface AddressRequest {
  id: number;
  zipCode: string;
  street: string;
  number: string;
  complement?: string;
  neighborhood?: string;
  city: string;
  state: string;
  addressType: EAddressType;
} 
