namespace PropertyService.Configuration
{
    public class RetryOptions
    {
        public int NumberOfRetries { get; set; }

        public int DelayInSeconds { get; set; }
    }
}
