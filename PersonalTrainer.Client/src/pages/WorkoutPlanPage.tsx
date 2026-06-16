import { useLocation, useNavigate } from 'react-router-dom';
import { Container, Card, Button, Badge, ListGroup } from 'react-bootstrap';
import type { WorkoutPlan } from '../services/WorkOutService';

export default function WorkoutPlanPage() {
  const location = useLocation();
  const navigate = useNavigate();
  const plan = location.state?.plan as WorkoutPlan | undefined;

  if (!plan) {
    return (
      <Container className="py-5 text-center">
        <p className="text-muted">No workout plan found.</p>
        <Button variant="dark" onClick={() => navigate('/workout')}>Create a Workout</Button>
      </Container>
    );
  }

  return (
    <Container className="py-5" style={{ maxWidth: '700px' }}>
      <h4 className="fw-bold mb-1">{plan.title}</h4>
      <p className="text-muted mb-4">{plan.motivationalIntro}</p>

      <Card className="mb-3 border-0 bg-light">
        <Card.Body>
          <small className="text-uppercase fw-semibold text-muted">Warm Up</small>
          <p className="mb-0 mt-1">{plan.warmupCue}</p>
        </Card.Body>
      </Card>

      <h5 className="fw-semibold mb-3">Exercises</h5>
      <ListGroup className="mb-3" variant="flush">
        {plan.exercises.map((ex, i) => (
          <ListGroup.Item key={i} className="px-0 py-3 border-bottom">
            <div className="d-flex justify-content-between align-items-start">
              <div>
                <p className="fw-semibold mb-1">{ex.name}</p>
                <p className="text-muted small mb-2">{ex.instructions}</p>
                <div className="d-flex gap-2 flex-wrap">
                  {ex.sets && ex.reps && (
                    <Badge bg="dark">{ex.sets} sets × {ex.reps} reps</Badge>
                  )}
                  {ex.durationSeconds > 0 && (
                    <Badge bg="dark">{ex.durationSeconds}s</Badge>
                  )}
                  <Badge bg="secondary">Rest {ex.restSeconds}s</Badge>
                  <Badge bg="light" text="dark" className="border">{ex.difficulty}</Badge>
                </div>
                <p className="text-muted small mt-2 mb-0">Muscles: {ex.musclesTargeted}</p>
                <p className="fst-italic small text-success mb-0">{ex.encouragementMessage}</p>
              </div>
            </div>
          </ListGroup.Item>
        ))}
      </ListGroup>

      <Card className="mb-4 border-0 bg-light">
        <Card.Body>
          <small className="text-uppercase fw-semibold text-muted">Cool Down</small>
          <p className="mb-0 mt-1">{plan.cooldownCue}</p>
        </Card.Body>
      </Card>

      <Card className="border-0 bg-success bg-opacity-10">
        <Card.Body className="text-center">
          <p className="fw-semibold text-success mb-0">{plan.completionMessage}</p>
        </Card.Body>
      </Card>

      <div className="text-center mt-4">
        <Button variant="outline-dark" onClick={() => navigate('/workout')}>Create Another Workout</Button>
      </div>
    </Container>
  );
}
