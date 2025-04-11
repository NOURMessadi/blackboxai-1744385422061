export interface Produit {
  id: number;
  name: string;
  description?: string;
  typeProduitId: number;
  familleProduitId: number;
  createdAt: Date;
  updatedAt: Date;
}

export interface ProduitDto {
  id: number;
  name: string;
  description?: string;
  typeProduitId: number;
  familleProduitId: number;
}