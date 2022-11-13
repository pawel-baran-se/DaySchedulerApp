using DaySchedulerApp.Application.Contracts;
using DaySchedulerApp.Domain;
using DaySchedulerApp.Persistance.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task Add(DaySchedule entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(DaySchedule entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DaySchedule>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DaySchedule> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(DaySchedule entity)
        {
            throw new NotImplementedException();
        }
    }
}
