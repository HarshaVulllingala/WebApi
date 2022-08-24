
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.WebApi;
using WebApi.Database;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //container.RegisterType<UserService>(new InjectionConstructor(IUserrepository));

            
            container.RegisterType<IUserService, UserService>(new InjectionConstructor(typeof(IUserrepository)));
            container.RegisterType<IUserrepository, Userrepository>(new InjectionConstructor(typeof(DatabaseCon)));


            
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

        }
    }
}