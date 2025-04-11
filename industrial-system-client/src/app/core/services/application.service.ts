import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ApplicationDto } from '../models/application.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  private readonly basePath = 'applications';

  constructor(private api: ApiService) {}

  getAll(): Observable<ApplicationDto[]> {
    return this.api.get<ApplicationDto[]>(this.basePath);
  }

  getById(id: number): Observable<ApplicationDto> {
    return this.api.get<ApplicationDto>(`${this.basePath}/${id}`);
  }

  create(application: ApplicationDto): Observable<ApplicationDto> {
    return this.api.post<ApplicationDto>(this.basePath, application);
  }

  update(id: number, application: ApplicationDto): Observable<ApplicationDto> {
    return this.api.put<ApplicationDto>(`${this.basePath}/${id}`, application);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`${this.basePath}/${id}`);
  }

  getByPosteId(posteId: number): Observable<ApplicationDto[]> {
    return this.api.get<ApplicationDto[]>(`${this.basePath}/poste/${posteId}`);
  }
}