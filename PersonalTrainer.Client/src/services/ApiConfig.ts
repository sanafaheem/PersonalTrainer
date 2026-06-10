import axios from 'axios';
import { getToken, getRefreshToken, saveToken, saveRefreshToken, logout } from './authService';

export const BASE_URL = 'http://localhost:5202/api';

export const AUTH_URL = `${BASE_URL}/auth`;
export const PROFILE_URL = `${BASE_URL}/profile`;
export const WORKOUT_URL = `${BASE_URL}/workout`;
export const SESSION_URL = `${BASE_URL}/session`;

// Axios instance with base URL and auth header
export const apiClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});

// Attach JWT token to every request automatically
apiClient.interceptors.request.use((config) => {
  const token = getToken();
  if (token) {
    config.headers['Authorization'] = `Bearer ${token}`;
  }
  return config;
});

// Handle 401 - refresh token automatically
apiClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    const isAuthEndpoint = originalRequest.url?.includes('/auth/');
    if (error.response?.status === 401 && !originalRequest._retry && !isAuthEndpoint) {
      originalRequest._retry = true;

      try {
        const refreshTokenValue = getRefreshToken();
        const response = await axios.post(`${AUTH_URL}/refresh-token`, 
          JSON.stringify(refreshTokenValue), {
          headers: { 'Content-Type': 'application/json' }
        });

        saveToken(response.data.token);
        saveRefreshToken(response.data.refreshToken);

        originalRequest.headers['Authorization'] = `Bearer ${response.data.token}`;
        return apiClient(originalRequest);

      } catch {
        logout();
        window.location.href = '/login';
      }
    }

    return Promise.reject(error);
  }
);