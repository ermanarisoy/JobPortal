namespace JobPortal.API.Model
{
    public class Job
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Announcement { get; set; }
        public int Duration { get; set; }
        public int AdQuality { get; set; }
        public string Benefits { get; set; }
        public string TypeOfWork { get; set; }
        public decimal Salary { get; set; }
    }
}
