import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { Container, Card, Form, Button, Alert, Spinner } from 'react-bootstrap';
import axios from 'axios';
import { register } from '../services/authService';
import { useAuth } from '../context/AuthContext';

interface RegisterForm {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
}

const initialForm: RegisterForm = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
};

export default function RegisterPage() {
    const [form, setForm] = useState<RegisterForm>(initialForm);
    const [formErrors, setFormErrors] = useState({ firstName: '', lastName: '', email: '', password: '' });
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    const { login: setAuthUser } = useAuth();
    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const handleSubmit = async (e: React.SyntheticEvent) => {
        e.preventDefault();
        setError('');

        if (!form.firstName.trim()) {
            setFormErrors(prev => ({ ...prev, firstName: 'First name is required, please fill it out.' }));
            return;
        }
        setFormErrors(prev => ({ ...prev, firstName: '' }));

        if (!form.lastName.trim()) {
            setFormErrors(prev => ({ ...prev, lastName: 'Last name is required, please fill it out.' }));
            return;
        }
        setFormErrors(prev => ({ ...prev, lastName: '' }));

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(form.email)) {
            setFormErrors(prev => ({ ...prev, email: 'Please enter a valid email address.' }));
            return;
        }
        setFormErrors(prev => ({ ...prev, email: '' }));

        const passwordErrors: string[] = [];
        if (form.password.length < 6)            passwordErrors.push('at least 6 characters');
        if (!/[A-Z]/.test(form.password))        passwordErrors.push('one uppercase letter');
        if (!/[0-9]/.test(form.password))        passwordErrors.push('one number');
        if (!/[^a-zA-Z0-9]/.test(form.password)) passwordErrors.push('one special character');

        if (passwordErrors.length > 0) {
            setFormErrors(prev => ({ ...prev, password: `Password must contain: ${passwordErrors.join(', ')}.` }));
            return;
        }
        setFormErrors(prev => ({ ...prev, password: '' }));

        setLoading(true);
        try {
            const response = await register(form);
            setAuthUser(response);
            navigate('/profile');
        } catch (err: unknown) {
            if (axios.isAxiosError(err)) {
                if (!err.response) {
                    setError('Unable to connect to server. Please try again later.');
                } else {
                    setError(err.response.data || 'Registration failed. Please try again.');
                }
            } else {
                setError('Something went wrong. Please try again.');
            }
        } finally {
            setLoading(false);
        }
    };

    return (
        <Container className="d-flex justify-content-center align-items-center min-vh-100">
            <Card style={{ width: '420px' }} className="shadow-sm p-4">
                <Card.Body>
                    <h4 className="mb-1 fw-semibold">Sign Up</h4>
                    <p className="text-muted mb-4">Already a member? <Link to="/login" className="text-success">Login</Link></p>

                    {error && <Alert variant="danger">{error}</Alert>}

                    <Form onSubmit={handleSubmit} noValidate>
                        <Form.Group className="mb-3">
                            <Form.Label>First Name</Form.Label>
                            <Form.Control
                                type="text"
                                name="firstName"
                                placeholder="John"
                                value={form.firstName}
                                onChange={handleChange}
                                isInvalid={!!formErrors.firstName}
                                required
                            />
                            {formErrors.firstName && (
                                <Form.Text className="text-danger">{formErrors.firstName}</Form.Text>
                            )}
                        </Form.Group>

                        <Form.Group className="mb-3">
                            <Form.Label>Last Name</Form.Label>
                            <Form.Control
                                type="text"
                                name="lastName"
                                placeholder="Doe"
                                value={form.lastName}
                                onChange={handleChange}
                                isInvalid={!!formErrors.lastName}
                                required
                            />
                            {formErrors.lastName && (
                                <Form.Text className="text-danger">{formErrors.lastName}</Form.Text>
                            )}
                        </Form.Group>

                        <Form.Group className="mb-3">
                            <Form.Label>Email</Form.Label>
                            <Form.Control
                                type="email"
                                name="email"
                                placeholder="faheem@example.com"
                                value={form.email}
                                onChange={handleChange}
                                isInvalid={!!formErrors.email}
                                required
                            />
                            {formErrors.email && (
                                <Form.Text className="text-danger">{formErrors.email}</Form.Text>
                            )}
                        </Form.Group>

                        <Form.Group className="mb-4">
                            <Form.Label>Password</Form.Label>
                            <Form.Control
                                type="password"
                                name="password"
                                placeholder="Enter your password"
                                value={form.password}
                                onChange={handleChange}
                                isInvalid={!!formErrors.password}
                                required
                            />
                            {formErrors.password && (
                                <Form.Text className="text-danger">{formErrors.password}</Form.Text>
                            )}
                        </Form.Group>

                        <Button
                            type="submit"
                            variant="success"
                            className="w-100"
                            disabled={loading}
                        >
                            {loading
                                ? <><Spinner size="sm" className="me-2" />Signing up...</>
                                : 'Sign up'
                            }
                        </Button>
                    </Form>
                </Card.Body>
            </Card>
        </Container>
    );
}
