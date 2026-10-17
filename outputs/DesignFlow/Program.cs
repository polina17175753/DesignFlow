using DesignFlow;

var store = new DataStore();
var projects = new ProjectService(store);
var tasks = new TaskService(store);
var payments = new PaymentService(store);

while (true)
{
    switch (ChooseMainMenuItem())
    {
        case 0: ProjectMenu(); break;
        case 1: TaskMenu(); break;
        case 2: PaymentMenu(); break;
        case 3: return;
    }
}

int ChooseMainMenuItem()
{
    string[] items = ["Управление проектами", "Управление задачами", "Учёт платежей", "Выход"];
    var selected = 0;

    while (true)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║              DESIGNFLOW                  ║");
        Console.WriteLine("║   Система автоматизации работы дизайнера ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.WriteLine("▼ Выберите раздел (↑/↓, Enter):");
        Console.WriteLine("┌──────────────────────────────────────────┐");

        for (var i = 0; i < items.Length; i++)
        {
            var marker = i == selected ? "›" : " ";
            Console.WriteLine($"│ {marker} {items[i],-39}│");
        }

        Console.WriteLine("└──────────────────────────────────────────┘");
        var key = Console.ReadKey(intercept: true).Key;
        if (key == ConsoleKey.UpArrow) selected = (selected + items.Length - 1) % items.Length;
        else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % items.Length;
        else if (key == ConsoleKey.Enter) return selected;
    }
}

void ProjectMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Управление проектами ===");
        Console.WriteLine("1. Добавить проект\n2. Показать проекты\n3. Изменить статус\n0. Назад");
        Console.Write("Выберите действие: ");
        switch (Console.ReadLine()?.Trim())
        {
            case "1":
                var name = ReadRequired("Название проекта: ");
                var client = ReadRequired("Клиент: ");
                projects.Add(name, client);
                Pause("Проект добавлен.");
                break;
            case "2": ShowProjects(); break;
            case "3":
                ShowProjects(false);
                var projectId = ReadInt("ID проекта: ");
                Console.WriteLine("Статус: 1 — Запланирован, 2 — В работе, 3 — Завершён");
                var status = ReadInt("Новый статус: ");
                if (status is < 1 or > 3) Pause("Неверный номер статуса.");
                else Pause(projects.SetStatus(projectId, (ProjectStatus)(status - 1)) ? "Статус обновлён." : "Проект не найден.");
                break;
            case "0": return;
            default: Pause("Неизвестный пункт меню."); break;
        }
    }
}

void TaskMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Управление задачами ===");
        Console.WriteLine("1. Создать задачу\n2. Показать задачи\n3. Отметить задачу выполненной\n0. Назад");
        Console.Write("Выберите действие: ");
        switch (Console.ReadLine()?.Trim())
        {
            case "1":
                ShowProjects(false);
                var projectId = ReadInt("ID проекта для задачи: ");
                if (store.FindProject(projectId) is null) { Pause("Проект не найден."); break; }
                tasks.Add(projectId, ReadRequired("Название задачи: "));
                Pause("Задача создана.");
                break;
            case "2": ShowTasks(); break;
            case "3":
                ShowTasks(false);
                var taskId = ReadInt("ID задачи: ");
                Pause(tasks.Complete(taskId) ? "Задача отмечена выполненной." : "Открытая задача не найдена.");
                break;
            case "0": return;
            default: Pause("Неизвестный пункт меню."); break;
        }
    }
}

void PaymentMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Учёт платежей ===");
        Console.WriteLine("1. Записать оплату\n2. Показать платежи и суммы\n0. Назад");
        Console.Write("Выберите действие: ");
        switch (Console.ReadLine()?.Trim())
        {
            case "1":
                ShowProjects(false);
                var projectId = ReadInt("ID проекта: ");
                if (store.FindProject(projectId) is null) { Pause("Проект не найден."); break; }
                var amount = ReadAmount("Сумма оплаты: ");
                var note = ReadRequired("Комментарий (например, аванс или финальный платёж): ");
                payments.Add(projectId, amount, note);
                Pause("Платёж записан.");
                break;
            case "2": ShowPayments(); break;
            case "0": return;
            default: Pause("Неизвестный пункт меню."); break;
        }
    }
}

void ShowProjects(bool pause = true)
{
    Console.Clear();
    Console.WriteLine("=== Проекты ===");
    if (store.Data.Projects.Count == 0) Console.WriteLine("Проектов пока нет.");
    foreach (var p in store.Data.Projects)
        Console.WriteLine($"ID {p.Id} | {p.Name} | Клиент: {p.Client} | Статус: {p.Status.DisplayName()}");
    if (pause) Wait();
}

void ShowTasks(bool pause = true)
{
    Console.Clear();
    Console.WriteLine("=== Задачи ===");
    if (store.Data.Tasks.Count == 0) Console.WriteLine("Задач пока нет.");
    foreach (var task in store.Data.Tasks)
    {
        var project = store.FindProject(task.ProjectId);
        Console.WriteLine($"ID {task.Id} | Проект: {project?.Name ?? "удалён"} | {task.Title} | {(task.IsCompleted ? "Выполнена" : "Открыта")}");
    }
    if (pause) Wait();
}

void ShowPayments()
{
    Console.Clear();
    Console.WriteLine("=== Платежи ===");
    if (store.Data.Payments.Count == 0) Console.WriteLine("Платежей пока нет.");
    foreach (var payment in store.Data.Payments)
        Console.WriteLine($"{payment.Date:dd.MM.yyyy} | {store.FindProject(payment.ProjectId)?.Name ?? "удалён"} | {payment.Note} | {payment.Amount:N2} ₽");
    Console.WriteLine("\nИтого по проектам:");
    foreach (var project in store.Data.Projects)
        Console.WriteLine($"{project.Name}: {payments.TotalFor(project.Id):N2} ₽");
    Console.WriteLine($"Всего получено: {store.Data.Payments.Sum(p => p.Amount):N2} ₽");
    Wait();
}

string ReadRequired(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        var value = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(value)) return value;
        Console.WriteLine("Поле не может быть пустым.");
    }
}

int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out var value) && value > 0) return value;
        Console.WriteLine("Введите положительное целое число.");
    }
}

decimal ReadAmount(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (decimal.TryParse(Console.ReadLine(), out var value) && value > 0) return value;
        Console.WriteLine("Введите сумму больше нуля (например, 1500 или 1500,50).");
    }
}

void Pause(string message) { Console.WriteLine(message); Wait(); }
void Wait() { Console.WriteLine("\nНажмите Enter, чтобы продолжить..."); Console.ReadLine(); }
