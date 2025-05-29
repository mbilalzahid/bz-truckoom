import { ServicesService } from '../services.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeleteConfirmationComponent } from '../delete-confirmation/delete-confirmation.component';

@Component({
  selector: 'app-service-list',
  standalone: false,
  templateUrl: './service-list.component.html',
  styleUrl: './service-list.component.scss',
})
export class ServiceListComponent implements OnInit {
  services: any[] = [];
  errorMessage: string = '';

  constructor(
    private servicesService: ServicesService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadServices();
  }

  loadServices(): void {
    this.servicesService.getServices().subscribe({
      next: (data: any[]) => {
        this.services = data;
        console.log('Services loaded:', data);
      },
      error: (error: any) => {
        this.errorMessage = 'Failed to load services';
        console.error(error);
      },
    });
  }

  onAddService(): void {
    this.router.navigate(['/services/add']);
  }

  onEditService(service: any): void {
    this.router.navigate(['/services/edit', service.serviceId]);
  }

  onDeleteService(service: any): void {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '350px',
      data: {
        message: `Are you sure you want to delete service "${service.serviceName}"?`,
      },
    });

    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.servicesService.deleteService(service.serviceId).subscribe({
          next: () => {
            this.loadServices();
            this.snackBar.open('Service deleted successfully', 'Close', {
              duration: 3000,
            });
          },
          error: (error) => {
            console.error(error);
            this.snackBar.open('Failed to delete service', 'Close', {
              duration: 3000,
            });
          },
        });
      }
    });
  }
}
