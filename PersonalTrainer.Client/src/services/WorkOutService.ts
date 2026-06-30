import { apiClient, GENERATE_WORKOUT_URL, MY_PLANS_URL } from './ApiConfig';

export interface GenerateWorkoutRequest {
  firstName: string;
  age: number;
  fitnessLevel: string;
  goal: string;
  focusArea: string;
  durationMinutes: number;
  equipment: string[];
  healthLimitations?: string;
}

export interface Exercise {
  name: string;
  instructions: string;
  durationSeconds: number;
  restSeconds: number;
  sets: number | null;
  reps: number | null;
  musclesTargeted: string;
  difficulty: string;
  encouragementMessage: string;
}

export interface WorkoutPlan {
  title: string;
  motivationalIntro: string;
  warmupCue: string;
  cooldownCue: string;
  completionMessage: string;
  exercises: Exercise[];
}

export interface WorkoutPlanSummary {
  id: number;
  title: string;
  motivationalIntro: string;
  exerciseCount: number;
  createdAt: string;
}

export async function generateWorkout(request: GenerateWorkoutRequest): Promise<WorkoutPlan> {
  const response = await apiClient.post<WorkoutPlan>(GENERATE_WORKOUT_URL, request);
  return response.data;
}

export async function getMyPlans(): Promise<WorkoutPlanSummary[]> {
  const response = await apiClient.get<WorkoutPlanSummary[]>(MY_PLANS_URL);
  return response.data;
}
