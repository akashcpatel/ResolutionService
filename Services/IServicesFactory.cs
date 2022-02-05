namespace Services
{
    public interface IServicesFactory
    {
        IUserService GetUserService();
        IResolutionService GetResolutionService();
    }
}
