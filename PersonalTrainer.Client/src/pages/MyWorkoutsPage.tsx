import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Row, Col, Card, Button, Badge, Spinner } from 'react-bootstrap';
import { getMyPlans, getPlanById } from '../services/WorkOutService';
import type { WorkoutPlanSummary } from '../services/WorkOutService';

export default function MyWorkoutsPage() {
  const navigate = useNavigate();
  const [plans, setPlans] = useState<WorkoutPlanSummary[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [starting, setStarting] = useState<number | null>(null);

  const handleStartWorkout = async (id: number) => {
    setStarting(id);
    try {
      const plan = await getPlanById(id);
      sessionStorage.setItem('workoutPlan', JSON.stringify(plan));
      navigate('/workout/plan');
    } finally {
      setStarting(null);
    }
  };

  useEffect(() => {
    getMyPlans()
      .then(setPlans)
      .catch(() => setError('Failed to load your workout plans.'))
      .finally(() => setLoading(false));
  }, []);

  if (loading) {
    return (
      <Container className="py-5 text-center">
        <Spinner animation="border" variant="dark" />
      </Container>
    );
  }

  if (error) {
    return (
      <Container className="py-5 text-center">
        <p className="text-danger">{error}</p>
      </Container>
    );
  }

  if (plans.length === 0) {
    return (
      <Container className="py-5 text-center">
        <p className="text-muted mb-3">You haven't generated any workout plans yet.</p>
        <Button variant="dark" onClick={() => navigate('/workout')}>Create Your First Workout</Button>
      </Container>
    );
  }

  return (
    <Container className="py-5">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h4 className="fw-bold mb-0">My Workouts</h4>
        <Button variant="dark" size="sm" onClick={() => navigate('/workout')}>+ New Workout</Button>
      </div>

      <Row className="g-3">
        {plans.map(plan => (
          <Col key={plan.id} xs={12} md={6} lg={4}>
            <Card className="h-100 shadow-sm">
              <Card.Body className="d-flex flex-column p-4">
                <h5 className="fw-bold mb-1">{plan.title}</h5>
                <p className="text-muted small mb-3">{plan.motivationalIntro}</p>

                <div className="d-flex gap-2 mb-3">
                  <Badge bg="dark">{plan.exerciseCount} exercises</Badge>
                  <Badge bg="light" text="dark" className="border">
                    {new Date(plan.createdAt).toLocaleDateString()}
                  </Badge>
                </div>

                <div className="mt-auto">
                  <Button
                    variant="dark"
                    className="w-100"
                    disabled={starting === plan.id}
                    onClick={() => handleStartWorkout(plan.id)}
                  >
                    {starting === plan.id ? 'Loading...' : 'Start Workout'}
                  </Button>
                </div>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
}
