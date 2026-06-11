import { createContext, useContext, useState, useEffect } from 'react';
import type{ReactNode} from 'react';
import { saveToken, saveRefreshToken, logout as logoutService, isAuthenticated, getToken } from '../services/authService';
import type { AuthResponse } from '../services/authService';


// Shape of the user object we store
interface AuthUser {
  firstName: string;
  lastName:string;
  email: string;
}

// Shape of everything the context provides
interface AuthContextType {
  user: AuthUser | null;
  isLoggedIn: boolean;
  login: (data: AuthResponse) => void;
  logout: () => void;
}
// Create the context
const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<AuthUser | null>(null);

  // On app load check if user is already logged in
  useEffect(() => {
    if (isAuthenticated()) {
      const token = getToken();
      // Decode token to get user info
      if (token) {
        const payload = JSON.parse(atob(token.split('.')[1]));
        setUser({
          firstName: payload.firstName || '',
          lastName: payload.lastName || '',
          email: payload.email
        });
      }
    }
  }, []);

  const login = (data: AuthResponse) => {
    saveToken(data.token);
    saveRefreshToken(data.refreshToken);
    setUser({
      firstName: data.firstName,
      lastName: data.lastName,
      email: data.email
    });
  };

  const logout =()=>{
    logoutService();
    setUser(null);
  };
   return (
    <AuthContext.Provider value={{
      user,
      isLoggedIn: !!user,
      login,
      logout
    }}>
      {children}
    </AuthContext.Provider>
  );

}

// Custom hook — how components access the context
export function useAuth() {
  const context = useContext(AuthContext);
  if (!context)
    throw new Error('useAuth must be used within AuthProvider');
  return context;
}
