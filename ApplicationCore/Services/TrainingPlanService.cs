using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Exercise;
using ApplicationCore.Models.TrainingPlan;
using ApplicationCore.Specifications;
using Ardalis.Result;
using System.Numerics;

namespace ApplicationCore.Services
{
    public class TrainingPlanService : ITrainingPlanService
    {
        IRepository<PersonalTrainer> _personalTrainerRepository;
        IRepository<Client> _clientRepository;
        IRepository<Exercise> _exerciseRepository;
        IRepository<TrainingPlan> _trainingPlanRepository;

        public TrainingPlanService(
            IRepository<Client> clientRepository,
            IRepository<PersonalTrainer> personalTrainerRepository,
            IRepository<Exercise> exerciseRepository,
            IRepository<TrainingPlan> trainingPlanRepository)
        {
            _clientRepository = clientRepository;
            _exerciseRepository = exerciseRepository;
            _trainingPlanRepository = trainingPlanRepository;
            _personalTrainerRepository = personalTrainerRepository;
        }

        public async Task<Result> AddNewExercise(NewExerciseModel model, string userId)
        {
            var _trainerSpec = new FindPersonalTrainerByUserId(userId);
            var trainer = await _personalTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var trainingPlan = await _trainingPlanRepository.GetByIdAsync(model.Id);
            if (trainingPlan == null) {
                return Result.Error("nie znaleziono planu treningowego");
            }

            var exercise = new Exercise
            {
                Name = model.Name,
                Description = model.Description,
                SeriesNumber = model.SeriesNumber,
                RepetitionsNumber = model.RepetitionsNumber,
                RestTime = model.RestTime,
                TrainingPlanId = trainingPlan.Id,
            };

            await _exerciseRepository.AddAsync(exercise);

            return Result.Success();
        }

        public async Task<Result> CreateTrainingPlan(NewTrainingPlanModel model, string userId)
        {
            var _trainerSpec = new FindPersonalTrainerByUserId(userId);
            var trainer = await _personalTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var client = await _clientRepository.GetByIdAsync(model.ClientId);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            var trainingPlan = new TrainingPlan
            {
                Name = model.Name,
                Description = model.Description,
                ClientId = client.Id,
                PersonalTrainerId = trainer.Id,
            };

            var trainingPlanEntity = await _trainingPlanRepository.AddAsync(trainingPlan);
            await _trainingPlanRepository.SaveChangesAsync();

            if (model.Exercises.Count > 0)
            {
                var exercises = new List<Exercise>();

                foreach (var exercise in model.Exercises)
                {
                    var newExercise = new Exercise
                    {
                        Name = exercise.Name,
                        Description = exercise.Description,
                        RepetitionsNumber = exercise.RepetitionsNumber,
                        SeriesNumber = exercise.SeriesNumber,
                        RestTime = exercise.RestTime,
                        TrainingPlanId = trainingPlanEntity.Id,
                    };

                    exercises.Add(newExercise);
                }

                await _exerciseRepository.AddRangeAsync(exercises);
                await _exerciseRepository.SaveChangesAsync();
            }

            return Result.Success();
        }

        public async Task<Result> DeleteExercise(int exerciseId, string userId)
        {
            var _trainerSpec = new FindPersonalTrainerByUserId(userId);
            var trainer = await _personalTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
            
            if(exercise == null)
            {
                return Result.Error("Nie znaleziono ćwiczenia");
            }

            await _exerciseRepository.DeleteAsync(exercise);
            await _exerciseRepository.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> DeletePlan(int planId, string userId)
        {
            var _trainerSpec = new FindPersonalTrainerByUserId(userId);
            var trainer = await _personalTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var trainingPlan = await _trainingPlanRepository.GetByIdAsync(planId);

            if (trainingPlan == null)
            {
                return Result.Error("Nie znaleziono planu");
            }

            await _trainingPlanRepository.DeleteAsync(trainingPlan);

            return Result.Success();
        }

        public async Task<Result> EditExercise(EditExerciseModel model, string userId)
        {
            var _trainerSpec = new FindPersonalTrainerByUserId(userId);
            var trainer = await _personalTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var exercise = await _exerciseRepository.GetByIdAsync(model.Id);

            if (exercise == null)
            {
                return Result.Error("Nie znaleziono ćwiczenia");
            }

            exercise.Name = model.Name;
            exercise.Description = model.Description;
            exercise.SeriesNumber = model.SeriesNumber;
            exercise.RepetitionsNumber = model.RepetitionsNumber;
            exercise.RestTime = model.RestTime;

            await _exerciseRepository.UpdateAsync(exercise);

            return Result.Success();

        }

        public async Task<Result> UpdateTrainingPlan(EditTrainingPlanModel model, string userId)
        {
            var _trainerSpec = new FindPersonalTrainerByUserId(userId);
            var trainer = await _personalTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var client = await _clientRepository.GetByIdAsync(model.ClientId);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            var trainingPlan = await _trainingPlanRepository.GetByIdAsync(model.Id);
            if (trainingPlan == null)
            {
                return Result.Error("Nie znaleziono planu treningowego");
            }

            trainingPlan.Name = model.Name;
            trainingPlan.Description = model.Description;
            trainingPlan.ClientId = model.ClientId;

            await _trainingPlanRepository.UpdateAsync(trainingPlan);
            return Result.Success();
        }
    }
}
