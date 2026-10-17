namespace DesignFlow;

public sealed class TaskService(DataStore store)
{
    public DesignTask Add(int projectId, string title)
    {
        var task = new DesignTask
        {
            Id = store.Data.Tasks.Count == 0 ? 1 : store.Data.Tasks.Max(t => t.Id) + 1,
            ProjectId = projectId,
            Title = title
        };
        store.Data.Tasks.Add(task);
        store.Save();
        return task;
    }

    public bool Complete(int id)
    {
        var task = store.Data.Tasks.FirstOrDefault(t => t.Id == id && !t.IsCompleted);
        if (task is null) return false;
        task.IsCompleted = true;
        store.Save();
        return true;
    }
}
