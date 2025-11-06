namespace VendorWebAPI.Patterns.Creational
{
    public class SingletonLogger
    {
        private static readonly Lazy<SingletonLogger> _instance =
            new Lazy<SingletonLogger>(() => new SingletonLogger());
        public static SingletonLogger Instance => _instance.Value;
        private SingletonLogger()
        {
        }
        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] {message}");
        }
    }
}
