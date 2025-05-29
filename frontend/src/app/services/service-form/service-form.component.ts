import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from '../services.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-service-form',
  standalone: false,
  templateUrl: './service-form.component.html',
  styleUrl: './service-form.component.scss',
})
export class ServiceFormComponent implements OnInit {
  serviceForm!: FormGroup;
  isEditMode = false;
  serviceId!: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private servicesService: ServicesService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.initForm();

    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.isEditMode = true;
        this.serviceId = +id;
        this.loadService(this.serviceId);
      }
    });
  }

  initForm() {
    this.serviceForm = this.fb.group({
      serviceName: ['', Validators.required],
      serviceDate: ['', Validators.required],
      tasks: this.fb.array([]),
    });

    this.addTask(); // Start with 1 task
  }

  get tasks(): FormArray {
    return this.serviceForm.get('tasks') as FormArray;
  }

  newTask(): FormGroup {
    return this.fb.group({
      taskName: ['', Validators.required],
      description: [''],
      remarks: [''],
    });
  }

  addTask(): void {
    this.tasks.push(this.newTask());
  }

  removeTask(index: number): void {
    this.tasks.removeAt(index);
  }

  loadService(id: number): void {
    this.servicesService.getServiceById(id).subscribe((service) => {
      this.serviceForm.patchValue({
        serviceName: service.serviceName,
        serviceDate: service.serviceDate,
      });

      this.tasks.clear();
      service.tasks.forEach((task: any) => {
        this.tasks.push(
          this.fb.group({
            taskName: [task.taskName, Validators.required],
            description: [task.description],
            remarks: [task.remarks],
          })
        );
      });
    });
  }

  // onSubmit(): void {
  //   if (this.serviceForm.invalid) return;

  //   const serviceData = this.serviceForm.value;

  //   if (this.isEditMode) {
  //     this.servicesService
  //       .updateService(this.serviceId, serviceData)
  //       .subscribe(() => {
  //         this.router.navigate(['/services']);
  //       });
  //   } else {
  //     this.servicesService.createService(serviceData).subscribe(() => {
  //       this.router.navigate(['/services']);
  //     });
  //   }
  // }
  onSubmit(): void {
    if (this.serviceForm.invalid) return;

    const serviceData = this.serviceForm.value;

    if (this.isEditMode) {
      this.servicesService
        .updateService(this.serviceId, serviceData)
        .subscribe({
          next: () => {
            this.snackBar.open('Service updated successfully', 'Close', {
              duration: 3000,
            });
            this.router.navigate(['/services']);
          },
          error: () => {
            this.snackBar.open('Failed to update service', 'Close', {
              duration: 3000,
            });
          },
        });
    } else {
      this.servicesService.createService(serviceData).subscribe({
        next: () => {
          this.snackBar.open('Service created successfully', 'Close', {
            duration: 3000,
          });
          this.router.navigate(['/services']);
        },
        error: () => {
          this.snackBar.open('Failed to create service', 'Close', {
            duration: 3000,
          });
        },
      });
    }
  }
}
