
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Castle.DynamicProxy.FluentInterceptorHelpers
{
	public partial class FluentInterceptedMember<T>
		where T : class
	{
		IFluentInterceptedMember_PrecededBy<T> IFluentInterceptedMember<T>.PrecededBy<TParam1>(Action<TParam1> action)
		{
			CheckArguments(typeof(TParam1)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0));
				});

			return this;
		}

		IFluentInterceptedMember_FollowedBy<T> IFluentInterceptedMember<T>.FollowedBy<TParam1>(Action<TParam1> action)
		{
			CheckArguments(typeof(TParam1)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0));
				});
			return this;
		}

		void IFluentInterceptedMember_FollowedBy<T>.PrecededBy<TParam1>(Action<TParam1> action)
		{
			CheckArguments(typeof(TParam1)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0));
				});
		}

		void IFluentInterceptedMember_PrecededBy<T>.FollowedBy<TParam1>(Action<TParam1> action)
		{
			CheckArguments(typeof(TParam1)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0));
				});
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1>(Action<TParam1> action)
		{
			CheckArguments(typeof(TParam1)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0));
				});

			DoNotProceed();
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TReturn>(Func<TParam1, TReturn> action)
		{
			CheckArguments(typeof(TParam1)); 

			Before((i) =>
				{
					i.ReturnValue = action((TParam1)i.GetArgumentValue(0));
				});

			DoNotProceed();
		}
		IFluentInterceptedMember_PrecededBy<T> IFluentInterceptedMember<T>.PrecededBy<TParam1, TParam2>(Action<TParam1, TParam2> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1));
				});

			return this;
		}

		IFluentInterceptedMember_FollowedBy<T> IFluentInterceptedMember<T>.FollowedBy<TParam1, TParam2>(Action<TParam1, TParam2> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1));
				});
			return this;
		}

		void IFluentInterceptedMember_FollowedBy<T>.PrecededBy<TParam1, TParam2>(Action<TParam1, TParam2> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1));
				});
		}

		void IFluentInterceptedMember_PrecededBy<T>.FollowedBy<TParam1, TParam2>(Action<TParam1, TParam2> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1));
				});
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2>(Action<TParam1, TParam2> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1));
				});

			DoNotProceed();
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2, TReturn>(Func<TParam1, TParam2, TReturn> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2)); 

			Before((i) =>
				{
					i.ReturnValue = action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1));
				});

			DoNotProceed();
		}
		IFluentInterceptedMember_PrecededBy<T> IFluentInterceptedMember<T>.PrecededBy<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2));
				});

			return this;
		}

		IFluentInterceptedMember_FollowedBy<T> IFluentInterceptedMember<T>.FollowedBy<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2));
				});
			return this;
		}

		void IFluentInterceptedMember_FollowedBy<T>.PrecededBy<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2));
				});
		}

		void IFluentInterceptedMember_PrecededBy<T>.FollowedBy<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2));
				});
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2));
				});

			DoNotProceed();
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2, TParam3, TReturn>(Func<TParam1, TParam2, TParam3, TReturn> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3)); 

			Before((i) =>
				{
					i.ReturnValue = action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2));
				});

			DoNotProceed();
		}
		IFluentInterceptedMember_PrecededBy<T> IFluentInterceptedMember<T>.PrecededBy<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3));
				});

			return this;
		}

		IFluentInterceptedMember_FollowedBy<T> IFluentInterceptedMember<T>.FollowedBy<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3));
				});
			return this;
		}

		void IFluentInterceptedMember_FollowedBy<T>.PrecededBy<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3));
				});
		}

		void IFluentInterceptedMember_PrecededBy<T>.FollowedBy<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3));
				});
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2, TParam3, TParam4>(Action<TParam1, TParam2, TParam3, TParam4> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3));
				});

			DoNotProceed();
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2, TParam3, TParam4, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TReturn> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4)); 

			Before((i) =>
				{
					i.ReturnValue = action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3));
				});

			DoNotProceed();
		}
		IFluentInterceptedMember_PrecededBy<T> IFluentInterceptedMember<T>.PrecededBy<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4));
				});

			return this;
		}

		IFluentInterceptedMember_FollowedBy<T> IFluentInterceptedMember<T>.FollowedBy<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4));
				});
			return this;
		}

		void IFluentInterceptedMember_FollowedBy<T>.PrecededBy<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4));
				});
		}

		void IFluentInterceptedMember_PrecededBy<T>.FollowedBy<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4));
				});
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2, TParam3, TParam4, TParam5>(Action<TParam1, TParam2, TParam3, TParam4, TParam5> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4));
				});

			DoNotProceed();
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5)); 

			Before((i) =>
				{
					i.ReturnValue = action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4));
				});

			DoNotProceed();
		}
		IFluentInterceptedMember_PrecededBy<T> IFluentInterceptedMember<T>.PrecededBy<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5), typeof(TParam6)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4), (TParam6)i.GetArgumentValue(5));
				});

			return this;
		}

		IFluentInterceptedMember_FollowedBy<T> IFluentInterceptedMember<T>.FollowedBy<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5), typeof(TParam6)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4), (TParam6)i.GetArgumentValue(5));
				});
			return this;
		}

		void IFluentInterceptedMember_FollowedBy<T>.PrecededBy<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5), typeof(TParam6)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4), (TParam6)i.GetArgumentValue(5));
				});
		}

		void IFluentInterceptedMember_PrecededBy<T>.FollowedBy<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5), typeof(TParam6)); 

			After((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4), (TParam6)i.GetArgumentValue(5));
				});
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5), typeof(TParam6)); 

			Before((i) =>
				{
					action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4), (TParam6)i.GetArgumentValue(5));
				});

			DoNotProceed();
		}

		void IFluentInterceptedMember<T>.ReplaceWith<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn>(Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TReturn> action)
		{
			CheckArguments(typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5), typeof(TParam6)); 

			Before((i) =>
				{
					i.ReturnValue = action((TParam1)i.GetArgumentValue(0), (TParam2)i.GetArgumentValue(1), (TParam3)i.GetArgumentValue(2), (TParam4)i.GetArgumentValue(3), (TParam5)i.GetArgumentValue(4), (TParam6)i.GetArgumentValue(5));
				});

			DoNotProceed();
		}
	}
}


