using Jobs.API.Entities;

namespace Jobs.API.Interface
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetJobs();
        Task<Job> GetJob(string id);
        Task<IEnumerable<Job>> GetJobByDescription(string description);
        Task<IEnumerable<Job>> GetJobByPosition(string position);

        Task CreateJob(Job job);
        Task<bool> UpdateJob(Job job);
        Task<bool> DeleteJob(string id);


        //Task<IEnumerable<Job>> GetJob();
        //Task<Job> GetJob(int id);
        //Task PutJob(Job job);
        //Task<Job> PostJob(Job job);
        //Task DeleteJob(Job job);
        //bool JobExists(int id);
    }
}
