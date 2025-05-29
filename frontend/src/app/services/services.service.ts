import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ServicesService {
  private apiUrl = 'http://localhost:5000/api/services';

  constructor(private http: HttpClient) {}

  getServices() {
    return this.http.get<any[]>(this.apiUrl);
  }

  getServiceById(id: number) {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  createService(service: any) {
    return this.http.post(this.apiUrl, service);
  }

  updateService(id: number, service: any) {
    return this.http.put(`${this.apiUrl}/${id}`, service);
  }

  deleteService(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
