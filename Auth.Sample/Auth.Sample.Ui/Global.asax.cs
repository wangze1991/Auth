using System;
using Autofac;
using Autofac.Integration.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using Auth.Sample.Application;
using Auth.Sample.Repository;
using Auth.Sample.Domain.Dto.Mapping;
namespace Auth.Sample.Ui
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            #region Autofac 依赖注入

            var builder = new ContainerBuilder();//构建自己的容器，同时注册所有的控制器以及他们的依赖项

            //注册所有的控制器
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //注册模型绑定器
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            //注册删选属性
            builder.RegisterFilterProvider();
            //添加HTTP请求生命周期域的注册  eg:HttpContextBase,HttpRequestBase
            builder.RegisterModule<AutofacWebTypesModule>();

            // builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();
            // builder.RegisterType<Web_ExceptionLogManager>().As<IWeb_ExceptionLogManager>().InstancePerHttpRequest(); //从HTTP请求中重到注入点
            // builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //       .Where(x => x.Name.EndsWith("Repository")&&typeof(IRepository<>).IsAssignableFrom(x))
            //      .AsImplementedInterfaces().InstancePerLifetimeScope();//InstancePerLifetimeScope 保证生命周期基于请求
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(x => x.Name.EndsWith("Application"))
            //       .AsImplementedInterfaces().InstancePerLifetimeScope();//InstancePerLifetimeScope 保证生命周期基于请求
            builder.RegisterType<ModuleRepository>().As<IModuleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ModuleApplication>().As<IModuleApplication>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserApplication>().As<IUserApplication>().InstancePerLifetimeScope();
            builder.RegisterType<FormAuthencationApplication>().As<IAuthenticationApplication>().InstancePerLifetimeScope();
            builder.RegisterType<AccountApplicaton>().As<IAccountApplicaton>().InstancePerLifetimeScope();
            builder.RegisterType<ButtonRepository>().As<IButtonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ButtonApplication>().As<IButtonApplication>().InstancePerLifetimeScope();
            builder.RegisterType<DeparatmentRepository>().As<IDepartmentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentApplication>().As<IDepartmentApplication>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleApplication>().As<IRoleApplication>().InstancePerLifetimeScope();
            builder.RegisterType<ModuleButtonRepository>().As<IModuleButtonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ModuleButtonApplication>().As<IModuleButtonApplication>().InstancePerLifetimeScope();
            builder.RegisterType<RoleModuleButtonRepository>().As<IRoleModuleButtonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleModuleButtonApplication>().As<IRoleModuleButtonApplication>().InstancePerLifetimeScope();
            builder.RegisterType<RoleUserRepository>().As<IRoleUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleUserApplication>().As<IRoleUserApplication>().InstancePerLifetimeScope();
            builder.RegisterType<WorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            #endregion

            #region tinyMapper 注入
            TinyMapperConfig.RegisterMapperConfig();
            #endregion
        }
    }
}
