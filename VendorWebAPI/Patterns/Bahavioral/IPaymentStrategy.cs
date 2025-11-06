namespace VendorWebAPI.Patterns.Bahavioral
{
    public interface IPaymentStrategy
    {
        string pay(decimal amount);
    }
}
