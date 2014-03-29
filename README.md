Castle.FluentInterceptor
========================

Moq inspired interceptor

Code example:

	var fluentInterceptor = new FluentInterceptor<Dog>();

	fluentInterceptor.Method(p => p.Eat(Any.Value<string>()))
		.FollowedBy(() =>
			{
				Debug.WriteLine("Eat method intercepted!"); 
			});


FluentInterceptor implements IInterceptor so you can use it as a standard interceptor.


