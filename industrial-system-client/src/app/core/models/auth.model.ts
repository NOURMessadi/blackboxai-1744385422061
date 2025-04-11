export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  user: UserDto;
}

export interface UserDto {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
}