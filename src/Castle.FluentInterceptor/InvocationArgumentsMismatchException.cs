using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castle.DynamicProxy
{
	[Serializable]
	public class InvocationArgumentsMismatchException : Exception
	{
		public InvocationArgumentsMismatchException() { }
		public InvocationArgumentsMismatchException(string message) : base(message) { }
		public InvocationArgumentsMismatchException(string message, Exception inner) : base(message, inner) { }
		protected InvocationArgumentsMismatchException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
