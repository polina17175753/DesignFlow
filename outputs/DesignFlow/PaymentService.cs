namespace DesignFlow;

public sealed class PaymentService(DataStore store)
{
    public Payment Add(int projectId, decimal amount, string note)
    {
        if (store.FindProject(projectId) is null)
            throw new ArgumentOutOfRangeException(nameof(projectId), "Проект не найден.");
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма платежа должна быть больше нуля.");
        ArgumentException.ThrowIfNullOrWhiteSpace(note);

        var payment = new Payment
        {
            Id = store.Data.Payments.Count == 0 ? 1 : store.Data.Payments.Max(p => p.Id) + 1,
            ProjectId = projectId,
            Amount = amount,
            Note = note,
            Date = DateTime.Today
        };
        store.Data.Payments.Add(payment);
        store.Save();
        return payment;
    }

    public decimal TotalFor(int projectId) => store.Data.Payments
        .Where(p => p.ProjectId == projectId)
        .Sum(p => p.Amount);
}
