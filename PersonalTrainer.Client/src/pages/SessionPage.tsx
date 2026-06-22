import { useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Card, ProgressBar, Button, Badge } from 'react-bootstrap';
import type { WorkoutPlan } from '../services/WorkOutService';

export default function SessionPage() {
  const navigate = useNavigate();
  const raw = sessionStorage.getItem('workoutPlan');
  const plan = raw ? (JSON.parse(raw) as WorkoutPlan) : null;
  const exercises = plan?.exercises ?? [];

  const [currentIndex, setCurrentIndex] = useState(0);
  const [timeLeft, setTimeLeft] = useState(exercises[0]?.durationSeconds ?? 0);
  const [isResting, setIsResting] = useState(false);

  const handleNext = useCallback(() => {
    setCurrentIndex(prev => prev + 1);
  }, []);

  // Reset timer when exercise changes — single source of truth
useEffect(() => {
  const ex = exercises[currentIndex];
  if (ex) {
    setIsResting(false);
    setTimeLeft(ex.durationSeconds);
  }
}, [currentIndex]);

// Countdown only — no state changes here except timeLeft
useEffect(() => {
  if (timeLeft <= 0) return; // let the transition useEffect handle 0
  const timer = setInterval(() => setTimeLeft(prev => prev - 1), 1000);
  return () => clearInterval(timer);
}, [timeLeft]);

// Handle timer hitting 0 — separate from countdown
useEffect(() => {
  if (timeLeft !== 0) return;
  
  const ex = exercises[currentIndex];
  if (!ex) return;

  if (!isResting && ex.restSeconds > 0) {
    setIsResting(true);
    setTimeLeft(ex.restSeconds);
  } else {
    setIsResting(false);
    handleNext();
  }
}, [timeLeft]);

  if (!plan) {
    return (
      <Container className="py-5 text-center">
        <p className="text-muted">No workout found.</p>
        <Button variant="dark" onClick={() => navigate('/workout')}>Create a Workout</Button>
      </Container>
    );
  }

  const isLast = currentIndex === exercises.length - 1;
  const isDone = currentIndex >= exercises.length;

  if (isDone) {
    return (
      <Container className="py-5 text-center" style={{ maxWidth: '600px' }}>
        <h4 className="fw-bold mb-2">Workout Complete!</h4>
        <p className="text-muted mb-4">{plan.completionMessage}</p>
        <div className="d-flex justify-content-center gap-3">
          <Button variant="outline-dark" onClick={() => navigate('/workout')}>New Workout</Button>
          <Button variant="dark" onClick={() => navigate('/workout/plan')}>View Plan</Button>
        </div>
      </Container>
    );
  }

  const ex = exercises[currentIndex];
  const exerciseProgress = Math.round((currentIndex / exercises.length) * 100);
  const timerProgress = ex.durationSeconds > 0
    ? Math.round((timeLeft / ex.durationSeconds) * 100)
    : 0;

  return (
    <Container className="py-5" style={{ maxWidth: '600px' }}>
      {/* Exercise progress */}
      <div className="d-flex justify-content-between mb-1">
        <small className="text-muted">Exercise {currentIndex + 1} of {exercises.length}</small>
        <small className="text-muted">{exerciseProgress}%</small>
      </div>
      <ProgressBar now={exerciseProgress} className="mb-4" style={{ height: '6px' }} />

      <Card className="shadow-sm">
        <Card.Body className="p-4">
          <h5 className="fw-bold mb-1">{ex.name}</h5>
          <p className="text-muted small mb-3">{ex.instructions}</p>

          {/* Countdown timer */}
          {/* <div className="text-center my-4">
            <div className="display-3 fw-bold">{timeLeft}</div>
            <small className="text-muted text-uppercase">seconds remaining</small>
            <ProgressBar
              now={timerProgress}
              variant={timeLeft <= 5 ? 'danger' : 'dark'}
              className="mt-2"
              style={{ height: '4px' }}
            />
          </div> */}
                  <div className="text-center my-4">
                      <small className="text-muted text-uppercase fw-semibold">
                          {isResting ? '😮‍💨 Rest' : 'Time Remaining'}
                      </small>
                      <div className={`display-3 fw-bold ${isResting ? 'text-secondary' : 'text-dark'}`}>
                          {timeLeft}
                      </div>
                      <small className="text-muted text-uppercase">seconds</small>
                      <ProgressBar
                          now={isResting
                              ? Math.round((timeLeft / ex.restSeconds) * 100)
                              : timerProgress}
                          variant={isResting ? 'secondary' : timeLeft <= 5 ? 'danger' : 'dark'}
                          className="mt-2"
                          style={{ height: '4px' }}
                      />
                  </div>

          <div className="d-flex gap-2 flex-wrap mb-3">
            <Badge bg="dark">{ex.durationSeconds}s</Badge>
            {ex.sets && ex.reps && (
              <Badge bg="dark">{ex.sets} sets × {ex.reps} reps</Badge>
            )}
            <Badge bg="secondary">Rest {ex.restSeconds}s</Badge>
            <Badge bg="light" text="dark" className="border">{ex.difficulty}</Badge>
          </div>

          <p className="text-muted small mb-2">
            <span className="fw-semibold">Muscles: </span>{ex.musclesTargeted}
          </p>
          <p className="fst-italic small text-success mb-0">{ex.encouragementMessage}</p>
        </Card.Body>
      </Card>

      {/* Next exercise preview */}
      {!isLast && (
        <Card className="mt-3 border-0 bg-light">
          <Card.Body className="py-2 px-3">
            <small className="text-muted">
              Next: <span className="fw-semibold">{exercises[currentIndex + 1].name}</span>
            </small>
          </Card.Body>
        </Card>
      )}

      <div className="d-flex justify-content-between mt-4">
        <Button
          variant="outline-secondary"
          disabled={currentIndex === 0}
          onClick={() => setCurrentIndex(prev => prev - 1)}
        >
          Back
        </Button>
        <Button variant="dark" onClick={handleNext}>
          {isLast ? 'Finish' : 'Next'}
        </Button>
      </div>
    </Container>
  );
}
