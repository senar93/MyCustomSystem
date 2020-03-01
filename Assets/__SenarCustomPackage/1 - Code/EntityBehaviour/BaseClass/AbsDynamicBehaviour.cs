namespace MyCustomSystem.EntityBehaviour
{
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using System.Linq;

	/// <summary>
	/// behaviour che può essere abilitato o disabilitato tramite il parametro enabled
	/// </summary>
	public class AbsDynamicBehaviour : AbsBehaviour
	{
		[SerializeField, DisableInPlayMode] private bool _enabled = true;

		[TabGroup("AbsBehaviour_Events", "Enable & Disable"), Space]
		public UnityEvent onEnable;
		[TabGroup("AbsBehaviour_Events", "Enable & Disable"), Space]
		public UnityEvent onDisable;

		#region PROPERTY
		public new bool enabled {
			get {
				return _enabled;
			}
			set {
				if (enabled != value)
				{
					_enabled = value;
					switch (_enabled)
					{
						case true:
							CustomOnEnable();
							onEnable?.Invoke();
							break;
						case false:
							CustomOnDisable();
							onDisable?.Invoke();
							break;
					}
				}
			}
		}

		#endregion

		#region OVERRIDABLE
		protected virtual void CustomOnEnable() { }

		protected virtual void CustomOnDisable() { }

		#endregion

	}
}