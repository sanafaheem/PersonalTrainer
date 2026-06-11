import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Card, Form, Button, Alert, Spinner } from 'react-bootstrap';
import axios from 'axios';
import { login } from '../services/authService';
import { useAuth } from '../context/AuthContext';

export default function LoginPage(){
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

   const { login: setAuthUser } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.SyntheticEvent) => {
    e.preventDefault();
    setError('');
    setLoading(true);
    try{
        const response= await login({email,password});
        setAuthUser(response);
        navigate('/profile');
    }catch(err:unknown){
        if (axios.isAxiosError(err)) {
          if (!err.response) {
            setError('Unable to connect to server. Please try again later.');
          } else {
            setError(err.response.data || 'Invalid email or password.');
          }
        } else {
          setError('Something went wrong. Please try again.');
        }
    }
    finally{
      setLoading(false);
    }

  };


  return (
    <Container className="d-flex justify-content-center align-items-center min-vh-100">
      <Card style={{ width: '420px' }} className="shadow-sm p-4">
        <Card.Body>
          <h4 className="mb-1 fw-semibold">Welcome back</h4>
          <p className="text-muted mb-4">Sign in to your Personal Trainer account</p>

          {error && <Alert variant="danger">{error}</Alert>}

          <Form onSubmit={handleSubmit}>
            <Form.Group className="mb-3">
              <Form.Label>Email</Form.Label>
              <Form.Control
                type="email"
                placeholder="faheem@example.com"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
            </Form.Group>

            <Form.Group className="mb-4">
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="password"
                placeholder="Enter your password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </Form.Group>

            <Button
              type="submit"
              variant="success"
              className="w-100"
              disabled={loading}
            >
              {loading
                ? <><Spinner size="sm" className="me-2" />Signing in...</>
                : 'Sign in'
              }
            </Button>
          </Form>

          <p className="text-center text-muted mt-3 mb-0">
            Don't have an account?{' '}
            <a href="/register" className="text-success">Register</a>
          </p>
        </Card.Body>
      </Card>
    </Container>
  );
};


