import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ServiceListComponent } from './service-list.component';
import { ServicesService } from '../services.service';
import { of, throwError } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

describe('ServiceListComponent', () => {
  let component: ServiceListComponent;
  let fixture: ComponentFixture<ServiceListComponent>;
  let mockService: jasmine.SpyObj<ServicesService>;

  beforeEach(async () => {
    mockService = jasmine.createSpyObj('ServicesService', ['getServices']);

    await TestBed.configureTestingModule({
      declarations: [ServiceListComponent],
      imports: [RouterTestingModule, MatIconModule, MatButtonModule],
      providers: [{ provide: ServicesService, useValue: mockService }],
    }).compileComponents();

    fixture = TestBed.createComponent(ServiceListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load services on init', () => {
    const mockData = [
      { serviceName: 'Oil Change', serviceDate: '2025-06-01', tasks: [] },
    ];
    mockService.getServices.and.returnValue(of(mockData));

    fixture.detectChanges();

    expect(component.services.length).toBe(1);
    expect(component.services[0].serviceName).toBe('Oil Change');
  });

  it('should handle error on service load', () => {
    mockService.getServices.and.returnValue(
      throwError(() => new Error('Error'))
    );

    fixture.detectChanges();

    expect(component.errorMessage).toBe('Failed to load services');
  });
});
