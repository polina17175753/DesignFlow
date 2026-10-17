namespace DesignFlow;

public enum ProjectStatus { Planned, InProgress, Completed }

public static class ProjectStatusExtensions
{
    public static string DisplayName(this ProjectStatus status) => status switch
    {
        ProjectStatus.Planned => "Запланирован",
        ProjectStatus.InProgress => "В работе",
        ProjectStatus.Completed => "Завершён",
        _ => "Неизвестно"
    };
}

public sealed class DesignProject
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Client { get; set; } = "";
    public ProjectStatus Status { get; set; } = ProjectStatus.Planned;
}

public sealed class DesignTask
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; }
}

public sealed class Payment
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Today;
}

public sealed class DesignFlowData
{
    public List<DesignProject> Projects { get; set; } = [];
    public List<DesignTask> Tasks { get; set; } = [];
    public List<Payment> Payments { get; set; } = [];
}
