import { Routes } from '@angular/router';
import { CustomerListComponent } from './features/customer/customer-list/customer-list.component';
import { CustomerFormComponent } from './features/customer/customer-form/customer-form.component';

export const routes: Routes = [
    { path: '', component: CustomerListComponent },
    { path: 'new', component: CustomerFormComponent },
    { path: 'new/:id', component: CustomerFormComponent }
];
