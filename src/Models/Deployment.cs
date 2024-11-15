namespace Models;

public class Deployment(DateTime startDate, DateTime endDate)
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;
}