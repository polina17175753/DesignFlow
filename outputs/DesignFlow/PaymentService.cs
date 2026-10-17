namespace DesignFlow;

public sealed class PaymentService(DataStore store)
{
    public Payment Add(int projectId, decimal amount, string note)
    {
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
