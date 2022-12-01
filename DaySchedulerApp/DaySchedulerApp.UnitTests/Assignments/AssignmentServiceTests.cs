using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.UnitTests.Assignments
{
    public class AssignmentServiceTests
    {
        private readonly Mock<IAssignmentRepository> _mockRepo;

        public AssignmentServiceTests()
        {
            _mockRepo = MockAssignmentRepository.GetAssignmentRepository();
        }
    }
}
