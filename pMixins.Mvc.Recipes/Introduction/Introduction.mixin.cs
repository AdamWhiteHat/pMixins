//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by CopaceticSoftware.pMixins.CodeGenerator v0.5.1.379
//      for file pMixins\pMixins.Mvc.Recipes\Introduction\Introduction.cs.
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.  
// </auto-generated> 
//------------------------------------------------------------------------------
namespace pMixins.Mvc.Recipes.Introduction
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.5.1.379")]
	public partial class Introduction : pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.pMixins.Mvc.Recipes.Introduction.HelloWorld.IHelloWorldRequirements, global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.Introduction.HelloWorld>
	{
		private sealed class __Mixins
		{
			public static global::System.Object ____Lock = new global::System.Object ();
			public __pMixinAutoGenerated.pMixins_Mvc_Recipes_Introduction_HelloWorld.HelloWorldMasterWrapper pMixins_Mvc_Recipes_Introduction_HelloWorld;
			public __Mixins (Introduction target)
			{
				pMixins_Mvc_Recipes_Introduction_HelloWorld = global::CopaceticSoftware.pMixins.Infrastructure.MixinActivatorFactory.GetCurrentActivator ().CreateInstance<__pMixinAutoGenerated.pMixins_Mvc_Recipes_Introduction_HelloWorld.HelloWorldMasterWrapper> ((pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.pMixins.Mvc.Recipes.Introduction.HelloWorld.IHelloWorldRequirements)target);
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.Introduction.Introduction target)
			{
				pMixins_Mvc_Recipes_Introduction_HelloWorld.__ActivateMixinDependencies (target);
			}
		}
		private sealed class __pMixinAutoGenerated
		{
			public sealed class pMixins_Mvc_Recipes_Introduction_HelloWorld
			{
				public class HelloWorldMasterWrapper : global::CopaceticSoftware.pMixins.Infrastructure.MasterWrapperBase
				{
					public HelloWorldMasterWrapper (pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.pMixins.Mvc.Recipes.Introduction.HelloWorld.IHelloWorldRequirements target)
					{
						_mixinInstance = base.TryActivateMixin<pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.pMixins.Mvc.Recipes.Introduction.HelloWorld.HelloWorldAbstractWrapper> (target);
						base.Initialize (target, _mixinInstance, new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.IMixinInterceptor> {

						});
					}
					public global::pMixins.Mvc.Recipes.Introduction.HelloWorld _mixinInstance;
					[global::System.Diagnostics.DebuggerStepThrough]
					public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.Introduction.Introduction target)
					{
					}
					[global::System.Diagnostics.DebuggerStepThrough]
					internal global::System.String SayHello ()
					{
						return base.ExecuteMethod ("SayHello", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => _mixinInstance.SayHello ());
					}
				}
			}
		}
		private __Mixins ____mixins;
		private __Mixins __mixins {
			get {
				if (null == ____mixins) {
					lock (__Mixins.____Lock) {
						if (null == ____mixins) {
							____mixins = new __Mixins (this);
							____mixins.__ActivateMixinDependencies (this);
						}
					}
				}
				return ____mixins;
			}
		}
		global::pMixins.Mvc.Recipes.Introduction.HelloWorld global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.Introduction.HelloWorld>.MixinInstance {
			get {
				return __mixins.pMixins_Mvc_Recipes_Introduction_HelloWorld._mixinInstance;
			}
		}
		[global::System.Diagnostics.DebuggerStepThrough]
		public static implicit operator global::pMixins.Mvc.Recipes.Introduction.HelloWorld (Introduction target) {
			return target.__mixins.pMixins_Mvc_Recipes_Introduction_HelloWorld._mixinInstance;
		}
		[global::System.Diagnostics.DebuggerStepThrough]
		public global::System.String SayHello ()
		{
			return __mixins.pMixins_Mvc_Recipes_Introduction_HelloWorld.SayHello ();
		}
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.__Shared
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.5.1.379")]
	public interface ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.pMixins.Mvc.Recipes.Introduction.HelloWorld
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.5.1.379")]
	public interface IHelloWorldRequirements : global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.__Shared.ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.pMixins.Mvc.Recipes.Introduction.HelloWorld
{
	public abstract class HelloWorldProtectedMembersWrapper : global::pMixins.Mvc.Recipes.Introduction.HelloWorld
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.pMixins.Mvc.Recipes.Introduction.HelloWorld
{
	public class HelloWorldAbstractWrapper : HelloWorldProtectedMembersWrapper
	{
		private readonly global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.pMixins.Mvc.Recipes.Introduction.HelloWorld.IHelloWorldRequirements _target;
		public HelloWorldAbstractWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.Introduction.Introduction.pMixins.Mvc.Recipes.Introduction.HelloWorld.IHelloWorldRequirements target) : base ()
		{
			_target = target;
		}
	}
}