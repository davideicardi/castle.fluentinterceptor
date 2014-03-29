using Castle.DynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Castle.DynamicProxy
{
	[TestClass]
	public class FluentInterceptorTest
	{
		static ProxyGenerator _proxyGenerator = new ProxyGenerator();
		ISample _proxyObject;
		FluentInterceptor<ISample> _interceptor;
		List<string> _executionList;

		[TestInitialize]
		public void SetUp()
		{
			// TARGET
			_interceptor = new FluentInterceptor<ISample>();

			_executionList = new List<string>();

			_proxyObject = _proxyGenerator.CreateInterfaceProxyWithTarget<ISample>(new SampleClass(_executionList), _interceptor);
		}

		[TestMethod]
		public void VoidMethod_CanBeIntercepted()
		{
			_interceptor.Method(p => p.VoidMethod1())
				.PrecededBy(() => _executionList.Add("VoidMethod1 Before"))
				.FollowedBy(() => _executionList.Add("VoidMethod1 After"));

			_proxyObject.VoidMethod1();

			var expectedInvocations = new[] { 
				"VoidMethod1 Before", "VoidMethod1", "VoidMethod1 After" };

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void VoidMethod_CanBeInterceptedMultipleTimes()
		{
			_interceptor.Method(p => p.VoidMethod1())
				.PrecededBy(() => _executionList.Add("Before1"))
				.FollowedBy(() => _executionList.Add("After1"));

			_interceptor.Method(p => p.VoidMethod1())
				.PrecededBy(() => _executionList.Add("Before2"))
				.FollowedBy(() => _executionList.Add("After2"));

			_proxyObject.VoidMethod1();

			var expectedInvocations = new[] { 
				"Before1", "Before2", "VoidMethod1", "After1", "After2" };

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void VoidMethod_CanBeReplaced()
		{
			_interceptor.Method(p => p.VoidMethod1())
				.ReplaceWith(() => _executionList.Add("VoidMethod1 Replaced"));

			_proxyObject.VoidMethod1();

			var expectedInvocations = new[] { 
				"VoidMethod1 Replaced" };

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void Function_CanBeIntercepted()
		{
			_interceptor.Method(p => p.Function1())
				.PrecededBy(() => _executionList.Add("Function1 Before"))
				.FollowedBy(() => _executionList.Add("Function1 After"));

			var returnValue = _proxyObject.Function1();

			Assert.AreEqual("VALUE1", returnValue);

			var expectedInvocations = new[] { 
				"Function1 Before", "Function1", "Function1 After" };

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void Function_CanBeReplaced()
		{
			_interceptor.Method(p => p.Function1())
				.ReplaceWith(() => 
					{
						_executionList.Add("Function1 Replaced");
						return "REPLACED VALUE";
					});

			var returnValue = _proxyObject.Function1();

			Assert.AreEqual("REPLACED VALUE", returnValue);

			var expectedInvocations = new[] { 
				"Function1 Replaced" };

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void MethodWithParameter_CanBeIntercepted_WithAnyParam()
		{
			_interceptor.Method(p => p.MethodWithParam1(Any.Value<string>()))
				.PrecededBy(() => _executionList.Add("MethodWithParam1 Before"))
				.FollowedBy(() => _executionList.Add("MethodWithParam1 After"));

			_proxyObject.MethodWithParam1("a parameter");

			_proxyObject.MethodWithParam1("another parameter");

			var expectedInvocations = new[] { 
				"MethodWithParam1 Before", "MethodWithParam1", "MethodWithParam1 After",
				"MethodWithParam1 Before", "MethodWithParam1", "MethodWithParam1 After",
			};

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void MethodWith2Parameters_CanBeIntercepted_WithAnyParamMixedWithFilter()
		{
			_interceptor.Method(p => p.MethodWithParam2("A", Any.Value<int>()))
				.PrecededBy(() => _executionList.Add("MA Before"))
				.FollowedBy(() => _executionList.Add("MA After"));

			_interceptor.Method(p => p.MethodWithParam2(Any.Value<string>(), 999))
				.PrecededBy(() => _executionList.Add("M999 Before"))
				.FollowedBy(() => _executionList.Add("M999 After"));

			_proxyObject.MethodWithParam2("A", 123); // intercepted
			_proxyObject.MethodWithParam2("B", 123); // not intercepted

			_proxyObject.MethodWithParam2("ABC", 999); // intercepted
			_proxyObject.MethodWithParam2("ABC", 000); // not intercepted

			var expectedInvocations = new[] { 
				"MA Before", "MethodWithParam2", "MA After",
				"MethodWithParam2",
				"M999 Before", "MethodWithParam2", "M999 After",
				"MethodWithParam2",
			};

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void Interceptor_ExecutionOrderIsPreserved()
		{
			_interceptor.Method(p => p.VoidMethod1())
				.PrecededBy(() => _executionList.Add("M1 Before"))
				.FollowedBy(() => _executionList.Add("M1 After"));
			_interceptor.Method(p => p.VoidMethod2())
				.PrecededBy(() => _executionList.Add("M2 Before"))
				.FollowedBy(() => _executionList.Add("M2 After"));

			_proxyObject.VoidMethod2();
			_proxyObject.VoidMethod1();

			var expectedInvocations = new[] { 
				"M2 Before", "VoidMethod2", "M2 After" ,
				"M1 Before", "VoidMethod1", "M1 After",
			};
			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void MethodParameters_SpecifiedAsConstant_AreUserAsFilter()
		{
			_interceptor.Method(p => p.MethodWithParam1("BBB"))
				.PrecededBy(() => _executionList.Add("Before"))
				.FollowedBy(() => _executionList.Add("After"));

			_proxyObject.MethodWithParam1("AAA");
			_proxyObject.MethodWithParam1("BBB");
			_proxyObject.MethodWithParam1("CCC");

			var expectedInvocations = new[] { 
				"MethodWithParam1",
				"Before", "MethodWithParam1", "After",
				"MethodWithParam1" };
			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void MethodParameters_SpecifiedAsVariable_AreUserAsFilter()
		{
			string BBB = "BBB";

			_interceptor.Method(p => p.MethodWithParam1(BBB))
				.PrecededBy(() => _executionList.Add("Before"))
				.FollowedBy(() => _executionList.Add("After"));

			_proxyObject.MethodWithParam1("AAA");
			_proxyObject.MethodWithParam1("BBB");
			_proxyObject.MethodWithParam1("CCC");

			var expectedInvocations = new[] { 
				"MethodWithParam1",
				"Before", "MethodWithParam1", "After",
				"MethodWithParam1" };
			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void MethodParameters_SpecifiedAsPrivateFunction_AreUserAsFilter()
		{
			_interceptor.Method(p => p.MethodWithParam1(GetBBB()))
				.PrecededBy(() => _executionList.Add("Before"))
				.FollowedBy(() => _executionList.Add("After"));

			_proxyObject.MethodWithParam1("AAA");
			_proxyObject.MethodWithParam1("BBB");
			_proxyObject.MethodWithParam1("CCC");

			var expectedInvocations = new[] { 
				"MethodWithParam1",
				"Before", "MethodWithParam1", "After",
				"MethodWithParam1" };
			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void MethodParameters_SpecifiedAsPrivateProperty_AreUserAsFilter()
		{
			_interceptor.Method(p => p.MethodWithParam1(BBB))
				.PrecededBy(() => _executionList.Add("Before"))
				.FollowedBy(() => _executionList.Add("After"));

			_proxyObject.MethodWithParam1("AAA");
			_proxyObject.MethodWithParam1("BBB");
			_proxyObject.MethodWithParam1("CCC");

			var expectedInvocations = new[] { 
				"MethodWithParam1",
				"Before", "MethodWithParam1", "After",
				"MethodWithParam1" };
			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void MethodParameters_SpecifiedAsPropertyOfAnotherType_AreUserAsFilter()
		{
			var anotherType = new TestOtherClass();

			_interceptor.Method(p => p.MethodWithParam1(anotherType.BBB))
				.PrecededBy(() => _executionList.Add("Before"))
				.FollowedBy(() => _executionList.Add("After"));

			_proxyObject.MethodWithParam1("AAA");
			_proxyObject.MethodWithParam1("BBB");
			_proxyObject.MethodWithParam1("CCC");

			var expectedInvocations = new[] { 
				"MethodWithParam1",
				"Before", "MethodWithParam1", "After",
				"MethodWithParam1" };
			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void PropertySet_CanBeIntercepted()
		{
			_interceptor.PropertySet(p => p.Property1)
				.PrecededBy(() => _executionList.Add("Before"))
				.FollowedBy(() => _executionList.Add("After"));

			_proxyObject.Property1 = 565;

			// property get shouldn't be intercepted
			var getTheValue = _proxyObject.Property1;

			var expectedInvocations = new[] { 
				"Before", "set_Property1", "After",
				"get_Property1"};

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void PropertyGet_CanBeIntercepted()
		{
			_interceptor.PropertyGet(p => p.Property1)
				.PrecededBy(() => _executionList.Add("Before"))
				.FollowedBy(() => _executionList.Add("After"));

			var getTheValue = _proxyObject.Property1;

			// property set shouldn't be intercepted
			_proxyObject.Property1 = 94;

			var expectedInvocations = new[] { 
				"Before", "get_Property1", "After",
				"set_Property1" };

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void InterceptorParameters_UsedInside_PrecededBy_and_FollowedBy()
		{
			_interceptor.Method(p => p.MethodWithParam1(Any.Value<string>()))
				.PrecededBy((string param1) => _executionList.Add(string.Format("MethodWithParam1 {0} Before", param1)))
				.FollowedBy((string param1) => _executionList.Add(string.Format("MethodWithParam1 {0} After", param1)));

			_proxyObject.MethodWithParam1("P1");

			_proxyObject.MethodWithParam1("P2");

			var expectedInvocations = new[] { 
				"MethodWithParam1 P1 Before", "MethodWithParam1", "MethodWithParam1 P1 After",
				"MethodWithParam1 P2 Before", "MethodWithParam1", "MethodWithParam1 P2 After",
			};

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void InterceptorParameters_UsedInside_ReplaceWith()
		{
			_interceptor.Method(p => p.MethodWithParam1(Any.Value<string>()))
				.ReplaceWith((string param1) => _executionList.Add(string.Format("MethodWithParam1 {0} Replace", param1)));

			_proxyObject.MethodWithParam1("P1");

			_proxyObject.MethodWithParam1("P2");

			var expectedInvocations = new[] { 
				"MethodWithParam1 P1 Replace",
				"MethodWithParam1 P2 Replace",
			};

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		public void InterceptorParameters_ForPropertySet()
		{
			_interceptor.PropertySet(p => p.Property1)
				.ReplaceWith((int param1) => _executionList.Add(string.Format("PropertySet called with {0}", param1)));

			_proxyObject.Property1 = 546;

			var expectedInvocations = new[] { 
				"PropertySet called with 546",
			};

			CollectionAssert.AreEqual(expectedInvocations, _executionList);
		}

		[TestMethod]
		[ExpectedException(typeof(InvocationArgumentsMismatchException))]
		public void InterceptorParameters_MustMatchTheOneIntercepted()
		{
			_interceptor.Method(p => p.MethodWithParam1(Any.Value<string>()))
				.PrecededBy((string param1, string param2) => _executionList.Add(string.Format("MethodWithParam1 {0} Before", param1)));
		}

		[TestMethod]
		[ExpectedException(typeof(InvocationArgumentsMismatchException))]
		public void InterceptorParameters_MustMatchTypeOfThOneIntercepted()
		{
			_interceptor.Method(p => p.MethodWithParam1(Any.Value<string>()))
				.PrecededBy((double param1) => _executionList.Add(string.Format("MethodWithParam1 {0} Before", param1)));
		}

		string BBB
		{
			get { return "BBB"; }
		}

		string GetBBB()
		{
			return "BBB";
		}

		public class TestOtherClass
		{
			public string BBB
			{
				get { return "BBB"; }
			}
		}

		public interface ISample
		{
			void VoidMethod1();
			void VoidMethod2();
			
			string Function1();
			
			void MethodWithParam1(string param1);
			void MethodWithParam2(string param1, int param2);

			int Property1
			{
				get;
				set;
			}
		}

		public class SampleClass : ISample
		{
			readonly List<string> _executionList;
			public SampleClass(List<string> executionList)
			{
				_executionList = executionList;
			}

			public void VoidMethod1()
			{
				_executionList.Add("VoidMethod1");
			}

			public void VoidMethod2()
			{
				_executionList.Add("VoidMethod2");
			}

			public string Function1()
			{
				_executionList.Add("Function1");
				return "VALUE1";
			}

			public void MethodWithParam1(string param1)
			{
				_executionList.Add("MethodWithParam1");
			}

			public void MethodWithParam2(string param1, int param2)
			{
				_executionList.Add("MethodWithParam2");
			}

			int _property1;
			public int Property1
			{
				get
				{
					_executionList.Add("get_Property1");
					return _property1;
				}
				set
				{
					_executionList.Add("set_Property1");
					_property1 = value;
				}
			}
		}
	}
}
