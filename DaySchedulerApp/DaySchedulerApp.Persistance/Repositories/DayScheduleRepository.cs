using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Domain;
using DaySchedulerApp.Persistance.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DaySchedulerApp.Persistance.Repositories
{
    public class DayScheduleRepository : IDayScheduleRepository
    {
        private readonly IMongoCollection<DaySchedule> _daySchedulesCollection;

        public DayScheduleRepository(IOptions<DaySchedulerDatabaseSettings> daySchedulerDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                daySchedulerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                daySchedulerDatabaseSettings.Value.DatabaseName);

            _daySchedulesCollection = mongoDatabase.GetCollection<DaySchedule>(
                daySchedulerDatabaseSettings.Value.DaySchedulesCollectionName);
        }

        public async Task<DaySchedule> Add(DaySchedule entity)
        {
            await _daySchedulesCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task Delete(string id)
        {
            await _daySchedulesCollection.DeleteOneAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DaySchedule>> GetAllAsync()
        {
            return await _daySchedulesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<DaySchedule> GetById(string id)
        {
            return await _daySchedulesCollection.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(string id, DaySchedule entity)
        {
            await _daySchedulesCollection.ReplaceOneAsync(d => d.Id == entity.Id, entity);
        }
    }
}