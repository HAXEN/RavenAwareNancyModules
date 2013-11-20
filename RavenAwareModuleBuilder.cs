using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing;
using Nancy.Validation;
using Nancy.ViewEngines;

namespace HAXEN.RavenAwareNancyModules
{
    public class RavenAwareModuleBuilder : INancyModuleBuilder
    {
        private readonly IRavenSessionProvider _sessionProvider;
        private readonly IViewFactory _viewFactory;
        private readonly IResponseFormatterFactory _responseFormatterFactory;
        private readonly IModelBinderLocator _modelBinderLocator;
        private readonly IModelValidatorLocator _validatorLocator;

        public RavenAwareModuleBuilder(IRavenSessionProvider sessionProvider, IViewFactory viewFactory, IResponseFormatterFactory responseFormatterFactory, IModelBinderLocator modelBinderLocator, IModelValidatorLocator validatorLocator)
        {
            _sessionProvider = sessionProvider;
            _viewFactory = viewFactory;
            _responseFormatterFactory = responseFormatterFactory;
            _modelBinderLocator = modelBinderLocator;
            _validatorLocator = validatorLocator;
        }

        public INancyModule BuildModule(INancyModule module, NancyContext context)
        {
            module.Context = context;
            module.Response = _responseFormatterFactory.Create(context);
            module.ViewFactory = _viewFactory;
            module.ModelBinderLocator = _modelBinderLocator;
            module.ValidatorLocator = _validatorLocator;

            module.EnsureRavenSession(context, _sessionProvider);

            return module;
        }
    }
}