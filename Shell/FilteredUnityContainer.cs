using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Logging;

namespace phirSOFT.Applications.MusicStand
{
    /// <summary>
    /// Todo: There has to be a better way than this
    /// </summary>
    internal class FilteredUnityContainer<T> : IContainerExtension<T>
    {
        private readonly IContainerExtension<T> _containerExtension;

        public FilteredUnityContainer(IContainerExtension<T> containerExtension)
        {
            _containerExtension = containerExtension;

        }

        public object Resolve(Type type)
        {
            return _containerExtension.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return _containerExtension.Resolve(type, name);
        }

        public void RegisterInstance(Type type, object instance)
        {
            _containerExtension.RegisterInstance(type, instance);
        }

        public void RegisterSingleton(Type @from, Type to)
        {
            if (from == typeof(ILoggerFacade))
                return;
            _containerExtension.RegisterSingleton(@from, to);
        }

        public void Register(Type @from, Type to)
        {
            if (from == typeof(ILoggerFacade))
                return;
            _containerExtension.Register(@from, to);
        }

        public void Register(Type @from, Type to, string name)
        {
            if (from == typeof(ILoggerFacade) && string.IsNullOrEmpty(name))
                return;
            _containerExtension.Register(@from, to, name);
        }

        public void FinalizeExtension()
        {
            _containerExtension.FinalizeExtension();
        }

        public object ResolveViewModelForView(object view, Type viewModelType)
        {
            return _containerExtension.ResolveViewModelForView(view, viewModelType);
        }

        public bool SupportsModules => _containerExtension.SupportsModules;
        public T Instance => _containerExtension.Instance;

    }
}
