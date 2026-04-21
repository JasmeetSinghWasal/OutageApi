public class Invoices
{
    public Guid Id { get; set; }
    public Guid InvoiceNumber { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaidDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool Status { get; set; }
    public string Notes { get; set; } = String.Empty;

}