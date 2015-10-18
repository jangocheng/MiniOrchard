namespace MiniOrchard.Mvc.Filters
{
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class FilterResolvingActionInvoker : ControllerActionInvoker
	{
		public readonly IEnumerable<IGlobalFilter> _filters;

		public FilterResolvingActionInvoker(IEnumerable<IGlobalFilter> filters)
		{
			_filters = filters;
		}

		protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var filters = base.GetFilters(controllerContext, actionDescriptor);
			foreach (var filter in _filters)
			{
				AddFilters(filter, filters);
			}

			return filters;
		}

		protected void AddFilters(IGlobalFilter filter, FilterInfo filterInfo)
		{
			if (filter is IAuthorizationFilter)
				filterInfo.AuthorizationFilters.Add(filter as IAuthorizationFilter);
			if (filter is IActionFilter)
				filterInfo.ActionFilters.Add(filter as IActionFilter);
			if (filter is IResultFilter)
				filterInfo.ResultFilters.Add(filter as IResultFilter);
			if (filter is IExceptionFilter)
				filterInfo.ExceptionFilters.Add(filter as IExceptionFilter);
		}
	}
}
