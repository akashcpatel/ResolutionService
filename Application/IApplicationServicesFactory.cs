namespace Application
{
    public interface IApplicationServicesFactory
    {
        IUserService GetUserService();
        IResolutionService GetResolutionService();
    }
}
