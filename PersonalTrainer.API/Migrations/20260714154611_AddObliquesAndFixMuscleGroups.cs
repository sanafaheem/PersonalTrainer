using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalTrainer.API.Migrations
{
    /// <inheritdoc />
    public partial class AddObliquesAndFixMuscleGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExerciseTemplates",
                columns: new[] { "Id", "Contraindications", "Instructions", "IsActive", "MaxDurationSeconds", "MaxReps", "MaxSets", "MinDurationSeconds", "MinReps", "MinSets", "Name" },
                values: new object[,]
                {
                    { 1, "wrist injury, shoulder injury", "Start in a plank position with hands shoulder-width apart. Lower your chest to the floor keeping your core tight, then push back up.", true, 60, 20, 4, 20, 5, 2, "Push Ups" },
                    { 2, "wrist injury, elbow injury", "Place hands close together forming a diamond shape. Lower chest toward hands keeping elbows tucked in.", true, 50, 15, 4, 20, 5, 2, "Diamond Push Ups" },
                    { 3, "shoulder injury", "Start in a downward dog position. Bend elbows and lower your head toward the floor, then push back up.", true, 50, 15, 3, 20, 5, 2, "Pike Push Ups" },
                    { 4, "lower back pain", "Hold a straight line from head to heels on your forearms and toes. Engage your core throughout.", true, 90, null, 4, 20, null, 2, "Plank Hold" },
                    { 5, "neck injury, lower back pain", "Lie on your back with knees bent. Lift your shoulders off the floor engaging your core, then lower slowly.", true, 60, 30, 4, 20, 10, 2, "Crunches" },
                    { 6, "neck injury, lower back pain", "Lie on your back, hands behind head. Bring opposite elbow to knee in a cycling motion.", true, 60, 30, 4, 20, 10, 2, "Bicycle Crunches" },
                    { 7, "lower back pain, hip flexor injury", "Lie flat on your back. Keep legs straight and raise them to 90 degrees then lower slowly.", true, 60, 20, 4, 20, 8, 2, "Leg Raises" },
                    { 8, "wrist injury, shoulder injury", "Start in a plank position. Alternate driving knees toward chest in a running motion.", true, 60, null, 4, 20, null, 2, "Mountain Climbers" },
                    { 9, "lower back pain", "Sit with knees bent and feet off the floor. Rotate your torso side to side.", true, 60, 30, 4, 20, 10, 2, "Russian Twists" },
                    { 10, "bad knees, knee injury", "Stand with feet shoulder-width apart. Push hips back and bend knees until thighs are parallel to floor. Push through heels to stand.", true, 60, 25, 4, 20, 8, 2, "Squats" },
                    { 11, "bad knees, knee injury, ankle injury", "Perform a squat then explode upward jumping off the floor. Land softly and go straight into the next rep.", true, 50, 20, 4, 20, 8, 2, "Jump Squats" },
                    { 12, "bad knees, knee injury", "Step forward with one leg and lower your hips until both knees are at 90 degrees. Push back to start and alternate legs.", true, 60, 20, 4, 20, 8, 2, "Lunges" },
                    { 13, "lower back pain", "Lie on your back with knees bent. Drive hips upward squeezing glutes at the top, then lower slowly.", true, 60, 25, 4, 20, 10, 2, "Glute Bridges" },
                    { 14, "ankle injury", "Stand with feet hip-width apart. Rise onto the balls of your feet, hold briefly, then lower.", true, 60, 30, 4, 20, 10, 2, "Calf Raises" },
                    { 15, "bad knees, knee injury", "Slide your back down a wall until thighs are parallel to the floor. Hold the position.", true, 90, null, 4, 20, null, 2, "Wall Sit" },
                    { 16, "bad knees, wrist injury, shoulder injury", "From standing, drop to a push up position, perform a push up, jump feet forward, then explode upward with arms overhead.", true, 60, 15, 4, 20, 5, 2, "Burpees" },
                    { 17, "knee injury, ankle injury", "Run in place driving knees as high as possible. Pump arms in rhythm.", true, 60, null, 4, 20, null, 2, "High Knees" },
                    { 18, "ankle injury, shoulder injury", "Jump feet wide while raising arms overhead, then jump back together.", true, 60, null, 4, 20, null, 2, "Jumping Jacks" },
                    { 19, "wrist injury, shoulder injury", "On hands and feet with knees hovering just off the floor. Move forward alternating opposite hand and foot.", true, 60, null, 3, 20, null, 2, "Bear Crawl" },
                    { 20, "lower back pain, hamstring injury", "Stand and fold forward. Walk hands out to a plank, then walk feet to hands and stand.", true, 60, 15, 3, 20, 5, 2, "Inchworm" },
                    { 21, "shoulder injury", "Hold dumbbells at shoulder height. Press overhead until arms are extended, then lower slowly.", true, 60, 15, 4, 30, 8, 2, "Dumbbell Shoulder Press" },
                    { 22, "elbow injury", "Hold dumbbells with palms facing up. Curl toward shoulders keeping elbows tucked, then lower slowly.", true, 60, 15, 4, 30, 8, 2, "Dumbbell Bicep Curl" },
                    { 23, "elbow injury, shoulder injury", "Hold one dumbbell overhead with both hands. Lower behind your head bending at elbows, then extend back up.", true, 60, 15, 4, 30, 8, 2, "Dumbbell Tricep Extension" },
                    { 24, "shoulder injury", "Hold dumbbells at sides. Raise arms out to the side to shoulder height then lower slowly.", true, 60, 15, 4, 30, 8, 2, "Dumbbell Lateral Raises" },
                    { 25, "lower back pain", "Hinge forward at the hips. Pull dumbbells toward your ribcage squeezing shoulder blades, then lower.", true, 60, 15, 4, 30, 8, 2, "Dumbbell Bent Over Row" },
                    { 26, "shoulder injury", "Lie on your back with dumbbells at chest height. Press upward until arms are extended then lower slowly.", true, 60, 15, 4, 30, 8, 2, "Dumbbell Chest Press" },
                    { 27, "shoulder injury", "Lie on your back holding dumbbells above chest. Open arms wide in an arc then bring back together.", true, 60, 12, 4, 30, 8, 2, "Dumbbell Chest Fly" },
                    { 28, "bad knees, knee injury", "Hold dumbbells at sides or shoulders. Perform a squat keeping chest up and knees tracking over toes.", true, 60, 20, 4, 30, 8, 2, "Dumbbell Squat" },
                    { 29, "bad knees, knee injury", "Hold dumbbells at sides. Step forward and lower until both knees are at 90 degrees then return.", true, 60, 16, 4, 30, 8, 2, "Dumbbell Lunges" },
                    { 30, "lower back pain, hamstring injury", "Hold dumbbells in front. Hinge at hips keeping back straight lowering weights along legs, then drive hips forward to stand.", true, 60, 15, 4, 30, 8, 2, "Dumbbell Romanian Deadlift" },
                    { 31, "ankle injury", "Hold dumbbells at sides. Rise onto balls of feet hold briefly then lower.", true, 60, 25, 4, 20, 10, 2, "Dumbbell Calf Raises" },
                    { 32, "lower back pain, shoulder injury", "Hinge at hips holding kettlebell. Drive hips forward explosively swinging the bell to chest height, then hinge back.", true, 60, 25, 4, 20, 10, 2, "Kettlebell Swing" },
                    { 33, "bad knees, knee injury", "Hold kettlebell at chest with both hands. Squat deep keeping chest up and elbows inside knees.", true, 60, 20, 4, 20, 8, 2, "Kettlebell Goblet Squat" },
                    { 34, "shoulder injury, wrist injury", "Lie holding kettlebell overhead. Follow a sequence to stand while keeping the bell pressed overhead, then reverse.", true, 90, 6, 3, 30, 2, 2, "Kettlebell Turkish Get Up" },
                    { 35, "shoulder injury, wrist injury", "Clean kettlebell to shoulder then press overhead. Lower with control and repeat.", true, 60, 12, 4, 20, 6, 2, "Kettlebell Clean and Press" },
                    { 36, "shoulder injury, elbow injury", "Hang from bar with overhand grip. Pull yourself up until chin clears the bar then lower slowly.", true, 60, 15, 4, 20, 2, 2, "Pull Ups" },
                    { 37, "shoulder injury, elbow injury", "Hang from bar with underhand grip. Pull yourself up until chin clears the bar then lower slowly.", true, 60, 15, 4, 20, 2, 2, "Chin Ups" },
                    { 38, "shoulder injury", "Hang from bar with both hands. Relax your shoulders and hang for the prescribed time.", true, 60, null, 4, 15, null, 2, "Dead Hang" },
                    { 39, "shoulder injury, hip flexor injury", "Hang from bar. Drive knees toward chest engaging core then lower with control.", true, 60, 20, 4, 20, 8, 2, "Hanging Knee Raises" },
                    { 40, "shoulder injury", "Hold band at chest width with both hands. Pull apart horizontally squeezing shoulder blades, then return.", true, 60, 25, 4, 20, 10, 2, "Resistance Band Pull Apart" },
                    { 41, "bad knees, knee injury", "Stand on band holding ends at shoulders. Perform a squat keeping tension throughout.", true, 60, 25, 4, 20, 10, 2, "Resistance Band Squat" },
                    { 42, "lower back pain", "Anchor band at waist height. Hold ends and pull toward your torso squeezing shoulder blades.", true, 60, 20, 4, 20, 10, 2, "Resistance Band Row" },
                    { 43, "elbow injury", "Stand on band with palms facing up. Curl hands toward shoulders keeping elbows tucked.", true, 60, 20, 4, 20, 10, 2, "Resistance Band Bicep Curl" },
                    { 44, "hip injury, knee injury", "Loop band around ankles on all fours. Kick one leg back squeezing glute at the top, then alternate.", true, 60, 20, 4, 20, 10, 2, "Resistance Band Glute Kickback" },
                    { 45, "knee injury", "Kneel and sit back on heels. Extend arms forward resting forehead on mat. Breathe deeply.", true, 60, null, 3, 20, null, 1, "Child's Pose" },
                    { 46, "lower back pain", "On all fours alternate arching your back toward the ceiling then dropping it toward the floor.", true, 60, 20, 3, 20, 8, 2, "Cat Cow Stretch" },
                    { 47, "lower back pain", "Lie face down with arms extended. Simultaneously lift arms and legs off the mat, hold briefly then lower.", true, 60, 15, 4, 20, 8, 2, "Superman Hold" },
                    { 48, "lower back pain, wrist injury", "Lie face down with hands under shoulders. Press up lifting chest while keeping hips on mat.", true, 60, null, 3, 20, null, 2, "Cobra Stretch" },
                    { 49, "knee injury, hip injury", "Kneel on one knee with the other foot forward. Drive hips forward feeling the stretch in the front of the back hip.", true, 60, null, 3, 20, null, 2, "Hip Flexor Stretch" },
                    { 50, "lower back pain", "Lie on back with arms up and knees at 90 degrees. Lower opposite arm and leg toward floor while keeping lower back pressed down.", true, 60, 20, 4, 20, 8, 2, "Dead Bug" },
                    { 51, "bad knees, ankle injury", "Run at a comfortable pace on the treadmill. Maintain upright posture and land midfoot.", true, 300, null, 3, 60, null, 1, "Treadmill Run" },
                    { 52, "knee injury", "Cycle at a steady pace maintaining consistent cadence. Adjust resistance to appropriate level.", true, 300, null, 3, 60, null, 1, "Stationary Bike" },
                    { 53, "lower back pain, shoulder injury", "Drive with legs first then lean back and pull handle to lower chest. Return in reverse order.", true, 300, null, 3, 60, null, 1, "Rowing Machine" },
                    { 54, "ankle injury", "Use the elliptical at a steady pace pushing and pulling the handles while maintaining upright posture.", true, 300, null, 3, 60, null, 1, "Elliptical" }
                });

            migrationBuilder.InsertData(
                table: "ExerciseEquipment",
                columns: new[] { "Id", "EquipmentType", "ExerciseTemplateId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 2, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 6, 4 },
                    { 6, 2, 5 },
                    { 7, 6, 5 },
                    { 8, 2, 6 },
                    { 9, 6, 6 },
                    { 10, 2, 7 },
                    { 11, 6, 7 },
                    { 12, 2, 8 },
                    { 13, 2, 9 },
                    { 14, 6, 9 },
                    { 15, 2, 10 },
                    { 16, 2, 11 },
                    { 17, 2, 12 },
                    { 18, 2, 13 },
                    { 19, 6, 13 },
                    { 20, 2, 14 },
                    { 21, 2, 15 },
                    { 22, 2, 16 },
                    { 23, 2, 17 },
                    { 24, 2, 18 },
                    { 25, 2, 19 },
                    { 26, 2, 20 },
                    { 27, 6, 20 },
                    { 28, 0, 21 },
                    { 29, 0, 22 },
                    { 30, 0, 23 },
                    { 31, 0, 24 },
                    { 32, 0, 25 },
                    { 33, 0, 26 },
                    { 34, 6, 26 },
                    { 35, 0, 27 },
                    { 36, 6, 27 },
                    { 37, 0, 28 },
                    { 38, 0, 29 },
                    { 39, 0, 30 },
                    { 40, 0, 31 },
                    { 41, 4, 32 },
                    { 42, 4, 33 },
                    { 43, 4, 34 },
                    { 44, 6, 34 },
                    { 45, 4, 35 },
                    { 46, 3, 36 },
                    { 47, 3, 37 },
                    { 48, 3, 38 },
                    { 49, 3, 39 },
                    { 50, 1, 40 },
                    { 51, 1, 41 },
                    { 52, 1, 42 },
                    { 53, 1, 43 },
                    { 54, 1, 44 },
                    { 55, 6, 45 },
                    { 56, 6, 46 },
                    { 57, 6, 47 },
                    { 58, 6, 48 },
                    { 59, 6, 49 },
                    { 60, 6, 50 },
                    { 61, 5, 51 },
                    { 62, 5, 52 },
                    { 63, 5, 53 },
                    { 64, 5, 54 }
                });

            migrationBuilder.InsertData(
                table: "ExerciseMuscleGroups",
                columns: new[] { "Id", "ExerciseTemplateId", "MuscleGroup" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 1, 1 },
                    { 3, 1, 2 },
                    { 4, 2, 1 },
                    { 5, 2, 0 },
                    { 6, 3, 2 },
                    { 7, 3, 1 },
                    { 8, 4, 6 },
                    { 9, 4, 2 },
                    { 10, 5, 6 },
                    { 11, 6, 6 },
                    { 12, 7, 6 },
                    { 13, 8, 12 },
                    { 14, 8, 6 },
                    { 15, 9, 6 },
                    { 16, 10, 7 },
                    { 17, 10, 9 },
                    { 18, 10, 8 },
                    { 19, 11, 7 },
                    { 20, 11, 9 },
                    { 21, 11, 12 },
                    { 22, 12, 7 },
                    { 23, 12, 9 },
                    { 24, 12, 8 },
                    { 25, 13, 9 },
                    { 26, 13, 8 },
                    { 27, 14, 10 },
                    { 28, 15, 7 },
                    { 29, 15, 9 },
                    { 30, 16, 12 },
                    { 31, 17, 12 },
                    { 32, 18, 12 },
                    { 33, 19, 12 },
                    { 34, 19, 6 },
                    { 35, 20, 12 },
                    { 36, 20, 8 },
                    { 37, 21, 2 },
                    { 38, 21, 1 },
                    { 39, 22, 3 },
                    { 40, 23, 1 },
                    { 41, 24, 2 },
                    { 42, 25, 4 },
                    { 43, 25, 5 },
                    { 44, 25, 3 },
                    { 45, 26, 0 },
                    { 46, 26, 1 },
                    { 47, 26, 2 },
                    { 48, 27, 0 },
                    { 49, 27, 2 },
                    { 50, 28, 7 },
                    { 51, 28, 9 },
                    { 52, 28, 8 },
                    { 53, 29, 7 },
                    { 54, 29, 9 },
                    { 55, 30, 8 },
                    { 56, 30, 9 },
                    { 57, 30, 4 },
                    { 58, 31, 10 },
                    { 59, 32, 12 },
                    { 60, 32, 9 },
                    { 61, 32, 8 },
                    { 62, 33, 7 },
                    { 63, 33, 9 },
                    { 64, 34, 12 },
                    { 65, 34, 2 },
                    { 66, 34, 6 },
                    { 67, 35, 2 },
                    { 68, 35, 12 },
                    { 69, 36, 5 },
                    { 70, 36, 4 },
                    { 71, 36, 3 },
                    { 72, 37, 3 },
                    { 73, 37, 5 },
                    { 74, 37, 4 },
                    { 75, 38, 5 },
                    { 76, 38, 2 },
                    { 77, 39, 6 },
                    { 78, 39, 5 },
                    { 79, 40, 2 },
                    { 80, 40, 4 },
                    { 81, 41, 7 },
                    { 82, 41, 9 },
                    { 83, 42, 4 },
                    { 84, 42, 3 },
                    { 85, 43, 3 },
                    { 86, 44, 9 },
                    { 87, 44, 8 },
                    { 88, 45, 4 },
                    { 89, 46, 4 },
                    { 90, 46, 6 },
                    { 91, 47, 4 },
                    { 92, 47, 9 },
                    { 93, 48, 4 },
                    { 94, 48, 6 },
                    { 95, 49, 7 },
                    { 96, 49, 9 },
                    { 97, 50, 6 },
                    { 98, 50, 4 },
                    { 99, 51, 12 },
                    { 100, 52, 7 },
                    { 101, 52, 8 },
                    { 102, 52, 10 },
                    { 103, 53, 12 },
                    { 104, 53, 4 },
                    { 105, 54, 12 },
                    { 106, 15, 8 },
                    { 107, 6, 11 },
                    { 108, 9, 11 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "ExerciseEquipment",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "ExerciseMuscleGroups",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "ExerciseTemplates",
                keyColumn: "Id",
                keyValue: 54);
        }
    }
}
