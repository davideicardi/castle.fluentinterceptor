using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Castle.DynamicProxy.FluentInterceptorHelpers
{
	public partial class FluentInterceptedMember<T> : IFluentInterceptedMember<T>, IFluentInterceptedMember_PrecededBy<T>, IFluentInterceptedMember_FollowedBy<T>
		where T : class
	{
		readonly MethodInfo _memberInfo;
		readonly object[] _arguments;

		public FluentInterceptedMember(MethodInfo memberInfo, object[] arguments)
		{
			_memberInfo = memberInfo;
			_arguments = arguments;

			BeforeAction = NullAction;
			AfterAction = NullAction;
			Proceed = true;
		}

		public Action<IInvocation> BeforeAction { get; private set; }
		public Action<IInvocation> AfterAction { get; private set; }

		public bool Proceed { get; private set; }

		public bool Match(IInvocation invocation)
		{
			if (invocation.Method != _memberInfo)
				return false;

			for (int i = 0; i < invocation.Arguments.Length; i++)
			{
				var argumentToMatch = _arguments[i];

				var matchValue = argumentToMatch as MatchValue;
				if (matchValue != null)
				{
					if (!matchValue.Match(invocation.Arguments[i]))
					{
						return false;
					}
				}
				else
				{
					if (!object.Equals(invocation.Arguments[i], argumentToMatch))
					{
						return false;
					}
				}
			}

			return true;
		}

		public FluentInterceptedMember<T> Before(Action<IInvocation> action)
		{
			BeforeAction = action;

			return this;
		}

		public FluentInterceptedMember<T> After(Action<IInvocation> action)
		{
			AfterAction = action;

			return this;
		}

		public FluentInterceptedMember<T> DoNotProceed()
		{
			Proceed = false;

			return this;
		}

		static void NullAction(IInvocation invocation)
		{

		}

		#region Fluent Interfaces
		IFluentInterceptedMember_PrecededBy<T> IFluentInterceptedMember<T>.PrecededBy(Action action)
		{
			Before((i) => action());
			return this;
		}

		IFluentInterceptedMember_FollowedBy<T> IFluentInterceptedMember<T>.FollowedBy(Action action)
		{
			After((i) => action());
			return this;
		}

		void IFluentInterceptedMember<T>.ReplaceWith(Action action)
		{
			Before((i) => action());
			DoNotProceed();
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TReturn>(Func<TReturn> action)
		{
			Before((i) => i.ReturnValue = action());
			DoNotProceed();
		}

		void IFluentInterceptedMember_PrecededBy<T>.FollowedBy(Action action)
		{
			After((i) => action());
		}

		void IFluentInterceptedMember_FollowedBy<T>.PrecededBy(Action action)
		{
			Before((i) => action());
		}
		#endregion

		void CheckArguments(params Type[] arguments)
		{
			if (_memberInfo.GetParameters().Length < arguments.Length)
			{
				throw new InvocationArgumentsMismatchException(string.Format("Interceptor is configured with a wrong number of parameters, invocation is performed with {0} parameters.", arguments.Length));
			}

			for (int i = 0; i < _memberInfo.GetParameters().Length; i++)
			{
				var expectedType = _memberInfo.GetParameters()[i].ParameterType;
				if (arguments[i] != expectedType)
				{
					throw new InvocationArgumentsMismatchException(string.Format("Interceptor is configured with a wrong parameter type. Parameter {0} should be a {1}.", i + 1, expectedType));
				}
			}
		}
	}
}
