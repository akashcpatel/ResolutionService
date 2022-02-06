namespace Services.Implementations
{
    internal class ServiceBase
    {
        protected string CreateEndPoint(string baseAddress, string extension) => $"{baseAddress}{extension}";
    }
}
