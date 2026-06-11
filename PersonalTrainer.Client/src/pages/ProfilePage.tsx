import { Container, Card } from 'react-bootstrap';
import { useAuth } from '../context/AuthContext';


export default function ProfilePage() {
const {user} =useAuth();
  return (
    <Container className='d-flex justify-content-center align-items-center min-vh-100'>
      <Card style={{ width: '420px' }} className="shadow-sm p-4">
        <Card.Body>
          <h4 className="mb-1 fw-semibold">My Profile</h4>
          <p className="text-muted mb-4">Your account details</p>

          <div className="mb-3">
            <small className="text-muted text-uppercase">First Name</small>
            <p className="fw-semibold mb-0">{user?.firstName}</p>
          </div>
          <div className="mb-3">
            <small className="text-muted text-uppercase">Last Name</small>
            <p className="fw-semibold mb-0">{user?.lastName}</p>
          </div>
          <div className="mb-3">
            <small className="text-muted text-uppercase">Email</small>
            <p className="fw-semibold mb-0">{user?.email}</p>
          </div>
        </Card.Body>
      </Card>
    </Container>
  );
}
