using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Domain;
using DaySchedulerApp.Persistance.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DaySchedulerApp.Persistance.Repositories
{
    public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        private IMongoCollection<Assignment> _collection;

        public AssignmentRepository(IOptions<DaySchedulerDatabaseSettings> daySchedulerDatabaseSettings)
            : base(daySchedulerDatabaseSettings, daySchedulerDatabaseSettings.Value.AssignmentsCollectionName)
        {
            var mongoClient = new MongoClient(
                daySchedulerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                daySchedulerDatabaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<Assignment>(daySchedulerDatabaseSettings.Value.AssignmentsCollectionName);
        }

        public async Task<List<Assignment>> GetByUserId(string userId)
        {
            return await _collection.Find(a => a.UserId == userId).ToListAsync();
        }

        public async Task<List<Assignment>> GetNotifiableForUserById(string userId)
        {
            var assignments = await GetByUserId(userId);
            return assignments.Where(a => a.SendNotification == true).ToList();
        }

        public async Task<List<Assignment>> GetCurrentAssignmentsForUser(string userId)
        {
            var today = DateTime.Now;
            var assignments = await GetNotifiableForUserById(userId);
            return assignments.Where(a => a.NextCompletion.Year == today.Year && a.NextCompletion.Month == today.Month && a.NextCompletion.Day == today.Day).ToList();
        }
    }
}