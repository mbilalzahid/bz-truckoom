import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ServiceFormComponent } from './service-form.component';
import { ServicesService } from '../services.service';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('ServiceFormComponent', () => {
  let component: ServiceFormComponent;
  let fixture: ComponentFixture<ServiceFormComponent>;
  let mockService: jasmine.SpyObj<ServicesService>;

  beforeEach(async () => {
    mockService = jasmine.createSpyObj('ServicesService', [
      'getServiceById',
      'createService',
      'updateService',
    ]);

    await TestBed.configureTestingModule({
      declarations: [ServiceFormComponent],
      imports: [ReactiveFormsModule, MatSnackBarModule],
      providers: [
        { provide: ServicesService, useValue: mockService },
        {
          provide: ActivatedRoute,
          useValue: {
            paramMap: of({
              get: () => null, // simulate add mode
            }),
          },
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ServiceFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create form with one task', () => {
    expect(component.serviceForm).toBeTruthy();
    expect(component.tasks.length).toBe(1);
  });

  it('should add a new task', () => {
    component.addTask();
    expect(component.tasks.length).toBe(2);
  });

  it('should remove a task', () => {
    component.addTask();
    component.removeTask(0);
    expect(component.tasks.length).toBe(1);
  });

  it('should call createService on submit in add mode', () => {
    mockService.createService.and.returnValue(of({}));
    component.serviceForm.patchValue({
      serviceName: 'Test Service',
      serviceDate: '2025-06-01T12:00',
    });
    component.onSubmit();
    expect(mockService.createService).toHaveBeenCalled();
  });
});
