//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by CopaceticSoftware.pMixins.CodeGenerator v0.1.13.376
//      for file pMixins\pMixins.Mvc.Recipes\SpecificMixinConstructor\SpecificMixinConstructor.cs.
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.  
// </auto-generated> 
//------------------------------------------------------------------------------
namespace pMixins.Mvc.Recipes.SpecificMixinConstructor
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
	public partial class SpecificMixinConstructor : global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.SpecificMixinConstructor.SpecificMixinConstructor.pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin.IMixinRequirements, global::CopaceticSoftware.pMixins.Infrastructure.IMixinConstructorRequirement<global::pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin>, global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
		private sealed class __Mixins
		{
			public static global::System.Object ____Lock = new global::System.Object ();
			public readonly __pMixinAutoGenerated.pMixins_Mvc_Recipes_SpecificMixinConstructor_Mixin.MixinMasterWrapper pMixins_Mvc_Recipes_SpecificMixinConstructor_Mixin;
			public __Mixins (SpecificMixinConstructor host)
			{
				pMixins_Mvc_Recipes_SpecificMixinConstructor_Mixin = global::CopaceticSoftware.pMixins.Infrastructure.MixinActivatorFactory.GetCurrentActivator ().CreateInstance<__pMixinAutoGenerated.pMixins_Mvc_Recipes_SpecificMixinConstructor_Mixin.MixinMasterWrapper> ((global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.SpecificMixinConstructor.SpecificMixinConstructor.pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin.IMixinRequirements)host);
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			public void __ActivateMixinDependencies (SpecificMixinConstructor host)
			{
				pMixins_Mvc_Recipes_SpecificMixinConstructor_Mixin.__ActivateMixinDependencies (host);
			}
		}
		private __Mixins _____mixins;
		private __Mixins ___mixins {
			get {
				if (null == _____mixins) {
					lock (__Mixins.____Lock) {
						if (null == _____mixins) {
							_____mixins = new __Mixins (this);
							_____mixins.__ActivateMixinDependencies (this);
						}
					}
				}
				return _____mixins;
			}
		}
		[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
		private sealed class __pMixinAutoGenerated
		{
			[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
			public sealed class pMixins_Mvc_Recipes_SpecificMixinConstructor_Mixin
			{
				[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
				public abstract class MixinProtectedMembersWrapper : global::pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin
				{
					public MixinProtectedMembersWrapper (global::System.String constructorName) : base (constructorName)
					{
					}
				}
				[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
				public class MixinAbstractWrapper : MixinProtectedMembersWrapper
				{
					private readonly global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.SpecificMixinConstructor.SpecificMixinConstructor.pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin.IMixinRequirements _target;
					public MixinAbstractWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.SpecificMixinConstructor.SpecificMixinConstructor.pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin.IMixinRequirements target, global::System.String constructorName) : base (constructorName)
					{
						_target = target;
					}
				}
				[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
				public sealed class MixinMasterWrapper : global::CopaceticSoftware.pMixins.Infrastructure.MasterWrapperBase
				{
					public readonly global::pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin _mixinInstance;
					public MixinMasterWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.SpecificMixinConstructor.SpecificMixinConstructor.pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin.IMixinRequirements mixinInstance)
					{
						_mixinInstance = ((global::CopaceticSoftware.pMixins.Infrastructure.IMixinConstructorRequirement<global::pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin>)mixinInstance).InitializeMixin ();
						base.Initialize (mixinInstance, _mixinInstance, new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.IMixinInterceptor> {

						});
					}
					[global::System.Diagnostics.DebuggerStepThrough]
					public void __ActivateMixinDependencies (SpecificMixinConstructor target)
					{
					}
					internal global::System.String ConstructorUsed {
						get {
							return base.ExecutePropertyGet ("ConstructorUsed", () => _mixinInstance.ConstructorUsed);
						}
					}
				}
			}
		}
		public global::System.String ConstructorUsed {
			get {
				return ___mixins.pMixins_Mvc_Recipes_SpecificMixinConstructor_Mixin.ConstructorUsed;
			}
		}
		[global::System.Diagnostics.DebuggerStepThrough]
		public static implicit operator global::pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin (SpecificMixinConstructor target) {
			return target.___mixins.pMixins_Mvc_Recipes_SpecificMixinConstructor_Mixin._mixinInstance;
		}
		global::pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin>.MixinInstance {
			get {
				return ___mixins.pMixins_Mvc_Recipes_SpecificMixinConstructor_Mixin._mixinInstance;
			}
		}
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.SpecificMixinConstructor.SpecificMixinConstructor.pMixins.Mvc.Recipes.SpecificMixinConstructor.Mixin
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
	public interface IMixinRequirements
	{
	}
}
