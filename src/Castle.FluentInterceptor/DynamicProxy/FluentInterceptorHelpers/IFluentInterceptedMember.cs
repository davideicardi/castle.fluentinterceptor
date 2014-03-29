using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castle.DynamicProxy.FluentInterceptorHelpers
{
	public interface IFluentInterceptedMember<T>
		where T : class
	{
		IFluentInterceptedMember_PrecededBy<T> PrecededBy(Action action);
		IFluentInterceptedMember_PrecededBy<T> PrecededBy<TParam1>(Action<TParam1> action);
		IFluentInterceptedMember_PrecededBy<T> PrecededBy<TParam1, TParam2>(Action<TParam1, TParam2> action);
		IFluentInterceptedMember_PrecededBy<T> PrecededBy<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action);
		IFluentInterceptedMember_PrecededBy<T> PrecededBy<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action);
		IFluentInterceptedMember_PrecededBy<T> PrecededBy<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action);
		IFluentInterceptedMember_PrecededBy<T> PrecededBy<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action);

		IFluentInterceptedMember_FollowedBy<T> FollowedBy(Action action);
		IFluentInterceptedMember_FollowedBy<T> FollowedBy<TParam1>(Action<TParam1> action);
		IFluentInterceptedMember_FollowedBy<T> FollowedBy<TParam1, TParam2>(Action<TParam1, TParam2> action);
		IFluentInterceptedMember_FollowedBy<T> FollowedBy<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action);
		IFluentInterceptedMember_FollowedBy<T> FollowedBy<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action);
		IFluentInterceptedMember_FollowedBy<T> FollowedBy<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action);
		IFluentInterceptedMember_FollowedBy<T> FollowedBy<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action);

		void ReplaceWith(Action action);
		void ReplaceWith<TParam1>(Action<TParam1> action);
		void ReplaceWith<TParam1, TParam2>(Action<TParam1, TParam2> action);
		void ReplaceWith<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action);
		void ReplaceWith<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action);
		void ReplaceWith<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action);
		void ReplaceWith<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action);

		void ReplaceWith<TReturn>(Func<TReturn> action);
		void ReplaceWith<TParam1, TReturn>(Func<TParam1, TReturn> action);
		void ReplaceWith<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> action);
		void ReplaceWith<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> action);
		void ReplaceWith<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> action);
		void ReplaceWith<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> action);
		void ReplaceWith<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> action);
	}

	public interface IFluentInterceptedMember_PrecededBy<T>
		where T : class
	{
		void FollowedBy(Action action);
		void FollowedBy<TParam1>(Action<TParam1> action);
		void FollowedBy<TParam1, TParam2>(Action<TParam1, TParam2> action);
		void FollowedBy<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action);
		void FollowedBy<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action);
		void FollowedBy<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action);
		void FollowedBy<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action);
	}

	public interface IFluentInterceptedMember_FollowedBy<T>
		where T : class
	{
		void PrecededBy(Action action);
		void PrecededBy<TParam1>(Action<TParam1> action);
		void PrecededBy<TParam1, TParam2>(Action<TParam1, TParam2> action);
		void PrecededBy<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action);
		void PrecededBy<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action);
		void PrecededBy<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action);
		void PrecededBy<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action);
	}

}
