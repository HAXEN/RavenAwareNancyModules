using Raven.Client;

namespace HAXEN.RavenAwareNancyModules
{
    public interface INeedDocumentSession
    {
        IDocumentSession DocumentSession { set; }
    }
}