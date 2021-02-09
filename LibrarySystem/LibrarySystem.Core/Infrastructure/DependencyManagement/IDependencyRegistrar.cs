using Autofac;
using LibrarySystem.Core.Infrastructure.TypeFinders;

namespace LibrarySystem.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
