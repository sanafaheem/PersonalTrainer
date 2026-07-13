import { useNavigate } from 'react-router-dom';
import { Container, Button, Row, Col, Card } from 'react-bootstrap';
import { useAuth } from '../context/AuthContext';

const features = [
  {
    icon: '🤖',
    title: 'AI-Generated Plans',
    description: 'Tell us your goals and fitness level — Gemini builds a personalised workout in seconds.',
  },
  {
    icon: '⏱️',
    title: 'Guided Sessions',
    description: 'Follow along with per-exercise timers, rest countdowns, and voice coaching.',
  },
  {
    icon: '💾',
    title: 'Save & Revisit',
    description: 'Your plans are saved so you can jump back in anytime without regenerating.',
  },
];

export default function HomePage() {
  const navigate = useNavigate();
  const { isLoggedIn } = useAuth();

  return (
    <>
      {/* Hero */}
      <div style={{ background: 'linear-gradient(135deg, #111 0%, #333 100%)', minHeight: '80vh' }}
        className="d-flex align-items-center text-white">
        <Container className="py-5 text-center">
          <p className="text-uppercase fw-semibold mb-3" style={{ letterSpacing: '0.15em', color: '#aaa' }}>
            Your AI Personal Trainer
          </p>
          <h1 className="display-4 fw-bold mb-4" style={{ lineHeight: 1.15 }}>
            Workouts built for<br />
            <span style={{ color: '#f0f0f0', fontStyle: 'italic' }}>you</span>, not everyone.
          </h1>
          <p className="lead mb-5 mx-auto" style={{ maxWidth: '500px', color: '#ccc' }}>
            Answer a few questions, hit Generate, and get a custom workout plan with guided timers and voice coaching — ready to go in seconds.
          </p>
          <div className="d-flex justify-content-center gap-3 flex-wrap">
            <Button
              size="lg"
              variant="light"
              className="fw-semibold px-4"
              onClick={() => navigate('/workout')}
            >
              Create My Workout
            </Button>
            {isLoggedIn && (
              <Button
                size="lg"
                variant="outline-light"
                className="px-4"
                onClick={() => navigate('/my-workouts')}
              >
                My Workouts
              </Button>
            )}
            {!isLoggedIn && (
              <Button
                size="lg"
                variant="outline-light"
                className="px-4"
                onClick={() => navigate('/login')}
              >
                Sign In
              </Button>
            )}
          </div>
        </Container>
      </div>

      {/* Features */}
      <Container className="py-5">
        <h2 className="fw-bold text-center mb-5">Everything you need to train smarter</h2>
        <Row className="g-4 justify-content-center">
          {features.map(f => (
            <Col key={f.title} xs={12} md={4}>
              <Card className="h-100 border-0 shadow-sm text-center p-3">
                <Card.Body>
                  <div className="display-5 mb-3">{f.icon}</div>
                  <h5 className="fw-bold mb-2">{f.title}</h5>
                  <p className="text-muted mb-0">{f.description}</p>
                </Card.Body>
              </Card>
            </Col>
          ))}
        </Row>
      </Container>

      {/* CTA */}
      <div className="bg-light py-5 text-center">
        <Container>
          <h3 className="fw-bold mb-3">Ready to get moving?</h3>
          <p className="text-muted mb-4">It takes less than a minute to generate your first workout.</p>
          <Button size="lg" variant="dark" className="px-5 fw-semibold" onClick={() => navigate('/workout')}>
            Get Started — It's Free
          </Button>
        </Container>
      </div>
    </>
  );
}
