namespace INSS.Web.Components.Resolvers;

public sealed class TestUserSessionResolver : IUserSessionResolver
{
    public string GetUserId()
    {
        return "0c4d0123-854b-4929-8a75-6b89c6619909";
    }
}