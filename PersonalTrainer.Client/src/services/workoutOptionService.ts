import { apiClient, WORKOUT_OPTIONS_URL } from './ApiConfig';

export interface EnumOption {
  value: string;
  displayName: string;
}

export interface WorkoutOptions {
  fitnessLevels: EnumOption[];
  workoutGoals: EnumOption[];
  focusAreas: EnumOption[];
  equipment: EnumOption[];
}

export const getWorkoutOptions = async (): Promise<WorkoutOptions> => {
  const response = await apiClient.get<WorkoutOptions>(WORKOUT_OPTIONS_URL);
  return response.data;
};
