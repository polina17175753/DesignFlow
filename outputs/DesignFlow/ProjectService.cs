namespace DesignFlow;

public sealed class ProjectService(DataStore store)
{
    public DesignProject Add(string name, string client)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(client);

        var project = new DesignProject
        {
            Id = store.Data.Projects.Count == 0 ? 1 : store.Data.Projects.Max(p => p.Id) + 1,
            Name = name,
            Client = client
        };
        store.Data.Projects.Add(project);
        store.Save();
        return project;
    }

    public bool SetStatus(int id, ProjectStatus status)
    {
        var project = store.FindProject(id);
        if (project is null) return false;
        project.Status = status;
        store.Save();
        return true;
    }
}
