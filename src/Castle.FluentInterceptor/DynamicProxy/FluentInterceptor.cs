using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Castle.DynamicProxy.FluentInterceptorHelpers;

namespace Castle.DynamicProxy
{
	public class FluentInterceptor<T> : IInterceptor
		where T : class
	{
		List<FluentInterceptedMember<T>> _dispatchers = new List<FluentInterceptedMember<T>>();

		/// <summary>
		/// Intercept property set.
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public IFluentInterceptedMember<T> PropertySet(Expression<Func<T, object>> expression)
		{
			return SetupSetProperty(expression);
		}

		/// <summary>
		/// Intercept a property get like.:
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public IFluentInterceptedMember<T> PropertyGet(Expression<Func<T, object>> expression)
		{
			return SetupGetProperty(expression);
		}

		/// <summary>
		/// Intercept method that returns a value.
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public IFluentInterceptedMember<T> Method(Expression<Func<T, object>> expression)
		{
			return SetupMethod(expression);
		}

		/// <summary>
		/// Intercept method that returns void.
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public IFluentInterceptedMember<T> Method(Expression<Action<T>> expression)
		{
			return SetupMethod(expression);
		}

		FluentInterceptedMember<T> SetupMethod(LambdaExpression expression)
		{
			var methodCall = expression.GetMethodCallExpression();
			var methodInfo = methodCall.Method;
			var arguments = methodCall.ExtractArguments().Select(p => p.Value).ToArray();

			for (int i = 0; i < arguments.Length; i++)
			{
				var exprArg = methodCall.Arguments[i];
				arguments[i] = GetConfiguredParameterValue(arguments[i], exprArg);
			}

			var dispatcher = new FluentInterceptedMember<T>(methodInfo, arguments);

			_dispatchers.Add(dispatcher);

			return dispatcher;
		}

		FluentInterceptedMember<T> SetupGetProperty(LambdaExpression expression)
		{
			var propertyExpression = expression.GetMemberAssignmentExpression();
			var property = propertyExpression.Member as PropertyInfo;

			if (property.GetMethod.GetParameters().Length != 0)
			{
				throw new Exception("Property with parameters cannot be intercepted.");
			}

			var dispatcher = new FluentInterceptedMember<T>(property.GetMethod, new object[0]);

			_dispatchers.Add(dispatcher);

			return dispatcher;
		}

		FluentInterceptedMember<T> SetupSetProperty(LambdaExpression expression)
		{
			var propertyExpression = expression.GetMemberAssignmentExpression();
			var property = propertyExpression.Member as PropertyInfo;

			var arguments = property.SetMethod.GetParameters().Select(p => new MatchValue()).ToArray();

			var dispatcher = new FluentInterceptedMember<T>(property.SetMethod, arguments);

			_dispatchers.Add(dispatcher);

			return dispatcher;
		}

		object GetConfiguredParameterValue(object actualValue, Expression exprArg)
		{
			var methodCall = exprArg as MethodCallExpression;
			if (methodCall != null)
			{
				if (methodCall.Method.DeclaringType == typeof(Any))
				{
					return new MatchValue();
				}
			}

			return actualValue;
		}

		FluentInterceptedMember<T>[] FindInterceptedMembers(IInvocation invocation)
		{
			return _dispatchers.Where(p => p.Match(invocation)).ToArray();
		}

		void IInterceptor.Intercept(IInvocation invocation)
		{
			var interceptedMembers = FindInterceptedMembers(invocation);

			foreach (var intercepted in interceptedMembers)
			{
				intercepted.BeforeAction(invocation);
			}

			if (interceptedMembers.All(p => p.Proceed))
			{
				invocation.Proceed();
			}

			foreach (var intercepted in interceptedMembers)
			{
				intercepted.AfterAction(invocation);
			}
		}
	}

}
