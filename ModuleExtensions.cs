using Nancy;
using Raven.Client;

namespace HAXEN.RavenAwareNancyModules
{
    public static class ModuleExtensions
    {
        public static void EnsureRavenSession(this INancyModule module, NancyContext context, IRavenSessionProvider sessionProvider)
        {
            var documentSessionModule = module as INeedDocumentSession;
            if (documentSessionModule != null)
            {
                context.Items.Add("ModuleRavenSession", sessionProvider.GetSession());
                documentSessionModule.DocumentSession = (IDocumentSession)context.Items["ModuleRavenSession"];
                module.After.AddItemToStartOfPipeline(nancyContext =>
                {
                    var session = (IDocumentSession)nancyContext.Items["ModuleRavenSession"];
                    if (session.Advanced.HasChanges)
                        session.SaveChanges();

                    session.Dispose();
                    nancyContext.Items["ModuleRavenSession"] = null;
                });
            }
            
        }
    }
}
