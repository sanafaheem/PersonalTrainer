import { createContext, useContext, useState, useEffect } from 'react';
import type{ReactNode} from 'react';
import { saveToken, saveRefreshToken, logout as logoutService, isAuthenticated, saveUser, getUser, removeUser } from '../services/authService';
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
  isInitializing: boolean;
  login: (data: AuthResponse) => void;
  logout: () => void;
}
// Create the context
const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<AuthUser | null>(null);
  const [isInitializing, setIsInitializing] = useState(true);

  // On app load restore user from localStorage if token is still valid
  useEffect(() => {
    if (isAuthenticated()) {
      const saved = getUser();
      if (saved) setUser(saved);
    }
    setIsInitializing(false);
  }, []);

  const login = (data: AuthResponse) => {
    saveToken(data.token);
    saveRefreshToken(data.refreshToken);
    const authUser = { firstName: data.firstName, lastName: data.lastName, email: data.email };
    saveUser(authUser);
    setUser(authUser);
  };

  const logout =()=>{
    logoutService();
    removeUser();
    setUser(null);
  };
   return (
    <AuthContext.Provider value={{
      user,
      isLoggedIn: !!user,
      isInitializing,
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
