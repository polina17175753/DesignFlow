using System.Text.Json;

namespace DesignFlow;

public sealed class DataStore
{
    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "designflow-data.json");
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public DesignFlowData Data { get; private set; }

    public DataStore()
    {
        try
        {
            Data = File.Exists(_filePath)
                ? JsonSerializer.Deserialize<DesignFlowData>(File.ReadAllText(_filePath), Options) ?? new()
                : new();
        }
        catch (JsonException)
        {
            Data = new();
            Console.WriteLine("Файл данных повреждён. Будет создано новое хранилище.");
        }
    }

    public DesignProject? FindProject(int id) => Data.Projects.FirstOrDefault(p => p.Id == id);

    public void Save() => File.WriteAllText(_filePath, JsonSerializer.Serialize(Data, Options));
}
