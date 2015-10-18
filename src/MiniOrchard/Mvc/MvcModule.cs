namespace MiniOrchard.Mvc
{
	using System.Web.Mvc;
	using Autofac;
	using MiniOrchard.Mvc.Filters;

	public class MvcModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<FilterResolvingActionInvoker>().As<IActionInvoker>().InstancePerDependency();
			builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
		}
	}
}
