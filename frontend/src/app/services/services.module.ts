import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { ServicesRoutingModule } from './services-routing.module';
import { ServiceListComponent } from './service-list/service-list.component';
import { ServiceFormComponent } from './service-form/service-form.component';
import { DeleteConfirmationComponent } from './delete-confirmation/delete-confirmation.component';

@NgModule({
  declarations: [
    ServiceListComponent,
    ServiceFormComponent,
    DeleteConfirmationComponent,
  ],
  imports: [CommonModule, ServicesRoutingModule, SharedModule],
})
export class ServicesModule {}
