//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by CopaceticSoftware.pMixins.CodeGenerator v0.5.1.379
//      for file pMixins\pMixins.Mvc.Recipes\VirtualMethodOverride\VirtualMethodOverride.cs.
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.  
// </auto-generated> 
//------------------------------------------------------------------------------
namespace pMixins.Mvc.Recipes.VirtualMethodOverride
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.5.1.379")]
	public partial class VirtualMethodOverride : pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements, global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin>
	{
		private sealed class __Mixins
		{
			public static global::System.Object ____Lock = new global::System.Object ();
			public __pMixinAutoGenerated.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin.VirtualMemberMixinMasterWrapper pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin;
			public __Mixins (VirtualMethodOverride target)
			{
				pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin = global::CopaceticSoftware.pMixins.Infrastructure.MixinActivatorFactory.GetCurrentActivator ().CreateInstance<__pMixinAutoGenerated.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin.VirtualMemberMixinMasterWrapper> ((pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements)target);
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride target)
			{
				pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin.__ActivateMixinDependencies (target);
			}
		}
		private sealed class __pMixinAutoGenerated
		{
			public sealed class pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin
			{
				public class VirtualMemberMixinMasterWrapper : global::CopaceticSoftware.pMixins.Infrastructure.MasterWrapperBase
				{
					public VirtualMemberMixinMasterWrapper (pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements target)
					{
						_mixinInstance = base.TryActivateMixin<pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.VirtualMemberMixinAbstractWrapper> (target);
						GetNameFunc = () => _mixinInstance.GetName ();
						base.Initialize (target, _mixinInstance, new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.IMixinInterceptor> {

						});
					}
					public global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin _mixinInstance;
					public global::System.Func<global::System.String> GetNameFunc {
						get;
						set;
					}
					[global::System.Diagnostics.DebuggerStepThrough]
					public void __ActivateMixinDependencies (pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride target)
					{
					}
					[global::System.Diagnostics.DebuggerStepThrough]
					internal global::System.String GetName ()
					{
						return base.ExecuteMethod ("GetName", new global::System.Collections.Generic.List<global::CopaceticSoftware.pMixins.Interceptors.Parameter> {

						}, () => this.GetNameFunc ());
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
		global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin global::CopaceticSoftware.pMixins.ConversionOperators.IContainMixin<global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin>.MixinInstance {
			get {
				return __mixins.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin._mixinInstance;
			}
		}
		[global::System.Diagnostics.DebuggerStepThrough]
		public static implicit operator global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin (VirtualMethodOverride target) {
			return target.__mixins.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin._mixinInstance;
		}
		[global::System.Diagnostics.DebuggerStepThrough]
		public virtual global::System.String GetName ()
		{
			return __mixins.pMixins_Mvc_Recipes_VirtualMethodOverride_VirtualMemberMixin.GetName ();
		}
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.__Shared
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.5.1.379")]
	public interface ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute ("pMixin", "0.5.1.379")]
	public interface IVirtualMemberMixinRequirements : global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.__Shared.ISharedRequirements
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin
{
	public abstract class VirtualMemberMixinProtectedMembersWrapper : global::pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin
	{
	}
}
namespace pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin
{
	public class VirtualMemberMixinAbstractWrapper : VirtualMemberMixinProtectedMembersWrapper
	{
		private readonly global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements _target;
		public VirtualMemberMixinAbstractWrapper (global::pMixins.AutoGenerated.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMethodOverride.pMixins.Mvc.Recipes.VirtualMethodOverride.VirtualMemberMixin.IVirtualMemberMixinRequirements target) : base ()
		{
			_target = target;
		}
	}
}