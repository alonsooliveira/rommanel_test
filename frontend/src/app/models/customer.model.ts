export class CustomerModel {
    id?: string;
    name?: string;
    documentNumber?: string;
    birthDate?: Date;
    cellphone?: string;
    email?: string;
    customerType?: number;
    addresses?: Array<AddressModel>;
}

export class AddressModel {
    id?: string;
    name?: string;
    postalCode?: string;
    street?: string;
    number?: string;
    complement?: string;
    neighborhood?: string;
    city?: string; 
    state?: string;
}