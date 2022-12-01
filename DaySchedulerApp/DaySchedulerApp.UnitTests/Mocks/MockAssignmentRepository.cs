using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.UnitTests.Mocks
{
    public class MockAssignmentRepository
    {
        public static Mock<IAssignmentRepository> GetAssignmentRepository()
        {
            var assignments = new List<Assignment>
            {
                new Assignment
                {
                    Id = "BsonId_1",
                    Name= "Test",
                    Description = "Test Description 1",
                    FrequencyInDays= 1,
                    SendNotification= true,
                    LatestCompletion = new DateTime(2019,05,25),
                    NextCompletion = new DateTime(2019,05,26),
                },
                new Assignment
                {
                    Id = "BsonId_2",
                    Name= "Test 1",
                    Description = "Test Description 2",
                    FrequencyInDays= 5,
                    SendNotification= true,
                    LatestCompletion = new DateTime(2019,06,04),
                    NextCompletion = new DateTime(2019,06,09),
                },
            };

            var mockRepo = new Mock<IAssignmentRepository>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(assignments);

            mockRepo.Setup(r => r.GetById(It.IsAny<string>())).ReturnsAsync(assignments.First());

            mockRepo.Setup(r => r.Add(It.IsAny<Assignment>())).ReturnsAsync((Assignment assignment) =>
            {
                assignments.Add(assignment);
                return assignment;
            });


            return mockRepo;
        }
    }
}
