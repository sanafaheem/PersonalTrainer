using System.Text.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PersonalTrainer.API.Enums;
using PersonalTrainer.API.Models;

namespace PersonalTrainer.API.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserWorkoutProfile> UserWorkoutProfiles { get; set; }
    public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseTemplate> ExerciseTemplates => Set<ExerciseTemplate>();
    public DbSet<ExerciseEquipment> ExerciseEquipment => Set<ExerciseEquipment>();
    public DbSet<ExerciseMuscleGroup> ExerciseMuscleGroups => Set<ExerciseMuscleGroup>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserWorkoutProfile>()
            .Property(p => p.Equipment)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<Equipment>>(v, (JsonSerializerOptions?)null) ?? new()
            )
            .HasColumnType("nvarchar(max)")
            .Metadata.SetValueComparer(new ValueComparer<List<Equipment>>(
                (a, b) => a != null && b != null && a.SequenceEqual(b),
                v => v.Aggregate(0, (a, e) => HashCode.Combine(a, e.GetHashCode())),
                v => v.ToList()
            ));

        builder.Entity<ExerciseEquipment>(entity =>
        {
            entity.ToTable("ExerciseEquipment");

            entity.HasOne(e => e.ExerciseTemplate)
                .WithMany(ex => ex.Equipment)
                .HasForeignKey(e => e.ExerciseTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<ExerciseMuscleGroup>(entity =>
        {
            entity.ToTable("ExerciseMuscleGroups");
            entity.HasOne(e => e.ExerciseTemplate)
                  .WithMany(ex => ex.MuscleGroups)
                  .HasForeignKey(e => e.ExerciseTemplateId)
                  .OnDelete(DeleteBehavior.Cascade);
        });


        // seed data for ExerciseTemplate, ExerciseEquipment, and ExerciseMuscleGroup
        // ── Exercise Templates ──────────────────────────────────────────
        builder.Entity<ExerciseTemplate>().HasData(

            // BODYWEIGHT — CHEST / TRICEPS / SHOULDERS
            new ExerciseTemplate
            {
                Id = 1,
                Name = "Push Ups",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 5,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Start in a plank position with hands shoulder-width apart. Lower your chest to the floor keeping your core tight, then push back up.",
                Contraindications = "wrist injury, shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 2,
                Name = "Diamond Push Ups",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 50,
                MinReps = 5,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Place hands close together forming a diamond shape. Lower chest toward hands keeping elbows tucked in.",
                Contraindications = "wrist injury, elbow injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 3,
                Name = "Pike Push Ups",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 50,
                MinReps = 5,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 3,
                Instructions = "Start in a downward dog position. Bend elbows and lower your head toward the floor, then push back up.",
                Contraindications = "shoulder injury",
                IsActive = true
            },

            // BODYWEIGHT — CORE
            new ExerciseTemplate
            {
                Id = 4,
                Name = "Plank Hold",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 90,
                MinReps = null,
                MaxReps = null,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold a straight line from head to heels on your forearms and toes. Engage your core throughout.",
                Contraindications = "lower back pain",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 5,
                Name = "Crunches",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 30,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Lie on your back with knees bent. Lift your shoulders off the floor engaging your core, then lower slowly.",
                Contraindications = "neck injury, lower back pain",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 6,
                Name = "Bicycle Crunches",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 30,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Lie on your back, hands behind head. Bring opposite elbow to knee in a cycling motion.",
                Contraindications = "neck injury, lower back pain",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 7,
                Name = "Leg Raises",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Lie flat on your back. Keep legs straight and raise them to 90 degrees then lower slowly.",
                Contraindications = "lower back pain, hip flexor injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 8,
                Name = "Mountain Climbers",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = null,
                MaxReps = null,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Start in a plank position. Alternate driving knees toward chest in a running motion.",
                Contraindications = "wrist injury, shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 9,
                Name = "Russian Twists",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 30,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Sit with knees bent and feet off the floor. Rotate your torso side to side.",
                Contraindications = "lower back pain",
                IsActive = true
            },

            // BODYWEIGHT — LOWER BODY
            new ExerciseTemplate
            {
                Id = 10,
                Name = "Squats",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 25,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Stand with feet shoulder-width apart. Push hips back and bend knees until thighs are parallel to floor. Push through heels to stand.",
                Contraindications = "bad knees, knee injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 11,
                Name = "Jump Squats",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 50,
                MinReps = 8,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Perform a squat then explode upward jumping off the floor. Land softly and go straight into the next rep.",
                Contraindications = "bad knees, knee injury, ankle injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 12,
                Name = "Lunges",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Step forward with one leg and lower your hips until both knees are at 90 degrees. Push back to start and alternate legs.",
                Contraindications = "bad knees, knee injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 13,
                Name = "Glute Bridges",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 25,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Lie on your back with knees bent. Drive hips upward squeezing glutes at the top, then lower slowly.",
                Contraindications = "lower back pain",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 14,
                Name = "Calf Raises",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 30,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Stand with feet hip-width apart. Rise onto the balls of your feet, hold briefly, then lower.",
                Contraindications = "ankle injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 15,
                Name = "Wall Sit",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 90,
                MinReps = null,
                MaxReps = null,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Slide your back down a wall until thighs are parallel to the floor. Hold the position.",
                Contraindications = "bad knees, knee injury",
                IsActive = true
            },

            // BODYWEIGHT — FULL BODY / CARDIO
            new ExerciseTemplate
            {
                Id = 16,
                Name = "Burpees",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 5,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "From standing, drop to a push up position, perform a push up, jump feet forward, then explode upward with arms overhead.",
                Contraindications = "bad knees, wrist injury, shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 17,
                Name = "High Knees",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = null,
                MaxReps = null,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Run in place driving knees as high as possible. Pump arms in rhythm.",
                Contraindications = "knee injury, ankle injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 18,
                Name = "Jumping Jacks",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = null,
                MaxReps = null,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Jump feet wide while raising arms overhead, then jump back together.",
                Contraindications = "ankle injury, shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 19,
                Name = "Bear Crawl",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = null,
                MaxReps = null,
                MinSets = 2,
                MaxSets = 3,
                Instructions = "On hands and feet with knees hovering just off the floor. Move forward alternating opposite hand and foot.",
                Contraindications = "wrist injury, shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 20,
                Name = "Inchworm",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 5,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 3,
                Instructions = "Stand and fold forward. Walk hands out to a plank, then walk feet to hands and stand.",
                Contraindications = "lower back pain, hamstring injury",
                IsActive = true
            },

            // DUMBBELLS — UPPER BODY
            new ExerciseTemplate
            {
                Id = 21,
                Name = "Dumbbell Shoulder Press",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold dumbbells at shoulder height. Press overhead until arms are extended, then lower slowly.",
                Contraindications = "shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 22,
                Name = "Dumbbell Bicep Curl",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold dumbbells with palms facing up. Curl toward shoulders keeping elbows tucked, then lower slowly.",
                Contraindications = "elbow injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 23,
                Name = "Dumbbell Tricep Extension",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold one dumbbell overhead with both hands. Lower behind your head bending at elbows, then extend back up.",
                Contraindications = "elbow injury, shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 24,
                Name = "Dumbbell Lateral Raises",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold dumbbells at sides. Raise arms out to the side to shoulder height then lower slowly.",
                Contraindications = "shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 25,
                Name = "Dumbbell Bent Over Row",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hinge forward at the hips. Pull dumbbells toward your ribcage squeezing shoulder blades, then lower.",
                Contraindications = "lower back pain",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 26,
                Name = "Dumbbell Chest Press",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Lie on your back with dumbbells at chest height. Press upward until arms are extended then lower slowly.",
                Contraindications = "shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 27,
                Name = "Dumbbell Chest Fly",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 12,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Lie on your back holding dumbbells above chest. Open arms wide in an arc then bring back together.",
                Contraindications = "shoulder injury",
                IsActive = true
            },

            // DUMBBELLS — LOWER BODY
            new ExerciseTemplate
            {
                Id = 28,
                Name = "Dumbbell Squat",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold dumbbells at sides or shoulders. Perform a squat keeping chest up and knees tracking over toes.",
                Contraindications = "bad knees, knee injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 29,
                Name = "Dumbbell Lunges",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 16,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold dumbbells at sides. Step forward and lower until both knees are at 90 degrees then return.",
                Contraindications = "bad knees, knee injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 30,
                Name = "Dumbbell Romanian Deadlift",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold dumbbells in front. Hinge at hips keeping back straight lowering weights along legs, then drive hips forward to stand.",
                Contraindications = "lower back pain, hamstring injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 31,
                Name = "Dumbbell Calf Raises",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 25,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold dumbbells at sides. Rise onto balls of feet hold briefly then lower.",
                Contraindications = "ankle injury",
                IsActive = true
            },

            // KETTLEBELL
            new ExerciseTemplate
            {
                Id = 32,
                Name = "Kettlebell Swing",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 25,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hinge at hips holding kettlebell. Drive hips forward explosively swinging the bell to chest height, then hinge back.",
                Contraindications = "lower back pain, shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 33,
                Name = "Kettlebell Goblet Squat",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold kettlebell at chest with both hands. Squat deep keeping chest up and elbows inside knees.",
                Contraindications = "bad knees, knee injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 34,
                Name = "Kettlebell Turkish Get Up",
                MinDurationSeconds = 30,
                MaxDurationSeconds = 90,
                MinReps = 2,
                MaxReps = 6,
                MinSets = 2,
                MaxSets = 3,
                Instructions = "Lie holding kettlebell overhead. Follow a sequence to stand while keeping the bell pressed overhead, then reverse.",
                Contraindications = "shoulder injury, wrist injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 35,
                Name = "Kettlebell Clean and Press",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 6,
                MaxReps = 12,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Clean kettlebell to shoulder then press overhead. Lower with control and repeat.",
                Contraindications = "shoulder injury, wrist injury",
                IsActive = true
            },

            // PULL UP BAR
            new ExerciseTemplate
            {
                Id = 36,
                Name = "Pull Ups",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 2,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hang from bar with overhand grip. Pull yourself up until chin clears the bar then lower slowly.",
                Contraindications = "shoulder injury, elbow injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 37,
                Name = "Chin Ups",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 2,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hang from bar with underhand grip. Pull yourself up until chin clears the bar then lower slowly.",
                Contraindications = "shoulder injury, elbow injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 38,
                Name = "Dead Hang",
                MinDurationSeconds = 15,
                MaxDurationSeconds = 60,
                MinReps = null,
                MaxReps = null,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hang from bar with both hands. Relax your shoulders and hang for the prescribed time.",
                Contraindications = "shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 39,
                Name = "Hanging Knee Raises",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hang from bar. Drive knees toward chest engaging core then lower with control.",
                Contraindications = "shoulder injury, hip flexor injury",
                IsActive = true
            },

            // RESISTANCE BAND
            new ExerciseTemplate
            {
                Id = 40,
                Name = "Resistance Band Pull Apart",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 25,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Hold band at chest width with both hands. Pull apart horizontally squeezing shoulder blades, then return.",
                Contraindications = "shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 41,
                Name = "Resistance Band Squat",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 25,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Stand on band holding ends at shoulders. Perform a squat keeping tension throughout.",
                Contraindications = "bad knees, knee injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 42,
                Name = "Resistance Band Row",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Anchor band at waist height. Hold ends and pull toward your torso squeezing shoulder blades.",
                Contraindications = "lower back pain",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 43,
                Name = "Resistance Band Bicep Curl",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Stand on band with palms facing up. Curl hands toward shoulders keeping elbows tucked.",
                Contraindications = "elbow injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 44,
                Name = "Resistance Band Glute Kickback",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 10,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Loop band around ankles on all fours. Kick one leg back squeezing glute at the top, then alternate.",
                Contraindications = "hip injury, knee injury",
                IsActive = true
            },

            // MAT — FLEXIBILITY / CORE
            new ExerciseTemplate
            {
                Id = 45,
                Name = "Child's Pose",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = null,
                MaxReps = null,
                MinSets = 1,
                MaxSets = 3,
                Instructions = "Kneel and sit back on heels. Extend arms forward resting forehead on mat. Breathe deeply.",
                Contraindications = "knee injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 46,
                Name = "Cat Cow Stretch",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 3,
                Instructions = "On all fours alternate arching your back toward the ceiling then dropping it toward the floor.",
                Contraindications = "lower back pain",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 47,
                Name = "Superman Hold",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 15,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Lie face down with arms extended. Simultaneously lift arms and legs off the mat, hold briefly then lower.",
                Contraindications = "lower back pain",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 48,
                Name = "Cobra Stretch",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = null,
                MaxReps = null,
                MinSets = 2,
                MaxSets = 3,
                Instructions = "Lie face down with hands under shoulders. Press up lifting chest while keeping hips on mat.",
                Contraindications = "lower back pain, wrist injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 49,
                Name = "Hip Flexor Stretch",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = null,
                MaxReps = null,
                MinSets = 2,
                MaxSets = 3,
                Instructions = "Kneel on one knee with the other foot forward. Drive hips forward feeling the stretch in the front of the back hip.",
                Contraindications = "knee injury, hip injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 50,
                Name = "Dead Bug",
                MinDurationSeconds = 20,
                MaxDurationSeconds = 60,
                MinReps = 8,
                MaxReps = 20,
                MinSets = 2,
                MaxSets = 4,
                Instructions = "Lie on back with arms up and knees at 90 degrees. Lower opposite arm and leg toward floor while keeping lower back pressed down.",
                Contraindications = "lower back pain",
                IsActive = true
            },

            // CARDIO MACHINE
            new ExerciseTemplate
            {
                Id = 51,
                Name = "Treadmill Run",
                MinDurationSeconds = 60,
                MaxDurationSeconds = 300,
                MinReps = null,
                MaxReps = null,
                MinSets = 1,
                MaxSets = 3,
                Instructions = "Run at a comfortable pace on the treadmill. Maintain upright posture and land midfoot.",
                Contraindications = "bad knees, ankle injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 52,
                Name = "Stationary Bike",
                MinDurationSeconds = 60,
                MaxDurationSeconds = 300,
                MinReps = null,
                MaxReps = null,
                MinSets = 1,
                MaxSets = 3,
                Instructions = "Cycle at a steady pace maintaining consistent cadence. Adjust resistance to appropriate level.",
                Contraindications = "knee injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 53,
                Name = "Rowing Machine",
                MinDurationSeconds = 60,
                MaxDurationSeconds = 300,
                MinReps = null,
                MaxReps = null,
                MinSets = 1,
                MaxSets = 3,
                Instructions = "Drive with legs first then lean back and pull handle to lower chest. Return in reverse order.",
                Contraindications = "lower back pain, shoulder injury",
                IsActive = true
            },

            new ExerciseTemplate
            {
                Id = 54,
                Name = "Elliptical",
                MinDurationSeconds = 60,
                MaxDurationSeconds = 300,
                MinReps = null,
                MaxReps = null,
                MinSets = 1,
                MaxSets = 3,
                Instructions = "Use the elliptical at a steady pace pushing and pulling the handles while maintaining upright posture.",
                Contraindications = "ankle injury",
                IsActive = true
            }
        );

        // ── Exercise Equipment ──────────────────────────────────────────
        builder.Entity<ExerciseEquipment>().HasData(
            // Push Ups (1) — Bodyweight
            new ExerciseEquipment { Id = 1, ExerciseTemplateId = 1, EquipmentType = Equipment.Bodyweight },
            // Diamond Push Ups (2) — Bodyweight
            new ExerciseEquipment { Id = 2, ExerciseTemplateId = 2, EquipmentType = Equipment.Bodyweight },
            // Pike Push Ups (3) — Bodyweight
            new ExerciseEquipment { Id = 3, ExerciseTemplateId = 3, EquipmentType = Equipment.Bodyweight },
            // Plank Hold (4) — Bodyweight + Mat
            new ExerciseEquipment { Id = 4, ExerciseTemplateId = 4, EquipmentType = Equipment.Bodyweight },
            new ExerciseEquipment { Id = 5, ExerciseTemplateId = 4, EquipmentType = Equipment.Mat },
            // Crunches (5) — Bodyweight + Mat
            new ExerciseEquipment { Id = 6, ExerciseTemplateId = 5, EquipmentType = Equipment.Bodyweight },
            new ExerciseEquipment { Id = 7, ExerciseTemplateId = 5, EquipmentType = Equipment.Mat },
            // Bicycle Crunches (6) — Bodyweight + Mat
            new ExerciseEquipment { Id = 8, ExerciseTemplateId = 6, EquipmentType = Equipment.Bodyweight },
            new ExerciseEquipment { Id = 9, ExerciseTemplateId = 6, EquipmentType = Equipment.Mat },
            // Leg Raises (7) — Bodyweight + Mat
            new ExerciseEquipment { Id = 10, ExerciseTemplateId = 7, EquipmentType = Equipment.Bodyweight },
            new ExerciseEquipment { Id = 11, ExerciseTemplateId = 7, EquipmentType = Equipment.Mat },
            // Mountain Climbers (8) — Bodyweight
            new ExerciseEquipment { Id = 12, ExerciseTemplateId = 8, EquipmentType = Equipment.Bodyweight },
            // Russian Twists (9) — Bodyweight + Mat
            new ExerciseEquipment { Id = 13, ExerciseTemplateId = 9, EquipmentType = Equipment.Bodyweight },
            new ExerciseEquipment { Id = 14, ExerciseTemplateId = 9, EquipmentType = Equipment.Mat },
            // Squats (10) — Bodyweight
            new ExerciseEquipment { Id = 15, ExerciseTemplateId = 10, EquipmentType = Equipment.Bodyweight },
            // Jump Squats (11) — Bodyweight
            new ExerciseEquipment { Id = 16, ExerciseTemplateId = 11, EquipmentType = Equipment.Bodyweight },
            // Lunges (12) — Bodyweight
            new ExerciseEquipment { Id = 17, ExerciseTemplateId = 12, EquipmentType = Equipment.Bodyweight },
            // Glute Bridges (13) — Bodyweight + Mat
            new ExerciseEquipment { Id = 18, ExerciseTemplateId = 13, EquipmentType = Equipment.Bodyweight },
            new ExerciseEquipment { Id = 19, ExerciseTemplateId = 13, EquipmentType = Equipment.Mat },
            // Calf Raises (14) — Bodyweight
            new ExerciseEquipment { Id = 20, ExerciseTemplateId = 14, EquipmentType = Equipment.Bodyweight },
            // Wall Sit (15) — Bodyweight
            new ExerciseEquipment { Id = 21, ExerciseTemplateId = 15, EquipmentType = Equipment.Bodyweight },
            // Burpees (16) — Bodyweight
            new ExerciseEquipment { Id = 22, ExerciseTemplateId = 16, EquipmentType = Equipment.Bodyweight },
            // High Knees (17) — Bodyweight
            new ExerciseEquipment { Id = 23, ExerciseTemplateId = 17, EquipmentType = Equipment.Bodyweight },
            // Jumping Jacks (18) — Bodyweight
            new ExerciseEquipment { Id = 24, ExerciseTemplateId = 18, EquipmentType = Equipment.Bodyweight },
            // Bear Crawl (19) — Bodyweight
            new ExerciseEquipment { Id = 25, ExerciseTemplateId = 19, EquipmentType = Equipment.Bodyweight },
            // Inchworm (20) — Bodyweight + Mat
            new ExerciseEquipment { Id = 26, ExerciseTemplateId = 20, EquipmentType = Equipment.Bodyweight },
            new ExerciseEquipment { Id = 27, ExerciseTemplateId = 20, EquipmentType = Equipment.Mat },
            // Dumbbell Shoulder Press (21) — Dumbbells
            new ExerciseEquipment { Id = 28, ExerciseTemplateId = 21, EquipmentType = Equipment.Dumbbells },
            // Dumbbell Bicep Curl (22) — Dumbbells
            new ExerciseEquipment { Id = 29, ExerciseTemplateId = 22, EquipmentType = Equipment.Dumbbells },
            // Dumbbell Tricep Extension (23) — Dumbbells
            new ExerciseEquipment { Id = 30, ExerciseTemplateId = 23, EquipmentType = Equipment.Dumbbells },
            // Dumbbell Lateral Raises (24) — Dumbbells
            new ExerciseEquipment { Id = 31, ExerciseTemplateId = 24, EquipmentType = Equipment.Dumbbells },
            // Dumbbell Bent Over Row (25) — Dumbbells
            new ExerciseEquipment { Id = 32, ExerciseTemplateId = 25, EquipmentType = Equipment.Dumbbells },
            // Dumbbell Chest Press (26) — Dumbbells + Mat
            new ExerciseEquipment { Id = 33, ExerciseTemplateId = 26, EquipmentType = Equipment.Dumbbells },
            new ExerciseEquipment { Id = 34, ExerciseTemplateId = 26, EquipmentType = Equipment.Mat },
            // Dumbbell Chest Fly (27) — Dumbbells + Mat
            new ExerciseEquipment { Id = 35, ExerciseTemplateId = 27, EquipmentType = Equipment.Dumbbells },
            new ExerciseEquipment { Id = 36, ExerciseTemplateId = 27, EquipmentType = Equipment.Mat },
            // Dumbbell Squat (28) — Dumbbells
            new ExerciseEquipment { Id = 37, ExerciseTemplateId = 28, EquipmentType = Equipment.Dumbbells },
            // Dumbbell Lunges (29) — Dumbbells
            new ExerciseEquipment { Id = 38, ExerciseTemplateId = 29, EquipmentType = Equipment.Dumbbells },
            // Dumbbell Romanian Deadlift (30) — Dumbbells
            new ExerciseEquipment { Id = 39, ExerciseTemplateId = 30, EquipmentType = Equipment.Dumbbells },
            // Dumbbell Calf Raises (31) — Dumbbells
            new ExerciseEquipment { Id = 40, ExerciseTemplateId = 31, EquipmentType = Equipment.Dumbbells },
            // Kettlebell Swing (32) — Kettlebell
            new ExerciseEquipment { Id = 41, ExerciseTemplateId = 32, EquipmentType = Equipment.Kettlebell },
            // Kettlebell Goblet Squat (33) — Kettlebell
            new ExerciseEquipment { Id = 42, ExerciseTemplateId = 33, EquipmentType = Equipment.Kettlebell },
            // Kettlebell Turkish Get Up (34) — Kettlebell + Mat
            new ExerciseEquipment { Id = 43, ExerciseTemplateId = 34, EquipmentType = Equipment.Kettlebell },
            new ExerciseEquipment { Id = 44, ExerciseTemplateId = 34, EquipmentType = Equipment.Mat },
            // Kettlebell Clean and Press (35) — Kettlebell
            new ExerciseEquipment { Id = 45, ExerciseTemplateId = 35, EquipmentType = Equipment.Kettlebell },
            // Pull Ups (36) — PullUpBar
            new ExerciseEquipment { Id = 46, ExerciseTemplateId = 36, EquipmentType = Equipment.PullUpBar },
            // Chin Ups (37) — PullUpBar
            new ExerciseEquipment { Id = 47, ExerciseTemplateId = 37, EquipmentType = Equipment.PullUpBar },
            // Dead Hang (38) — PullUpBar
            new ExerciseEquipment { Id = 48, ExerciseTemplateId = 38, EquipmentType = Equipment.PullUpBar },
            // Hanging Knee Raises (39) — PullUpBar
            new ExerciseEquipment { Id = 49, ExerciseTemplateId = 39, EquipmentType = Equipment.PullUpBar },
            // Resistance Band Pull Apart (40) — ResistanceBand
            new ExerciseEquipment { Id = 50, ExerciseTemplateId = 40, EquipmentType = Equipment.ResistanceBand },
            // Resistance Band Squat (41) — ResistanceBand
            new ExerciseEquipment { Id = 51, ExerciseTemplateId = 41, EquipmentType = Equipment.ResistanceBand },
            // Resistance Band Row (42) — ResistanceBand
            new ExerciseEquipment { Id = 52, ExerciseTemplateId = 42, EquipmentType = Equipment.ResistanceBand },
            // Resistance Band Bicep Curl (43) — ResistanceBand
            new ExerciseEquipment { Id = 53, ExerciseTemplateId = 43, EquipmentType = Equipment.ResistanceBand },
            // Resistance Band Glute Kickback (44) — ResistanceBand
            new ExerciseEquipment { Id = 54, ExerciseTemplateId = 44, EquipmentType = Equipment.ResistanceBand },
            // Child's Pose (45) — Mat
            new ExerciseEquipment { Id = 55, ExerciseTemplateId = 45, EquipmentType = Equipment.Mat },
            // Cat Cow (46) — Mat
            new ExerciseEquipment { Id = 56, ExerciseTemplateId = 46, EquipmentType = Equipment.Mat },
            // Superman Hold (47) — Mat
            new ExerciseEquipment { Id = 57, ExerciseTemplateId = 47, EquipmentType = Equipment.Mat },
            // Cobra Stretch (48) — Mat
            new ExerciseEquipment { Id = 58, ExerciseTemplateId = 48, EquipmentType = Equipment.Mat },
            // Hip Flexor Stretch (49) — Mat
            new ExerciseEquipment { Id = 59, ExerciseTemplateId = 49, EquipmentType = Equipment.Mat },
            // Dead Bug (50) — Mat
            new ExerciseEquipment { Id = 60, ExerciseTemplateId = 50, EquipmentType = Equipment.Mat },
            // Treadmill Run (51) — CardioMachine
            new ExerciseEquipment { Id = 61, ExerciseTemplateId = 51, EquipmentType = Equipment.CardioMachine },
            // Stationary Bike (52) — CardioMachine
            new ExerciseEquipment { Id = 62, ExerciseTemplateId = 52, EquipmentType = Equipment.CardioMachine },
            // Rowing Machine (53) — CardioMachine
            new ExerciseEquipment { Id = 63, ExerciseTemplateId = 53, EquipmentType = Equipment.CardioMachine },
            // Elliptical (54) — CardioMachine
            new ExerciseEquipment { Id = 64, ExerciseTemplateId = 54, EquipmentType = Equipment.CardioMachine }
        );

        // ── Exercise Muscle Groups ──────────────────────────────────────
        builder.Entity<ExerciseMuscleGroup>().HasData(
            // Push Ups (1)
            new ExerciseMuscleGroup { Id = 1, ExerciseTemplateId = 1, MuscleGroup = MuscleGroup.Chest },
            new ExerciseMuscleGroup { Id = 2, ExerciseTemplateId = 1, MuscleGroup = MuscleGroup.Triceps },
            new ExerciseMuscleGroup { Id = 3, ExerciseTemplateId = 1, MuscleGroup = MuscleGroup.Shoulders },
            // Diamond Push Ups (2)
            new ExerciseMuscleGroup { Id = 4, ExerciseTemplateId = 2, MuscleGroup = MuscleGroup.Triceps },
            new ExerciseMuscleGroup { Id = 5, ExerciseTemplateId = 2, MuscleGroup = MuscleGroup.Chest },
            // Pike Push Ups (3)
            new ExerciseMuscleGroup { Id = 6, ExerciseTemplateId = 3, MuscleGroup = MuscleGroup.Shoulders },
            new ExerciseMuscleGroup { Id = 7, ExerciseTemplateId = 3, MuscleGroup = MuscleGroup.Triceps },
            // Plank Hold (4)
            new ExerciseMuscleGroup { Id = 8, ExerciseTemplateId = 4, MuscleGroup = MuscleGroup.Core },
            new ExerciseMuscleGroup { Id = 9, ExerciseTemplateId = 4, MuscleGroup = MuscleGroup.Shoulders },
            // Crunches (5)
            new ExerciseMuscleGroup { Id = 10, ExerciseTemplateId = 5, MuscleGroup = MuscleGroup.Core },
            // Bicycle Crunches (6)
            new ExerciseMuscleGroup { Id = 11, ExerciseTemplateId = 6, MuscleGroup = MuscleGroup.Core },
            // Leg Raises (7)
            new ExerciseMuscleGroup { Id = 12, ExerciseTemplateId = 7, MuscleGroup = MuscleGroup.Core },
            // Mountain Climbers (8)
            new ExerciseMuscleGroup { Id = 13, ExerciseTemplateId = 8, MuscleGroup = MuscleGroup.FullBody },
            new ExerciseMuscleGroup { Id = 14, ExerciseTemplateId = 8, MuscleGroup = MuscleGroup.Core },
            // Russian Twists (9)
            new ExerciseMuscleGroup { Id = 15, ExerciseTemplateId = 9, MuscleGroup = MuscleGroup.Core },
            // Squats (10)
            new ExerciseMuscleGroup { Id = 16, ExerciseTemplateId = 10, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 17, ExerciseTemplateId = 10, MuscleGroup = MuscleGroup.Glutes },
            new ExerciseMuscleGroup { Id = 18, ExerciseTemplateId = 10, MuscleGroup = MuscleGroup.Hamstrings },
            // Jump Squats (11)
            new ExerciseMuscleGroup { Id = 19, ExerciseTemplateId = 11, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 20, ExerciseTemplateId = 11, MuscleGroup = MuscleGroup.Glutes },
            new ExerciseMuscleGroup { Id = 21, ExerciseTemplateId = 11, MuscleGroup = MuscleGroup.FullBody },
            // Lunges (12)
            new ExerciseMuscleGroup { Id = 22, ExerciseTemplateId = 12, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 23, ExerciseTemplateId = 12, MuscleGroup = MuscleGroup.Glutes },
            new ExerciseMuscleGroup { Id = 24, ExerciseTemplateId = 12, MuscleGroup = MuscleGroup.Hamstrings },
            // Glute Bridges (13)
            new ExerciseMuscleGroup { Id = 25, ExerciseTemplateId = 13, MuscleGroup = MuscleGroup.Glutes },
            new ExerciseMuscleGroup { Id = 26, ExerciseTemplateId = 13, MuscleGroup = MuscleGroup.Hamstrings },
            // Calf Raises (14)
            new ExerciseMuscleGroup { Id = 27, ExerciseTemplateId = 14, MuscleGroup = MuscleGroup.Calves },
            // Wall Sit (15)
            new ExerciseMuscleGroup { Id = 28, ExerciseTemplateId = 15, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 29, ExerciseTemplateId = 15, MuscleGroup = MuscleGroup.Glutes },
            // Burpees (16)
            new ExerciseMuscleGroup { Id = 30, ExerciseTemplateId = 16, MuscleGroup = MuscleGroup.FullBody },
            // High Knees (17)
            new ExerciseMuscleGroup { Id = 31, ExerciseTemplateId = 17, MuscleGroup = MuscleGroup.FullBody },
            // Jumping Jacks (18)
            new ExerciseMuscleGroup { Id = 32, ExerciseTemplateId = 18, MuscleGroup = MuscleGroup.FullBody },
            // Bear Crawl (19)
            new ExerciseMuscleGroup { Id = 33, ExerciseTemplateId = 19, MuscleGroup = MuscleGroup.FullBody },
            new ExerciseMuscleGroup { Id = 34, ExerciseTemplateId = 19, MuscleGroup = MuscleGroup.Core },
            // Inchworm (20)
            new ExerciseMuscleGroup { Id = 35, ExerciseTemplateId = 20, MuscleGroup = MuscleGroup.FullBody },
            new ExerciseMuscleGroup { Id = 36, ExerciseTemplateId = 20, MuscleGroup = MuscleGroup.Hamstrings },
            // Dumbbell Shoulder Press (21)
            new ExerciseMuscleGroup { Id = 37, ExerciseTemplateId = 21, MuscleGroup = MuscleGroup.Shoulders },
            new ExerciseMuscleGroup { Id = 38, ExerciseTemplateId = 21, MuscleGroup = MuscleGroup.Triceps },
            // Dumbbell Bicep Curl (22)
            new ExerciseMuscleGroup { Id = 39, ExerciseTemplateId = 22, MuscleGroup = MuscleGroup.Biceps },
            // Dumbbell Tricep Extension (23)
            new ExerciseMuscleGroup { Id = 40, ExerciseTemplateId = 23, MuscleGroup = MuscleGroup.Triceps },
            // Dumbbell Lateral Raises (24)
            new ExerciseMuscleGroup { Id = 41, ExerciseTemplateId = 24, MuscleGroup = MuscleGroup.Shoulders },
            // Dumbbell Bent Over Row (25)
            new ExerciseMuscleGroup { Id = 42, ExerciseTemplateId = 25, MuscleGroup = MuscleGroup.Back },
            new ExerciseMuscleGroup { Id = 43, ExerciseTemplateId = 25, MuscleGroup = MuscleGroup.Lats },
            new ExerciseMuscleGroup { Id = 44, ExerciseTemplateId = 25, MuscleGroup = MuscleGroup.Biceps },
            // Dumbbell Chest Press (26)
            new ExerciseMuscleGroup { Id = 45, ExerciseTemplateId = 26, MuscleGroup = MuscleGroup.Chest },
            new ExerciseMuscleGroup { Id = 46, ExerciseTemplateId = 26, MuscleGroup = MuscleGroup.Triceps },
            new ExerciseMuscleGroup { Id = 47, ExerciseTemplateId = 26, MuscleGroup = MuscleGroup.Shoulders },
            // Dumbbell Chest Fly (27)
            new ExerciseMuscleGroup { Id = 48, ExerciseTemplateId = 27, MuscleGroup = MuscleGroup.Chest },
            new ExerciseMuscleGroup { Id = 49, ExerciseTemplateId = 27, MuscleGroup = MuscleGroup.Shoulders },
            // Dumbbell Squat (28)
            new ExerciseMuscleGroup { Id = 50, ExerciseTemplateId = 28, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 51, ExerciseTemplateId = 28, MuscleGroup = MuscleGroup.Glutes },
            new ExerciseMuscleGroup { Id = 52, ExerciseTemplateId = 28, MuscleGroup = MuscleGroup.Hamstrings },
            // Dumbbell Lunges (29)
            new ExerciseMuscleGroup { Id = 53, ExerciseTemplateId = 29, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 54, ExerciseTemplateId = 29, MuscleGroup = MuscleGroup.Glutes },
            // Dumbbell Romanian Deadlift (30)
            new ExerciseMuscleGroup { Id = 55, ExerciseTemplateId = 30, MuscleGroup = MuscleGroup.Hamstrings },
            new ExerciseMuscleGroup { Id = 56, ExerciseTemplateId = 30, MuscleGroup = MuscleGroup.Glutes },
            new ExerciseMuscleGroup { Id = 57, ExerciseTemplateId = 30, MuscleGroup = MuscleGroup.Back },
            // Dumbbell Calf Raises (31)
            new ExerciseMuscleGroup { Id = 58, ExerciseTemplateId = 31, MuscleGroup = MuscleGroup.Calves },
            // Kettlebell Swing (32)
            new ExerciseMuscleGroup { Id = 59, ExerciseTemplateId = 32, MuscleGroup = MuscleGroup.FullBody },
            new ExerciseMuscleGroup { Id = 60, ExerciseTemplateId = 32, MuscleGroup = MuscleGroup.Glutes },
            new ExerciseMuscleGroup { Id = 61, ExerciseTemplateId = 32, MuscleGroup = MuscleGroup.Hamstrings },
            // Kettlebell Goblet Squat (33)
            new ExerciseMuscleGroup { Id = 62, ExerciseTemplateId = 33, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 63, ExerciseTemplateId = 33, MuscleGroup = MuscleGroup.Glutes },
            // Kettlebell Turkish Get Up (34)
            new ExerciseMuscleGroup { Id = 64, ExerciseTemplateId = 34, MuscleGroup = MuscleGroup.FullBody },
            new ExerciseMuscleGroup { Id = 65, ExerciseTemplateId = 34, MuscleGroup = MuscleGroup.Shoulders },
            new ExerciseMuscleGroup { Id = 66, ExerciseTemplateId = 34, MuscleGroup = MuscleGroup.Core },
            // Kettlebell Clean and Press (35)
            new ExerciseMuscleGroup { Id = 67, ExerciseTemplateId = 35, MuscleGroup = MuscleGroup.Shoulders },
            new ExerciseMuscleGroup { Id = 68, ExerciseTemplateId = 35, MuscleGroup = MuscleGroup.FullBody },
            // Pull Ups (36)
            new ExerciseMuscleGroup { Id = 69, ExerciseTemplateId = 36, MuscleGroup = MuscleGroup.Lats },
            new ExerciseMuscleGroup { Id = 70, ExerciseTemplateId = 36, MuscleGroup = MuscleGroup.Back },
            new ExerciseMuscleGroup { Id = 71, ExerciseTemplateId = 36, MuscleGroup = MuscleGroup.Biceps },
            // Chin Ups (37)
            new ExerciseMuscleGroup { Id = 72, ExerciseTemplateId = 37, MuscleGroup = MuscleGroup.Biceps },
            new ExerciseMuscleGroup { Id = 73, ExerciseTemplateId = 37, MuscleGroup = MuscleGroup.Lats },
            new ExerciseMuscleGroup { Id = 74, ExerciseTemplateId = 37, MuscleGroup = MuscleGroup.Back },
            // Dead Hang (38)
            new ExerciseMuscleGroup { Id = 75, ExerciseTemplateId = 38, MuscleGroup = MuscleGroup.Lats },
            new ExerciseMuscleGroup { Id = 76, ExerciseTemplateId = 38, MuscleGroup = MuscleGroup.Shoulders },
            // Hanging Knee Raises (39)
            new ExerciseMuscleGroup { Id = 77, ExerciseTemplateId = 39, MuscleGroup = MuscleGroup.Core },
            new ExerciseMuscleGroup { Id = 78, ExerciseTemplateId = 39, MuscleGroup = MuscleGroup.Lats },
            // Resistance Band Pull Apart (40)
            new ExerciseMuscleGroup { Id = 79, ExerciseTemplateId = 40, MuscleGroup = MuscleGroup.Shoulders },
            new ExerciseMuscleGroup { Id = 80, ExerciseTemplateId = 40, MuscleGroup = MuscleGroup.Back },
            // Resistance Band Squat (41)
            new ExerciseMuscleGroup { Id = 81, ExerciseTemplateId = 41, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 82, ExerciseTemplateId = 41, MuscleGroup = MuscleGroup.Glutes },
            // Resistance Band Row (42)
            new ExerciseMuscleGroup { Id = 83, ExerciseTemplateId = 42, MuscleGroup = MuscleGroup.Back },
            new ExerciseMuscleGroup { Id = 84, ExerciseTemplateId = 42, MuscleGroup = MuscleGroup.Biceps },
            // Resistance Band Bicep Curl (43)
            new ExerciseMuscleGroup { Id = 85, ExerciseTemplateId = 43, MuscleGroup = MuscleGroup.Biceps },
            // Resistance Band Glute Kickback (44)
            new ExerciseMuscleGroup { Id = 86, ExerciseTemplateId = 44, MuscleGroup = MuscleGroup.Glutes },
            new ExerciseMuscleGroup { Id = 87, ExerciseTemplateId = 44, MuscleGroup = MuscleGroup.Hamstrings },
            // Child's Pose (45)
            new ExerciseMuscleGroup { Id = 88, ExerciseTemplateId = 45, MuscleGroup = MuscleGroup.Back },
            // Cat Cow (46)
            new ExerciseMuscleGroup { Id = 89, ExerciseTemplateId = 46, MuscleGroup = MuscleGroup.Back },
            new ExerciseMuscleGroup { Id = 90, ExerciseTemplateId = 46, MuscleGroup = MuscleGroup.Core },
            // Superman Hold (47)
            new ExerciseMuscleGroup { Id = 91, ExerciseTemplateId = 47, MuscleGroup = MuscleGroup.Back },
            new ExerciseMuscleGroup { Id = 92, ExerciseTemplateId = 47, MuscleGroup = MuscleGroup.Glutes },
            // Cobra Stretch (48)
            new ExerciseMuscleGroup { Id = 93, ExerciseTemplateId = 48, MuscleGroup = MuscleGroup.Back },
            new ExerciseMuscleGroup { Id = 94, ExerciseTemplateId = 48, MuscleGroup = MuscleGroup.Core },
            // Hip Flexor Stretch (49)
            new ExerciseMuscleGroup { Id = 95, ExerciseTemplateId = 49, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 96, ExerciseTemplateId = 49, MuscleGroup = MuscleGroup.Glutes },
            // Dead Bug (50)
            new ExerciseMuscleGroup { Id = 97, ExerciseTemplateId = 50, MuscleGroup = MuscleGroup.Core },
            new ExerciseMuscleGroup { Id = 98, ExerciseTemplateId = 50, MuscleGroup = MuscleGroup.Back },
            // Treadmill Run (51)
            new ExerciseMuscleGroup { Id = 99, ExerciseTemplateId = 51, MuscleGroup = MuscleGroup.FullBody },
            // Stationary Bike (52)
            new ExerciseMuscleGroup { Id = 100, ExerciseTemplateId = 52, MuscleGroup = MuscleGroup.Quads },
            new ExerciseMuscleGroup { Id = 101, ExerciseTemplateId = 52, MuscleGroup = MuscleGroup.Hamstrings },
            new ExerciseMuscleGroup { Id = 102, ExerciseTemplateId = 52, MuscleGroup = MuscleGroup.Calves },
            // Rowing Machine (53)
            new ExerciseMuscleGroup { Id = 103, ExerciseTemplateId = 53, MuscleGroup = MuscleGroup.FullBody },
            new ExerciseMuscleGroup { Id = 104, ExerciseTemplateId = 53, MuscleGroup = MuscleGroup.Back },
            // Elliptical (54)
            new ExerciseMuscleGroup { Id = 105, ExerciseTemplateId = 54, MuscleGroup = MuscleGroup.FullBody },
            // Wall Sit (15) — missing Hamstrings
            new ExerciseMuscleGroup { Id = 106, ExerciseTemplateId = 15, MuscleGroup = MuscleGroup.Hamstrings },
            // Bicycle Crunches (6) — Obliques
            new ExerciseMuscleGroup { Id = 107, ExerciseTemplateId = 6, MuscleGroup = MuscleGroup.Obliques },
            // Russian Twists (9) — Obliques
            new ExerciseMuscleGroup { Id = 108, ExerciseTemplateId = 9, MuscleGroup = MuscleGroup.Obliques }
        );
    }
}
