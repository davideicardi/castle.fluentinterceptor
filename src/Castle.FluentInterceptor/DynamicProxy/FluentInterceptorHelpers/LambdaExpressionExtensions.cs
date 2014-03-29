using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Castle.DynamicProxy.FluentInterceptorHelpers
{
	public static class LambdaExpressionExtensions
	{
		public static MethodCallExpression GetMethodCallExpression(this LambdaExpression expression)
		{
			var call = expression.Body as MethodCallExpression;
			if (call == null)
			{
				throw new InvalidOperationException("Expression must be a method call");
			}

			if (call.Object != expression.Parameters[0])
			{
				throw new InvalidOperationException("Method call must target lambda argument");
			}

			return call;
		}

		public static MemberExpression GetMemberAssignmentExpression(this LambdaExpression expression)
		{
			var unary = expression.Body as UnaryExpression;
			if (unary == null)
			{
				throw new InvalidOperationException("Expression must be a property starting with a property.");
			}

			var assignement = unary.Operand as MemberExpression;
			if (assignement == null)
			{
				throw new InvalidOperationException("Expression must be a property.");
			}

			return assignement;
		}

		public static NewExpression GetNewExpression(this LambdaExpression expression)
		{
			var newExpression = expression.Body as NewExpression;
			if (newExpression == null)
				throw new Exception("Constructor not valid.");

			return newExpression;
		}

		public static Dictionary<ParameterInfo, object> ExtractArguments(this NewExpression expression)
		{
			return ExtractArguments(expression.Constructor.GetParameters(), expression.Arguments);
		}

		public static Dictionary<ParameterInfo, object> ExtractArguments(this MethodCallExpression expression)
		{
			return ExtractArguments(expression.Method.GetParameters(), expression.Arguments);
		}

		private static Dictionary<ParameterInfo, object> ExtractArguments(ParameterInfo[] methodParameters, IEnumerable<Expression> expressionArguments)
		{
			var extractedArguments = new Dictionary<ParameterInfo, object>();

			int i = 0;
			foreach (var argExp in expressionArguments)
			{
				object argumentValue = EvalExpression(argExp);

				extractedArguments.Add(methodParameters[i], argumentValue);

				i++;
			}

			return extractedArguments;
		}

		private static object EvalExpression(Expression expression)
		{
			if (expression == null)
			{
				return null;
			}

			object argumentValue;

			MemberExpression argumentMemberExpression;
			NewExpression argumentNewExpression;
			ConstantExpression constantExpression;
			MethodCallExpression methodCallExpression;

			if ((argumentMemberExpression = expression as MemberExpression) != null)
			{
				var argumentMemberTarget = EvalExpression(argumentMemberExpression.Expression);

				if (argumentMemberExpression.Member is FieldInfo)
				{
					var field = argumentMemberExpression.Member as FieldInfo;

					argumentValue = field.GetValue(argumentMemberTarget);
				}
				else if (argumentMemberExpression.Member is PropertyInfo)
				{
					var property = argumentMemberExpression.Member as PropertyInfo;

					argumentValue = property.GetValue(argumentMemberTarget);
				}
				else
				{
					throw new Exception("Argument expression member not supported.");
				}
			}
			else if ((argumentNewExpression = expression as NewExpression) != null)
			{
				var innerArguments = ExtractArguments(argumentNewExpression);
				argumentValue = argumentNewExpression.Constructor.Invoke(innerArguments);
			}
			else if ((constantExpression = expression as ConstantExpression) != null)
			{
				argumentValue = constantExpression.Value;
			}
			else if ((methodCallExpression = expression as MethodCallExpression) != null)
			{
				var innerArguments = ExtractArguments(methodCallExpression);
				var innerTarget = EvalExpression(methodCallExpression.Object);
				argumentValue = methodCallExpression.Method.Invoke(innerTarget, innerArguments);
			}
			else
			{
				throw new ArgumentException("Cannot extract argument, unknown expression type.");
			}

			return argumentValue;
		}

		public static object Invoke(this ConstructorInfo constructor, Dictionary<ParameterInfo, object> arguments)
		{
			var parameters = arguments.Values.ToArray();
			return constructor.Invoke(parameters);
		}

		public static object Invoke(this MethodInfo method, object target, Dictionary<ParameterInfo, object> arguments)
		{
			var parameters = arguments.Values.ToArray();
			return method.Invoke(target, parameters);
		}
	}
}
