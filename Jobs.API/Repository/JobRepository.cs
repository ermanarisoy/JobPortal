using Jobs.API.Data;
using Jobs.API.Interface;
using Jobs.API.Entities;
using MongoDB.Driver;

namespace Jobs.API.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly IJobContext _context;
        private readonly IBadWordRepository _badWordRepository;

        public JobRepository(IJobContext context, IBadWordRepository badWordRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _badWordRepository = badWordRepository ?? throw new ArgumentNullException(nameof(badWordRepository));
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            return await _context
                            .Jobs
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Job> GetJob(string id)
        {
            return await _context
                           .Jobs
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Job>> GetJobByDescription(string description)
        {
            FilterDefinition<Job> filter = Builders<Job>.Filter.Eq(p => p.Description, description);

            return await _context
                            .Jobs
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobByPosition(string position)
        {
            FilterDefinition<Job> filter = Builders<Job>.Filter.Eq(p => p.Position, position);

            return await _context
                            .Jobs
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task CreateJob(Job job)
        {
            if (!string.IsNullOrEmpty(job.Description))
            {
                job.Quality++;
            }
            if (!string.IsNullOrEmpty(job.WorkingType))
            {
                job.Quality++;
            }
            if (job.Salary > 0)
            {
                job.Quality++;
            }
            job.Quality += 2;
            foreach (var item in _badWordRepository.GetBadWords().Result)
            {
                if (job.Description.Contains(item.Word))
                {
                    job.Quality -= 2;
                    break;
                }
            }
            job.ReleaseDate = DateTime.Now;
            job.EndDate = job.ReleaseDate.AddDays(15);
            await _context.Jobs.InsertOneAsync(job);
        }

        public async Task<bool> UpdateJob(Job job)
        {
            var updateResult = await _context
                                        .Jobs
                                        .ReplaceOneAsync(filter: g => g.Id == job.Id, replacement: job);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteJob(string id)
        {
            FilterDefinition<Job> filter = Builders<Job>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Jobs
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        #region

        //private readonly JobsAPIContext _context;
        //public JobRepository(JobsAPIContext context)
        //{
        //    _context = context;
        //}

        //public async Task DeleteJob(Job job)
        //{
        //    _context.Job.Remove(job);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<Job>> GetJob()
        //{
        //    return await _context.Job.ToListAsync();
        //}

        //public async Task<Job> GetJob(int id)
        //{
        //    return await _context.Job.FindAsync(id);
        //}

        //public async Task<Job> PostJob(Job job)
        //{
        //    if (!string.IsNullOrEmpty(job.Description))
        //    {
        //        job.Quality++;
        //    }
        //    if (!string.IsNullOrEmpty(job.WorkingType))
        //    {
        //        job.Quality++;
        //    }
        //    if (job.Salary > 0)
        //    {
        //        job.Quality++;
        //    }
        //    job.ReleaseDate = DateTime.Now;
        //    job.EndDate = job.ReleaseDate.AddDays(15);
        //    _context.Job.Add(job);
        //    await _context.SaveChangesAsync();
        //    return job;
        //}

        //public async Task PutJob(Job job)
        //{
        //    _context.Entry(job).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}

        //public bool JobExists(int id)
        //{
        //    return (_context.Job?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
        #endregion
    }
}
