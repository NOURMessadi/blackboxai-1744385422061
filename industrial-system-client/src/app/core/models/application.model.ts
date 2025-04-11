export interface Application {
  id: number;
  name: string;
  description?: string;
  posteId?: number;
  createdAt: Date;
  updatedAt: Date;
}

export interface ApplicationDto {
  id: number;
  name: string;
  description?: string;
  posteId?: number;
}