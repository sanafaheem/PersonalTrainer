import { apiClient, AUTH_URL } from './ApiConfig';


export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  email: string;
  firstName: string;
  lastName: string;
  refreshToken: string;
  refreshTokenExpiration: string;
}

export const login = async (request: LoginRequest): Promise<AuthResponse> => {
  const response = await apiClient.post<AuthResponse>(`${AUTH_URL}/login`, request);
  return response.data;
}

export const register = async (request: RegisterRequest): Promise<AuthResponse> => {
  const response = await apiClient.post<AuthResponse>(`${AUTH_URL}/register`, request);
  return response.data;
}

export const saveToken = (token: string) =>
  localStorage.setItem('pt_token', token);

export const saveRefreshToken = (token: string) =>
  localStorage.setItem('pt_refresh_token', token);

export const getToken = () =>
  localStorage.getItem('pt_token');

export const getRefreshToken = () =>
  localStorage.getItem('pt_refresh_token');

export const logout = () => {
  localStorage.removeItem('pt_token');
  localStorage.removeItem('pt_refresh_token');
}

export const isAuthenticated = (): boolean =>
  !!localStorage.getItem('pt_token');

export const saveUser = (user: { firstName: string; lastName: string; email: string }) =>
  localStorage.setItem('pt_user', JSON.stringify(user));

export const getUser = () => {
  const user = localStorage.getItem('pt_user');
  return user ? JSON.parse(user) : null;
};

export const removeUser = () => localStorage.removeItem('pt_user');

