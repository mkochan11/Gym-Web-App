using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Exercise;
using ApplicationCore.Models.TrainingPlan;
using ApplicationCore.Services;
using ApplicationCore.Specifications;
using Moq;

namespace UnitTests.Services
{
    public class TrainingPlanServiceTests
    {
        private Mock<IRepository<PersonalTrainer>> personalTrainerRepositoryMock;
        private Mock<IRepository<Client>> clientRepositoryMock;
        private Mock<IRepository<Exercise>> exerciseRepositoryMock;
        private Mock<IRepository<TrainingPlan>> trainingPlanRepositoryMock;
        private TrainingPlanService service;

        [SetUp]
        public void Setup()
        {
            personalTrainerRepositoryMock = new Mock<IRepository<PersonalTrainer>>();
            clientRepositoryMock = new Mock<IRepository<Client>>();
            exerciseRepositoryMock = new Mock<IRepository<Exercise>>();
            trainingPlanRepositoryMock = new Mock<IRepository<TrainingPlan>>();

            service = new TrainingPlanService(
                clientRepositoryMock.Object,
                personalTrainerRepositoryMock.Object,
                exerciseRepositoryMock.Object,
                trainingPlanRepositoryMock.Object
            );
        }

        [Test]
        public async Task AddNewExerciseShouldReturnSuccess()
        {
            var model = new NewExerciseModel
            {
                Id = 1,
                Name = "Push-ups",
                Description = "Bodyweight exercise",
                SeriesNumber = 3,
                RepetitionsNumber = 15,
                RestTime = new TimeSpan(0, 0, 90)
            };

            var trainer = new PersonalTrainer 
            { 
                Id = 1, 
                AccountId = "userId" 
            };

            var trainingPlan = new TrainingPlan 
            { 
                Id = 1 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default))
                .ReturnsAsync(trainer);

            trainingPlanRepositoryMock
                .Setup(x => x.GetByIdAsync(model.Id, default))
                .ReturnsAsync(trainingPlan);

            exerciseRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Exercise>(), default))
                .ReturnsAsync(new Exercise());

            var result = await service.AddNewExercise(model, "userId");

            Assert.IsTrue(result.IsSuccess);

            personalTrainerRepositoryMock
                .Verify(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default), Times.Once);

            trainingPlanRepositoryMock
                .Verify(x => x.GetByIdAsync(model.Id, default), Times.Once);

            exerciseRepositoryMock
                .Verify(x => x.AddAsync(It.IsAny<Exercise>(), default), Times.Once);
        }

        [Test]
        public async Task CreateTrainingPlanShouldReturnSuccess()
        {
            var model = new NewTrainingPlanModel
            {
                Name = "Full Body Plan",
                Description = "Strength and endurance",
                ClientId = 1,
                Exercises = new List<NewExerciseModel>
                {
                    new NewExerciseModel
                    {
                        Name = "Squats",
                        Description = "Lower body strength",
                        RepetitionsNumber = 10,
                        SeriesNumber = 3,
                        RestTime = new TimeSpan(0, 0, 90)
                    }
                }
            };

            var trainer = new PersonalTrainer 
            { 
                Id = 1, 
                AccountId = "userId"
            };

            var client = new Client 
            { 
                AccountId = "clientId",
                Id = 1 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default))
                .ReturnsAsync(trainer);

            clientRepositoryMock
                .Setup(x => x.GetByIdAsync(model.ClientId, default))
                .ReturnsAsync(client);

            trainingPlanRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<TrainingPlan>(), default))
                .ReturnsAsync(new TrainingPlan { Id = 1 });

            var result = await service.CreateTrainingPlan(model, "userId");

            Assert.IsTrue(result.IsSuccess);

            personalTrainerRepositoryMock
                .Verify(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default), Times.Once);

            clientRepositoryMock
                .Verify(x => x.GetByIdAsync(model.ClientId, default ), Times.Once);

            trainingPlanRepositoryMock
                .Verify(x => x.AddAsync(It.IsAny<TrainingPlan>(), default), Times.Once);

            exerciseRepositoryMock
                .Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<Exercise>>(), default), Times.Once);
        }

        [Test]
        public async Task DeleteExercise_ShouldReturnSuccess_WhenExerciseIsDeleted()
        {
            var trainer = new PersonalTrainer 
            { 
                Id = 1, 
                AccountId = "userId" 
            };

            var exercise = new Exercise 
            { 
                Id = 1 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default))
                .ReturnsAsync(trainer);

            exerciseRepositoryMock
                .Setup(x => x.GetByIdAsync(exercise.Id, default))
                .ReturnsAsync(exercise);

            exerciseRepositoryMock
                .Setup(x => x.DeleteAsync(exercise, default))
                .Returns(Task.CompletedTask);

            var result = await service.DeleteExercise(1, "userId");

            personalTrainerRepositoryMock
                .Verify(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default), Times.Once);

            exerciseRepositoryMock
                .Verify(x => x.GetByIdAsync(exercise.Id, default), Times.Once);

            exerciseRepositoryMock
                .Verify(x => x.DeleteAsync(exercise, default), Times.Once);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task DeletePlan_ShouldReturnSuccess_WhenPlanIsDeleted()
        {
            var trainer = new PersonalTrainer 
            { 
                Id = 1, 
                AccountId = "userId" 
            };

            var trainingPlan = new TrainingPlan 
            { 
                Id = 1 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default))
                .ReturnsAsync(trainer);

            trainingPlanRepositoryMock
                .Setup(x => x.GetByIdAsync(trainingPlan.Id, default))
                .ReturnsAsync(trainingPlan);

            trainingPlanRepositoryMock
                .Setup(x => x.DeleteAsync(trainingPlan, default))
                .Returns(Task.CompletedTask);

            var result = await service.DeletePlan(1, "userId");

            personalTrainerRepositoryMock
                .Verify(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default), Times.Once);

            trainingPlanRepositoryMock
                .Verify(x => x.GetByIdAsync(trainingPlan.Id, default), Times.Once);

            trainingPlanRepositoryMock
                .Verify(x => x.DeleteAsync(trainingPlan, default), Times.Once);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task EditExercise_ShouldReturnSuccess_WhenExerciseIsUpdated()
        {
            var model = new EditExerciseModel
            {
                Id = 1,
                Name = "Updated Exercise",
                Description = "Updated Description",
                SeriesNumber = 4,
                RepetitionsNumber = 12,
                RestTime = new TimeSpan(0, 0, 90)
            };

            var trainer = new PersonalTrainer 
            {
                Id = 1, 
                AccountId = "userId" 
            };

            var exercise = new Exercise 
            { 
                Id = 1 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default))
                .ReturnsAsync(trainer);

            exerciseRepositoryMock
                .Setup(x => x.GetByIdAsync(model.Id, default))
                .ReturnsAsync(exercise);

            exerciseRepositoryMock
                .Setup(x => x.UpdateAsync(exercise, default))
                .Returns(Task.CompletedTask);

            var result = await service.EditExercise(model, "userId");

            personalTrainerRepositoryMock
                .Verify(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default), Times.Once);

            exerciseRepositoryMock
                .Verify(x => x.GetByIdAsync(model.Id, default), Times.Once);

            exerciseRepositoryMock
                .Verify(x => x.UpdateAsync(exercise, default), Times.Once);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task UpdateTrainingPlan_ShouldReturnSuccess_WhenPlanIsUpdated()
        {
            var model = new EditTrainingPlanModel
            {
                Id = 1,
                Name = "Updated Plan",
                Description = "Updated Description",
                ClientId = 2
            };

            var trainer = new PersonalTrainer 
            { 
                Id = 1, 
                AccountId = "userId" 
            };

            var client = new Client 
            { 
                AccountId = "clientId",
                Id = 2 
            };

            var trainingPlan = new TrainingPlan 
            { 
                Id = 1 
            };

            personalTrainerRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default))
                .ReturnsAsync(trainer);

            clientRepositoryMock
                .Setup(x => x.GetByIdAsync(model.ClientId, default))
                .ReturnsAsync(client);

            trainingPlanRepositoryMock
                .Setup(x => x.GetByIdAsync(model.Id, default))
                .ReturnsAsync(trainingPlan);

            trainingPlanRepositoryMock
                .Setup(x => x.UpdateAsync(trainingPlan, default))
                .Returns(Task.CompletedTask);

            var result = await service.UpdateTrainingPlan(model, "userId");

            personalTrainerRepositoryMock
                .Verify(x => x.FirstOrDefaultAsync(It.IsAny<FindPersonalTrainerByUserId>(), default), Times.Once);

            clientRepositoryMock
                .Verify(x => x.GetByIdAsync(model.ClientId, default), Times.Once);

            trainingPlanRepositoryMock
                .Verify(x => x.GetByIdAsync(model.Id, default), Times.Once);

            trainingPlanRepositoryMock
                .Verify(x => x.UpdateAsync(trainingPlan, default), Times.Once);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}