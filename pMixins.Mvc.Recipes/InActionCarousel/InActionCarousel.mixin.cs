//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by CopaceticSoftware.pMixins.CodeGenerator v0.6.0.457
//      for file pMixins\pMixins.Mvc.Recipes\InActionCarousel\InActionCarousel.cs.
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.  
// </auto-generated> 
//------------------------------------------------------------------------------
namespace pMixins.Mvc.Recipes.InActionCarousel
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public partial class Penguin : global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim, pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements, global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer>
	{
		private sealed class __Mixins
		{
			public static global::System.Object ____Lock = new global::System.Object ();
			public __pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Swimmer.SwimmerMasterWrapper pMixins_Mvc_Recipes_InActionCarousel_Swimmer;
			public __Mixins (Penguin target)
			{
				pMixins_Mvc_Recipes_InActionCarousel_Swimmer = global::CopaceticSoftware.pMixins.Infrastructure.MixinActivatorFactory.GetCurrentActivator ().CreateInstance<__pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Swimmer.SwimmerMasterWrapper> ((pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements)target);
			}
			[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
			[global::System.Diagnostics.DebuggerStepThrough]
			public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.InActionCarousel.Penguin target)
			{
				pMixins_Mvc_Recipes_InActionCarousel_Swimmer.__ActivateMixinDependencies (target);
			}
		}
		private sealed class __pMixinAutoGenerated
		{
			public sealed class pMixins_Mvc_Recipes_InActionCarousel_Swimmer
			{
				public class SwimmerMasterWrapper : global::CopaceticSoftware.pMixins.Infrastructure.MasterWrapperBase, global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim
				{
					public SwimmerMasterWrapper (pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements target)
					{
						_mixinInstance = base.TryActivateMixin<pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.SwimmerAbstractWrapper> (target);
						base.Initialize (target, _mixinInstance, new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.IMixinInterceptor> {

						});
					}
					public global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer _mixinInstance;
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.InActionCarousel.Penguin target)
					{
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void Swim ()
					{
						base.ExecuteVoidMethod ("Swim", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => _mixinInstance.Swim ());
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					void global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim.Swim ()
					{
						base.ExecuteVoidMethod ("Swim", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => ((global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim)_mixinInstance).Swim ());
					}
				}
			}
		}
		private __Mixins ____mixins;
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
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
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer>.MixinInstance {
			get {
				return __mixins.pMixins_Mvc_Recipes_InActionCarousel_Swimmer._mixinInstance;
			}
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public static implicit operator global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer (Penguin target) {
			return target.__mixins.pMixins_Mvc_Recipes_InActionCarousel_Swimmer._mixinInstance;
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public void Swim ()
		{
			__mixins.pMixins_Mvc_Recipes_InActionCarousel_Swimmer.Swim ();
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		void global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim.Swim ()
		{
			((global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim)__mixins.pMixins_Mvc_Recipes_InActionCarousel_Swimmer).Swim ();
		}
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.__Shared
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public interface ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.pMixins.Mvc.Recipes.InActionCarousel.Swimmer
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public interface ISwimmerRequirements : global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.__Shared.ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.pMixins.Mvc.Recipes.InActionCarousel.Swimmer
{
	public abstract class SwimmerProtectedMembersWrapper : global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.pMixins.Mvc.Recipes.InActionCarousel.Swimmer
{
	public class SwimmerAbstractWrapper : SwimmerProtectedMembersWrapper
	{
		private readonly global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements _target;
		public SwimmerAbstractWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Penguin.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements target) : base ()
		{
			_target = target;
		}
	}
}
namespace pMixins.Mvc.Recipes.InActionCarousel
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public partial class FlyingFish : global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim, global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly, pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements, pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements, global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer>, global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Flyer>
	{
		private sealed class __Mixins
		{
			public static global::System.Object ____Lock = new global::System.Object ();
			public __pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Swimmer.SwimmerMasterWrapper pMixins_Mvc_Recipes_InActionCarousel_Swimmer;
			public __pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Flyer.FlyerMasterWrapper pMixins_Mvc_Recipes_InActionCarousel_Flyer;
			public __Mixins (FlyingFish target)
			{
				pMixins_Mvc_Recipes_InActionCarousel_Swimmer = global::CopaceticSoftware.pMixins.Infrastructure.MixinActivatorFactory.GetCurrentActivator ().CreateInstance<__pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Swimmer.SwimmerMasterWrapper> ((pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements)target);
				pMixins_Mvc_Recipes_InActionCarousel_Flyer = global::CopaceticSoftware.pMixins.Infrastructure.MixinActivatorFactory.GetCurrentActivator ().CreateInstance<__pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Flyer.FlyerMasterWrapper> ((pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements)target);
			}
			[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
			[global::System.Diagnostics.DebuggerStepThrough]
			public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.InActionCarousel.FlyingFish target)
			{
				pMixins_Mvc_Recipes_InActionCarousel_Swimmer.__ActivateMixinDependencies (target);
				pMixins_Mvc_Recipes_InActionCarousel_Flyer.__ActivateMixinDependencies (target);
			}
		}
		private sealed class __pMixinAutoGenerated
		{
			public sealed class pMixins_Mvc_Recipes_InActionCarousel_Swimmer
			{
				public class SwimmerMasterWrapper : global::CopaceticSoftware.pMixins.Infrastructure.MasterWrapperBase, global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim
				{
					public SwimmerMasterWrapper (pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements target)
					{
						_mixinInstance = base.TryActivateMixin<pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.SwimmerAbstractWrapper> (target);
						base.Initialize (target, _mixinInstance, new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.IMixinInterceptor> {

						});
					}
					public global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer _mixinInstance;
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.InActionCarousel.FlyingFish target)
					{
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void Swim ()
					{
						base.ExecuteVoidMethod ("Swim", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => _mixinInstance.Swim ());
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					void global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim.Swim ()
					{
						base.ExecuteVoidMethod ("Swim", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => ((global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim)_mixinInstance).Swim ());
					}
				}
			}
			public sealed class pMixins_Mvc_Recipes_InActionCarousel_Flyer
			{
				public class FlyerMasterWrapper : global::CopaceticSoftware.pMixins.Infrastructure.MasterWrapperBase, global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly
				{
					public FlyerMasterWrapper (pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements target)
					{
						_mixinInstance = base.TryActivateMixin<pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Flyer.FlyerAbstractWrapper> (target);
						base.Initialize (target, _mixinInstance, new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.IMixinInterceptor> {

						});
					}
					public global::pMixins.Mvc.Recipes.InActionCarousel.Flyer _mixinInstance;
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.InActionCarousel.FlyingFish target)
					{
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void Fly ()
					{
						base.ExecuteVoidMethod ("Fly", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => _mixinInstance.Fly ());
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					void global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly.Fly ()
					{
						base.ExecuteVoidMethod ("Fly", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => ((global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly)_mixinInstance).Fly ());
					}
				}
			}
		}
		private __Mixins ____mixins;
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
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
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer>.MixinInstance {
			get {
				return __mixins.pMixins_Mvc_Recipes_InActionCarousel_Swimmer._mixinInstance;
			}
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		global::pMixins.Mvc.Recipes.InActionCarousel.Flyer global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Flyer>.MixinInstance {
			get {
				return __mixins.pMixins_Mvc_Recipes_InActionCarousel_Flyer._mixinInstance;
			}
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public static implicit operator global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer (FlyingFish target) {
			return target.__mixins.pMixins_Mvc_Recipes_InActionCarousel_Swimmer._mixinInstance;
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public static implicit operator global::pMixins.Mvc.Recipes.InActionCarousel.Flyer (FlyingFish target) {
			return target.__mixins.pMixins_Mvc_Recipes_InActionCarousel_Flyer._mixinInstance;
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public void Swim ()
		{
			__mixins.pMixins_Mvc_Recipes_InActionCarousel_Swimmer.Swim ();
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		void global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim.Swim ()
		{
			((global::pMixins.Mvc.Recipes.InActionCarousel.ICanSwim)__mixins.pMixins_Mvc_Recipes_InActionCarousel_Swimmer).Swim ();
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public void Fly ()
		{
			__mixins.pMixins_Mvc_Recipes_InActionCarousel_Flyer.Fly ();
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		void global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly.Fly ()
		{
			((global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly)__mixins.pMixins_Mvc_Recipes_InActionCarousel_Flyer).Fly ();
		}
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.__Shared
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public interface ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Swimmer
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public interface ISwimmerRequirements : global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.__Shared.ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Swimmer
{
	public abstract class SwimmerProtectedMembersWrapper : global::pMixins.Mvc.Recipes.InActionCarousel.Swimmer
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Swimmer
{
	public class SwimmerAbstractWrapper : SwimmerProtectedMembersWrapper
	{
		private readonly global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements _target;
		public SwimmerAbstractWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Swimmer.ISwimmerRequirements target) : base ()
		{
			_target = target;
		}
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Flyer
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public interface IFlyerRequirements : global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.__Shared.ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Flyer
{
	public abstract class FlyerProtectedMembersWrapper : global::pMixins.Mvc.Recipes.InActionCarousel.Flyer
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Flyer
{
	public class FlyerAbstractWrapper : FlyerProtectedMembersWrapper
	{
		private readonly global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements _target;
		public FlyerAbstractWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.FlyingFish.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements target) : base ()
		{
			_target = target;
		}
	}
}
namespace pMixins.Mvc.Recipes.InActionCarousel
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public partial class Pegasus : global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly, global::pMixins.Mvc.Recipes.InActionCarousel.ICanRun, pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements, pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Runner.IRunnerRequirements, global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Flyer>, global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Runner>
	{
		private sealed class __Mixins
		{
			public static global::System.Object ____Lock = new global::System.Object ();
			public __pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Flyer.FlyerMasterWrapper pMixins_Mvc_Recipes_InActionCarousel_Flyer;
			public __pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Runner.RunnerMasterWrapper pMixins_Mvc_Recipes_InActionCarousel_Runner;
			public __Mixins (Pegasus target)
			{
				pMixins_Mvc_Recipes_InActionCarousel_Flyer = global::CopaceticSoftware.pMixins.Infrastructure.MixinActivatorFactory.GetCurrentActivator ().CreateInstance<__pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Flyer.FlyerMasterWrapper> ((pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements)target);
				pMixins_Mvc_Recipes_InActionCarousel_Runner = global::CopaceticSoftware.pMixins.Infrastructure.MixinActivatorFactory.GetCurrentActivator ().CreateInstance<__pMixinAutoGenerated.pMixins_Mvc_Recipes_InActionCarousel_Runner.RunnerMasterWrapper> ((pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Runner.IRunnerRequirements)target);
			}
			[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
			[global::System.Diagnostics.DebuggerStepThrough]
			public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.InActionCarousel.Pegasus target)
			{
				pMixins_Mvc_Recipes_InActionCarousel_Flyer.__ActivateMixinDependencies (target);
				pMixins_Mvc_Recipes_InActionCarousel_Runner.__ActivateMixinDependencies (target);
			}
		}
		private sealed class __pMixinAutoGenerated
		{
			public sealed class pMixins_Mvc_Recipes_InActionCarousel_Flyer
			{
				public class FlyerMasterWrapper : global::CopaceticSoftware.pMixins.Infrastructure.MasterWrapperBase, global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly
				{
					public FlyerMasterWrapper (pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements target)
					{
						_mixinInstance = base.TryActivateMixin<pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Flyer.FlyerAbstractWrapper> (target);
						base.Initialize (target, _mixinInstance, new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.IMixinInterceptor> {

						});
					}
					public global::pMixins.Mvc.Recipes.InActionCarousel.Flyer _mixinInstance;
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.InActionCarousel.Pegasus target)
					{
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void Fly ()
					{
						base.ExecuteVoidMethod ("Fly", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => _mixinInstance.Fly ());
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					void global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly.Fly ()
					{
						base.ExecuteVoidMethod ("Fly", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => ((global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly)_mixinInstance).Fly ());
					}
				}
			}
			public sealed class pMixins_Mvc_Recipes_InActionCarousel_Runner
			{
				public class RunnerMasterWrapper : global::CopaceticSoftware.pMixins.Infrastructure.MasterWrapperBase, global::pMixins.Mvc.Recipes.InActionCarousel.ICanRun
				{
					public RunnerMasterWrapper (pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Runner.IRunnerRequirements target)
					{
						_mixinInstance = base.TryActivateMixin<pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Runner.RunnerAbstractWrapper> (target);
						base.Initialize (target, _mixinInstance, new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.IMixinInterceptor> {

						});
					}
					public global::pMixins.Mvc.Recipes.InActionCarousel.Runner _mixinInstance;
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.InActionCarousel.Pegasus target)
					{
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					public void Run ()
					{
						base.ExecuteVoidMethod ("Run", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => _mixinInstance.Run ());
					}
					[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
					[global::System.Diagnostics.DebuggerStepThrough]
					void global::pMixins.Mvc.Recipes.InActionCarousel.ICanRun.Run ()
					{
						base.ExecuteVoidMethod ("Run", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => ((global::pMixins.Mvc.Recipes.InActionCarousel.ICanRun)_mixinInstance).Run ());
					}
				}
			}
		}
		private __Mixins ____mixins;
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
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
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		global::pMixins.Mvc.Recipes.InActionCarousel.Flyer global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Flyer>.MixinInstance {
			get {
				return __mixins.pMixins_Mvc_Recipes_InActionCarousel_Flyer._mixinInstance;
			}
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		global::pMixins.Mvc.Recipes.InActionCarousel.Runner global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.InActionCarousel.Runner>.MixinInstance {
			get {
				return __mixins.pMixins_Mvc_Recipes_InActionCarousel_Runner._mixinInstance;
			}
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public static implicit operator global::pMixins.Mvc.Recipes.InActionCarousel.Flyer (Pegasus target) {
			return target.__mixins.pMixins_Mvc_Recipes_InActionCarousel_Flyer._mixinInstance;
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public static implicit operator global::pMixins.Mvc.Recipes.InActionCarousel.Runner (Pegasus target) {
			return target.__mixins.pMixins_Mvc_Recipes_InActionCarousel_Runner._mixinInstance;
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public void Fly ()
		{
			__mixins.pMixins_Mvc_Recipes_InActionCarousel_Flyer.Fly ();
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		void global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly.Fly ()
		{
			((global::pMixins.Mvc.Recipes.InActionCarousel.ICanFly)__mixins.pMixins_Mvc_Recipes_InActionCarousel_Flyer).Fly ();
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		public void Run ()
		{
			__mixins.pMixins_Mvc_Recipes_InActionCarousel_Runner.Run ();
		}
		[global::CopaceticSoftware.pMixins.Attributes.MixedInMemberAttribute]
		[global::System.Diagnostics.DebuggerStepThrough]
		void global::pMixins.Mvc.Recipes.InActionCarousel.ICanRun.Run ()
		{
			((global::pMixins.Mvc.Recipes.InActionCarousel.ICanRun)__mixins.pMixins_Mvc_Recipes_InActionCarousel_Runner).Run ();
		}
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.__Shared
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public interface ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Flyer
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public interface IFlyerRequirements : global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.__Shared.ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Flyer
{
	public abstract class FlyerProtectedMembersWrapper : global::pMixins.Mvc.Recipes.InActionCarousel.Flyer
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Flyer
{
	public class FlyerAbstractWrapper : FlyerProtectedMembersWrapper
	{
		private readonly global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements _target;
		public FlyerAbstractWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Flyer.IFlyerRequirements target) : base ()
		{
			_target = target;
		}
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Runner
{
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.6.0.457")]
	public interface IRunnerRequirements : global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.__Shared.ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Runner
{
	public abstract class RunnerProtectedMembersWrapper : global::pMixins.Mvc.Recipes.InActionCarousel.Runner
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Runner
{
	public class RunnerAbstractWrapper : RunnerProtectedMembersWrapper
	{
		private readonly global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Runner.IRunnerRequirements _target;
		public RunnerAbstractWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.InActionCarousel.Pegasus.pMixins.Mvc.Recipes.InActionCarousel.Runner.IRunnerRequirements target) : base ()
		{
			_target = target;
		}
	}
}
