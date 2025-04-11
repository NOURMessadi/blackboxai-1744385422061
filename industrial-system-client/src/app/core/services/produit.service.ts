import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ProduitDto } from '../models/produit.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProduitService {
  private readonly basePath = 'produits';

  constructor(private api: ApiService) {}

  getAll(): Observable<ProduitDto[]> {
    return this.api.get<ProduitDto[]>(this.basePath);
  }

  getById(id: number): Observable<ProduitDto> {
    return this.api.get<ProduitDto>(`${this.basePath}/${id}`);
  }

  create(produit: ProduitDto): Observable<ProduitDto> {
    return this.api.post<ProduitDto>(this.basePath, produit);
  }

  update(id: number, produit: ProduitDto): Observable<ProduitDto> {
    return this.api.put<ProduitDto>(`${this.basePath}/${id}`, produit);
  }

  delete(id: number): Observable<void> {
    return this.api.delete<void>(`${this.basePath}/${id}`);
  }

  getByTypeProduit(typeProduitId: number): Observable<ProduitDto[]> {
    return this.api.get<ProduitDto[]>(`${this.basePath}/type/${typeProduitId}`);
  }

  getByFamilleProduit(familleProduitId: number): Observable<ProduitDto[]> {
    return this.api.get<ProduitDto[]>(`${this.basePath}/famille/${familleProduitId}`);
  }

  search(searchTerm: string): Observable<ProduitDto[]> {
    return this.api.get<ProduitDto[]>(`${this.basePath}/search?term=${searchTerm}`);
  }
}