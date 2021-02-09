using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using LibrarySystem.Business;
using LibrarySystem.Core.Data;
using LibrarySystem.Core.Infrastructure;
using LibrarySystem.Core.Infrastructure.DependencyManagement;
using LibrarySystem.Core.Infrastructure.TypeFinders;
using LibrarySystem.Data;
using LibrarySystem.Data.Repositories;
using LibrarySystem.IService;
using LibrarySystem.Service;
using System.Linq;
using System.Reflection;

namespace LibrarySystem.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            #region 数据库

            const string MAIN_DB = "LibrarySystem";

            builder.Register(c => new LibrarySystemDbContext(MAIN_DB))
                    .As<IDbContext>()
                    .Named<IDbContext>(MAIN_DB)
                    .SingleInstance();

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(MAIN_DB))
                .SingleInstance();

            #endregion

            // 注入Business及接口
            //builder.RegisterAssemblyTypes(typeof(UserBusiness).Assembly)
               builder.RegisterAssemblyTypes(typeof(SysAccountBusiness).Assembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();


            //builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
                 builder.RegisterAssemblyTypes(typeof(SysAccountService).Assembly)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
        }
    }
}