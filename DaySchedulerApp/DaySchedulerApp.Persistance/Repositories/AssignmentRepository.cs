using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Domain;
using DaySchedulerApp.Persistance.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;

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
            return await _collection.Find(a => a.UserId == userId && a.SendNotification == true).ToListAsync();
        }
    }
}