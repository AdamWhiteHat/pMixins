//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by CopaceticSoftware.pMixins.CodeGenerator v0.1.13.376
//      for file pMixins\pMixins.Mvc.Recipes\VirtualMethodOverride\VirtualMethodOverride.cs.
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.  
// </auto-generated> 
//------------------------------------------------------------------------------
namespace pMixins.Mvc.Recipes.VirtualMethodOverride
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
	public partial class VirtualMethodOverride : global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements, global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin>
	{
		[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
		private sealed class __Mixins
		{
			public static global::System.Object ____Lock = new global::System.Object ();
			public readonly __pMixinAutoGenerated.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin.VirtualMemberMixinMasterWrapper pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin;
			public __Mixins (VirtualMethodOverride host)
			{
				pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin = global::CopaceticSoftware.pMixins.Infrastructure.MixinActivatorFactory.GetCurrentActivator ().CreateInstance<__pMixinAutoGenerated.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin.VirtualMemberMixinMasterWrapper> ((global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements)host);
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			public void __ActivateMixinDependencies (VirtualMethodOverride host)
			{
				pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin.__ActivateMixinDependencies (host);
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
			public sealed class pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin
			{
				[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
				public abstract class VirtualMemberMixinProtectedMembersWrapper : global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin
				{
					public VirtualMemberMixinProtectedMembersWrapper () : base ()
					{
					}
				}
				[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
				public class VirtualMemberMixinAbstractWrapper : VirtualMemberMixinProtectedMembersWrapper
				{
					private readonly global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements _target;
					public VirtualMemberMixinAbstractWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements target)
					{
						_target = target;
					}
				}
				[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
				public sealed class VirtualMemberMixinMasterWrapper : global::CopaceticSoftware.pMixins.Infrastructure.MasterWrapperBase
				{
					public global::System.Func<string> GetNameFunc {
						get;
						set;
					}
					public readonly global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin _mixinInstance;
					public VirtualMemberMixinMasterWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements mixinInstance)
					{
						_mixinInstance = base.TryActivateMixin<__pMixinAutoGenerated.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin.VirtualMemberMixinAbstractWrapper> (mixinInstance);
						GetNameFunc = () => base.ExecuteMethod ("GetName", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => _mixinInstance.GetName ());
						base.Initialize (mixinInstance, _mixinInstance, new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.IMixinInterceptor> {

						});
					}
					[global::System.Diagnostics.DebuggerStepThrough]
					public void __ActivateMixinDependencies (VirtualMethodOverride target)
					{
					}
					[global::System.Diagnostics.DebuggerStepThrough]
					internal global::System.String GetName ()
					{
						return GetNameFunc ();
					}
				}
			}
		}
		[global::System.Diagnostics.DebuggerStepThrough]
		public virtual global::System.String GetName ()
		{
			return ___mixins.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin.GetName ();
		}
		[global::System.Diagnostics.DebuggerStepThrough]
		public static implicit operator global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin (VirtualMethodOverride target) {
			return target.___mixins.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin._mixinInstance;
		}
		global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin>.MixinInstance {
			get {
				return ___mixins.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin._mixinInstance;
			}
		}
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.1.13.376")]
	public interface IVirtualMemberMixinRequirements
	{
	}
}
