import { CommonModule, DOCUMENT } from '@angular/common';
import { AfterViewInit, Component, ElementRef, Inject } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../../../services/customer.service';
import { AddressModel, CustomerModel } from '../../../models/customer.model';
import { PostalCodeService } from '../../../services/postal-code.service';
import { PostalCodeModel } from '../../../models/postalCode.model';

@Component({
  selector: 'app-customer-form',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './customer-form.component.html',
  styleUrl: './customer-form.component.css'
})
export class CustomerFormComponent implements AfterViewInit {
  customerForm: FormGroup;
  private paramId: string | null
  errorToAddCustomer: Array<string> = [];
  errorToAddAdddress: string = "";
  allowRegisterNumber: boolean = false;

  states = [
    { sigla: 'AC', nome: 'Acre' },
    { sigla: 'AL', nome: 'Alagoas' },
    { sigla: 'AP', nome: 'Amapá' },
    { sigla: 'AM', nome: 'Amazonas' },
    { sigla: 'BA', nome: 'Bahia' },
    { sigla: 'CE', nome: 'Ceará' },
    { sigla: 'DF', nome: 'Distrito Federal' },
    { sigla: 'ES', nome: 'Espírito Santo' },
    { sigla: 'GO', nome: 'Goiás' },
    { sigla: 'MA', nome: 'Maranhão' },
    { sigla: 'MT', nome: 'Mato Grosso' },
    { sigla: 'MS', nome: 'Mato Grosso do Sul' },
    { sigla: 'MG', nome: 'Minas Gerais' },
    { sigla: 'PA', nome: 'Pará' },
    { sigla: 'PB', nome: 'Paraíba' },
    { sigla: 'PR', nome: 'Paraná' },
    { sigla: 'PE', nome: 'Pernambuco' },
    { sigla: 'PI', nome: 'Piauí' },
    { sigla: 'RJ', nome: 'Rio de Janeiro' },
    { sigla: 'RN', nome: 'Rio Grande do Norte' },
    { sigla: 'RS', nome: 'Rio Grande do Sul' },
    { sigla: 'RO', nome: 'Rondônia' },
    { sigla: 'RR', nome: 'Roraima' },
    { sigla: 'SC', nome: 'Santa Catarina' },
    { sigla: 'SP', nome: 'São Paulo' },
    { sigla: 'SE', nome: 'Sergipe' },
    { sigla: 'TO', nome: 'Tocantins' }
  ];


  get addresses(): FormArray {
    return this.customerForm.get("addresses") as FormArray;
  }

  constructor(
    private fb: FormBuilder,
    public router: Router,
    private route: ActivatedRoute,
    private customerService: CustomerService,
    private postalCodeService: PostalCodeService) {
    this.paramId = this.route.snapshot.paramMap.get('id');

    this.customerForm = this.fb.group({
      id: [null],
      name: [null, [Validators.required]],
      documentNumber: [null, [Validators.required]],
      registerNumber: [null],
      birthDate: [null, [Validators.required]],
      cellphone: [null, [Validators.required]],
      email: [null, [Validators.required]],
      customerType: [null, [Validators.required]],
      addresses: this.fb.array([this.addressFormGroup(new AddressModel())]),
    });
  }

  ngAfterViewInit() {
    if (this.paramId) {
      this.getCustomerById();
    }
  }

  isCompany(): boolean {
    return this.customerForm.get("customerType")?.value == 2;
  }

  getCustomerById() {
    this.customerService.getById(this.paramId!).subscribe((data : CustomerModel) => {
      console.log(data);
      this.customerForm.patchValue(data);
      this.customerForm.get("customerType")?.disable();
    });
  }

  addressFormGroup(address: AddressModel) {
    return this.fb.group({
      id: [address.id],
      name: [address.name, [Validators.required]],
      postalCode: [address.postalCode, [Validators.required]],
      street: [address.street, [Validators.required]],
      number: [address.number, [Validators.required]],
      complement: [address.complement],
      neighborhood: [address.neighborhood, [Validators.required]],
      city: [address.city, [Validators.required]],
      state: [address.state, [Validators.required]],
    });
  }

  addAddressFormGroup() {
    let ultimoIndex = this.addresses.length > 0 ? (this.addresses.length - 1) : 0;
    if (this.addresses.controls[ultimoIndex].invalid) {
      this.errorToAddAdddress = "Campos obrigatórios do Endereço não preenchidos"
      return;
    }

    this.addresses.push(this.addressFormGroup(new AddressModel()));
  }

  removeAddress(index: number) {
    this.addresses.removeAt(index);
  }

  save() {
    this.errorToAddCustomer = [];
    if (this.customerForm.invalid) {
      this.markControlsDirty(this.customerForm);
      this.errorToAddCustomer.push("Campos obrigatórios não preechidos");
      return;
    }

    if (!this.paramId) {
      this.customerForm.get("customerType")?.setValue(parseInt(this.customerForm.get("customerType")?.value));
      this.customerService.add(this.customerForm.getRawValue()).subscribe({
        complete: () => { this.router.navigate(["/"]) },
        error: (err: any) => {
          err.error.errors.forEach((e: string) => {
            this.errorToAddCustomer.push(e);
          })
        },
        next: () => { },
      });
    } else {
      this.customerForm.get("id")?.setValue(this.paramId);
      this.customerService.update(this.customerForm.getRawValue()).subscribe({
        complete: () => { this.router.navigate(["/"]) },
        error: (err: any) => {
          err.error.errors.forEach((e: string) => {
            this.errorToAddCustomer.push(e);
          })
        },
        next: () => { },
      });
    }
  }

  cancelar() {
    this.router.navigate(["/"])
  }

  fillPostalCode(index: number) {
    this.postalCodeService.getPostalCode(this.addresses.controls[index].get("postalCode")?.value).subscribe((data: PostalCodeModel) => {
      this.addresses.controls[index].get("street")?.setValue(data.logradouro)
      this.addresses.controls[index].get("neighborhood")?.setValue(data.bairro)
      this.addresses.controls[index].get("city")?.setValue(data.localidade)
      this.addresses.controls[index].get("state")?.setValue(data.uf?.toUpperCase())
    });
  }

  fillRegisterNumber(event: any) {
    this.customerForm.get("registerNumber")?.setValue("");
    this.customerForm.get("registerNumber")?.enable()
    if (event.target.checked) {
      this.customerForm.get("registerNumber")?.setValue("ISENTO");
      this.customerForm.get("registerNumber")?.disable()
    }
  }

  public markControlsDirty(group: FormGroup | FormArray): void {
    Object.keys(group.controls).forEach((key: string) => {
      const abstractControl = group.get(key);

      if (abstractControl instanceof FormGroup || abstractControl instanceof FormArray) {
        this.markControlsDirty(abstractControl);
      } else {
        abstractControl?.markAsTouched();
      }
    });
  }
}
