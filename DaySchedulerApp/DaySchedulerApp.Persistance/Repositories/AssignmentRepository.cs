using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Domain;
using DaySchedulerApp.Persistance.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DaySchedulerApp.Persistance.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly IMongoCollection<Assignment> _assignmentCollection;

        public AssignmentRepository(IOptions<DaySchedulerDatabaseSettings> daySchedulerDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                daySchedulerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                daySchedulerDatabaseSettings.Value.DatabaseName);

            _assignmentCollection = mongoDatabase.GetCollection<Assignment>(
                daySchedulerDatabaseSettings.Value.AssignmentsCollectionName);
        }

        public async Task Add(Assignment entity)
        {
            await _assignmentCollection.InsertOneAsync(entity);
        }

        public async Task Delete(Assignment entity)
        {
            await _assignmentCollection.DeleteOneAsync(a => a.Id == entity.Id);
        }

        public async Task<IEnumerable<Assignment>> GetAllAsync()
        {
            return await _assignmentCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Assignment> GetById(string id)
        {
            return await _assignmentCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(Assignment entity)
        {
            await _assignmentCollection.ReplaceOneAsync(a => a.Id == entity.Id, entity);
        }
    }
}