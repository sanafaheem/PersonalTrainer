import { Navbar, Nav, Container, Button } from 'react-bootstrap';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

export default function NavBar() {
  const { user, isLoggedIn, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };
  const handleMyWorkouts = () => {
  if (!isLoggedIn) {
    navigate('/login', { state: { message: 'Sign in to view your saved workouts' } });
  } else {
    navigate('/my-workouts');
  }
};

  return (
    <Navbar bg="dark" variant="dark" expand="lg">
      <Container>
        <Navbar.Brand as={Link} to="/">Personal Trainer</Navbar.Brand>
        <Navbar.Toggle aria-controls="main-navbar" />
        <Navbar.Collapse id="main-navbar">
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/workout">Create Workout</Nav.Link>
            <Nav.Link onClick={handleMyWorkouts}>My Workouts</Nav.Link>
          </Nav>

          <Nav className="ms-auto align-items-center">
            {isLoggedIn ? (
              <>
                <Nav.Link as={Link} to="/profile" className="text-white me-3">
                  {user?.firstName} {user?.lastName}
                </Nav.Link>
                <Button variant="outline-light" size="sm" onClick={handleLogout}>
                  Logout
                </Button>
              </>
            ) : (
              <Button variant="outline-light" size="sm" onClick={() => navigate('/login')}>
                Login
              </Button>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}
