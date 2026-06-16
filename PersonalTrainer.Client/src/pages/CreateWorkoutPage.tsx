import { useState, useEffect } from 'react';
import type React from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Card, ProgressBar, Button, Form } from 'react-bootstrap';
import { getWorkoutOptions } from '../services/workoutOptionService';
import type { WorkoutOptions } from '../services/workoutOptionService';
import { generateWorkout } from '../services/WorkOutService';
import { useAuth } from '../context/AuthContext';

const TOTAL_STEPS = 3;

interface WorkoutForm {
  // Step 1
  firstName: string;
  age: number;
  fitnessLevel: string;
  // Step 2
  goal: string;
  focusArea: string;
  // Step 3
  durationMinutes: number;
  equipment: string[];
  healthLimitations: string;
}

interface FormErrors {
  firstName?: string;
  age?: string;
  fitnessLevel?: string;
  goal?: string;
  focusArea?: string;
  equipment?: string;
}

const initialForm: WorkoutForm = {
  firstName: '',
  age: 13,
  fitnessLevel: '',
  goal: '',
  focusArea: '',
  durationMinutes: 30,
  equipment: [],
  healthLimitations: '',
};

export default function CreateWorkoutPage() {
  const navigate = useNavigate();
  const [step, setStep] = useState(1);
  const [workoutOptions, setWorkoutOptions] = useState<WorkoutOptions | null>(null);
  const [errors, setErrors] = useState<FormErrors>({});
  const { isLoggedIn, user } = useAuth();
  const [form, setForm] = useState<WorkoutForm>({ ...initialForm, firstName: user?.firstName || '' });

  useEffect(() => {
    getWorkoutOptions().then(setWorkoutOptions);
  }, []);

  useEffect(() => {
    if (user?.firstName) {
      setForm(prev => ({ ...prev, firstName: user.firstName }));
    }
  }, [user?.firstName]);

  const progress = ((step - 1) / (TOTAL_STEPS - 1)) * 100;

  const validateStep = (currentStep: number): FormErrors => {
    const e: FormErrors = {};

    if (currentStep === 1) {
      if (!isLoggedIn && !form.firstName.trim()) e.firstName = 'First name is required';
      if (!form.age || form.age < 13 || form.age > 120) e.age = 'Please enter a valid age (13–120)';
      if (!form.fitnessLevel) e.fitnessLevel = 'Please select a fitness level';
    }

    if (currentStep === 2) {
      if (!form.goal) e.goal = 'Please select a goal';
      if (!form.focusArea) e.focusArea = 'Please select a focus area';
    }

    if (currentStep === 3) {
      if (form.equipment.length === 0) e.equipment = 'Please select at least one equipment option';
    }

    return e;
  };

  const handleNext = (e: React.MouseEvent) => {
    if (e.detail > 1) return;
    const stepErrors = validateStep(step);
    if (Object.keys(stepErrors).length > 0) {
      setErrors(stepErrors);
      return;
    }
    setErrors({});
    setStep(step + 1);
  };

  const handleBack = (e: React.MouseEvent) => {
    if (e.detail > 1) return;
    setErrors({});
    setStep(step - 1);
  };

  const handleGenerate = async () => {
    const stepErrors = validateStep(step);
    if (Object.keys(stepErrors).length > 0) {
      setErrors(stepErrors);
      return;
    }
    setErrors({});
    const plan = await generateWorkout({
      firstName: form.firstName,
      age: form.age,
      fitnessLevel: form.fitnessLevel,
      goal: form.goal,
      focusArea: form.focusArea,
      durationMinutes: form.durationMinutes,
      equipment: form.equipment,
      healthLimitations: form.healthLimitations || undefined,
    });
    navigate('/workout/plan', { state: { plan } });
  };

  const updateForm = (fields: Partial<WorkoutForm>) => {
    setForm(prev => ({ ...prev, ...fields }));
  };

  const renderStep = () => {
    switch (step) {
      case 1: return <StepOne form={form} updateForm={updateForm} workoutOptions={workoutOptions} isLoggedIn={isLoggedIn} errors={errors} />;
      case 2: return <StepTwo form={form} updateForm={updateForm} workoutOptions={workoutOptions} errors={errors} />;
      case 3: return <StepThree form={form} updateForm={updateForm} workoutOptions={workoutOptions} errors={errors} />;
    }
  };

  return (
    <Container className="py-5" style={{ maxWidth: '600px' }}>
      <Card className="shadow-sm">
        <Card.Body className="p-4">
          <h4 className="fw-semibold mb-1">Create Workout</h4>
          <div className="d-flex justify-content-between mb-1">
            <small className="text-muted">Step {step} of {TOTAL_STEPS}</small>
            <small className="text-muted">{Math.round(progress)}%</small>
          </div>
          <ProgressBar now={progress} className="mb-4" style={{ height: '6px' }} />

          {renderStep()}

          <div className="d-flex justify-content-between mt-4">
            <Button type="button" variant="outline-secondary" onClick={handleBack} disabled={step === 1}>
              Back
            </Button>
            {step < TOTAL_STEPS ? (
              <Button type="button" variant="dark" onClick={handleNext}>
                Next
              </Button>
            ) : (
              <Button type="button" variant="dark" onClick={handleGenerate}>
                Generate
              </Button>
            )}
          </div>
        </Card.Body>
      </Card>
    </Container>
  );
}

interface StepProp {
  form: WorkoutForm;
  updateForm: (fields: Partial<WorkoutForm>) => void;
  workoutOptions: WorkoutOptions | null;
  isLoggedIn?: boolean;
  errors: FormErrors;
}

function StepOne({ form, updateForm, workoutOptions, isLoggedIn, errors }: StepProp) {
  return (
    <>
      <h5 className="mb-4">About You</h5>

      <Form.Group className="mb-3">
        <Form.Label>First Name</Form.Label>
        {isLoggedIn ? (
          <p className="fw-semibold mb-0">{form.firstName}</p>
        ) : (
          <>
            <Form.Control
              placeholder="Enter your first name"
              value={form.firstName}
              isInvalid={!!errors.firstName}
              onChange={e => updateForm({ firstName: e.target.value })}
            />
            <Form.Control.Feedback type="invalid">{errors.firstName}</Form.Control.Feedback>
          </>
        )}
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Age</Form.Label>
        <Form.Control
          type="number"
          placeholder="Enter your age"
           value={form.age || ''}  // shows empty instead of 0
          min={13}
          max={120}
          isInvalid={!!errors.age}
          onChange={e => updateForm({ age: Number(e.target.value) })}
        />
        <Form.Control.Feedback type="invalid">{errors.age}</Form.Control.Feedback>
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Fitness Level</Form.Label>
        <Form.Select
          value={form.fitnessLevel}
          isInvalid={!!errors.fitnessLevel}
          onChange={e => updateForm({ fitnessLevel: e.target.value })}
        >
          <option value="">Select fitness level</option>
          {workoutOptions?.fitnessLevels.map(opt => (
            <option key={opt.value} value={opt.value}>{opt.displayName}</option>
          ))}
        </Form.Select>
        <Form.Control.Feedback type="invalid">{errors.fitnessLevel}</Form.Control.Feedback>
      </Form.Group>
    </>
  );
}

function StepTwo({ form, updateForm, workoutOptions, errors }: StepProp) {
  return (
    <>
      <h5 className="mb-4">Primary Focus</h5>

      <Form.Group className="mb-3">
        <Form.Label>Goal</Form.Label>
        <Form.Select
          value={form.goal}
          isInvalid={!!errors.goal}
          onChange={e => updateForm({ goal: e.target.value })}
        >
          <option value="">Select your goal</option>
          {workoutOptions?.workoutGoals.map(opt => (
            <option key={opt.value} value={opt.value}>{opt.displayName}</option>
          ))}
        </Form.Select>
        <Form.Control.Feedback type="invalid">{errors.goal}</Form.Control.Feedback>
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Focus Area</Form.Label>
        <Form.Select
          value={form.focusArea}
          isInvalid={!!errors.focusArea}
          onChange={e => updateForm({ focusArea: e.target.value })}
        >
          <option value="">Select focus area</option>
          {workoutOptions?.focusAreas.map(opt => (
            <option key={opt.value} value={opt.value}>{opt.displayName}</option>
          ))}
        </Form.Select>
        <Form.Control.Feedback type="invalid">{errors.focusArea}</Form.Control.Feedback>
      </Form.Group>
    </>
  );
}

function StepThree({ form, updateForm, workoutOptions, errors }: StepProp) {
  return (
    <>
      <h5 className="mb-4">Workout Preferences</h5>

      <Form.Group className="mb-3">
        <Form.Label>Duration</Form.Label>
        <Form.Select
          value={form.durationMinutes}
          onChange={e => updateForm({ durationMinutes: Number(e.target.value) })}
        >
          <option value={10}>10 minutes</option>
          <option value={30}>30 minutes</option>
          <option value={45}>45 minutes</option>
          <option value={60}>60 minutes</option>
        </Form.Select>
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Equipment</Form.Label>
        {workoutOptions?.equipment.map(opt => (
          <Form.Check
            key={opt.value}
            type="checkbox"
            label={opt.displayName}
            checked={form.equipment.includes(opt.value)}
            onChange={e => {
              const updated = e.target.checked
                ? [...form.equipment, opt.value]
                : form.equipment.filter(eq => eq !== opt.value);
              updateForm({ equipment: updated });
            }}
          />
        ))}
        {errors.equipment && <div className="text-danger small mt-1">{errors.equipment}</div>}
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Health Limitations</Form.Label>
        <Form.Control
          as="textarea"
          rows={3}
          value={form.healthLimitations}
          onChange={e => updateForm({ healthLimitations: e.target.value })}
          placeholder="e.g. bad knees, lower back pain, shoulder injury, asthma..."
        />
      </Form.Group>
    </>
  );
}
