public class Payment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public Patient? Patient { get; set; }
}