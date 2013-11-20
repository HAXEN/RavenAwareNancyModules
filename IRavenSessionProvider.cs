using Raven.Client;

namespace HAXEN.RavenAwareNancyModules
{
    public interface IRavenSessionProvider
    {
        IDocumentSession GetSession();
    }
}