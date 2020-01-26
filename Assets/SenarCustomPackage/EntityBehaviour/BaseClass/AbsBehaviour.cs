namespace MyCustomSystem.EntityBehaviour
{
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using System.Linq;

	/// <summary>
	/// padre di tutti i Behaviour, 
	/// </summary>
	public class AbsBehaviour : SerializedMonoBehaviour
	{
		[ShowIf("hasBeenSetup"), PropertyOrder(int.MinValue), ReadOnly, SerializeField]
		public AbsEntity entity { get; protected set; }
		[HideInEditorMode, PropertyOrder(-999999), ReadOnly, SerializeField]
		public bool hasBeenSetup { get; protected set; }
		
		[HideIf("hasBeenSetup")]
		public bool setupByEntity = true;

		[TabGroup("AbsBehaviour_Events", "Setup"), DisableIf("hasBeenSetup"), Space]
		public UnityEvent onSetup;
		[TabGroup("AbsBehaviour_Events", "Destory"), Space]
		public UnityEvent onDestroy;


		#region OVERRIDABLE
		/// <summary>
		/// funzione chiamata durante OnDestroy,
		/// per implementare comportamente custom durante OnDestroy sovrascrivere questa funzione
		/// </summary>
		protected virtual void CustomOnDestroy() { }
		/// <summary>
		/// funzione chiamata durante Setup,
		/// per implementare comportamente custom durante Setup sovrascrivere questa funzione
		/// </summary>
		protected virtual void CustomSetup() { }

		#endregion

		#region API
		/// <summary>
		/// Esegue il Setup dell'behaviour
		/// per aggiungere comportamenti custom sovrascrivere CustomSetup
		/// </summary>
		/// <param name="entity">entità a cui appartiene il behaviour</param>
		public void Setup(AbsEntity entity)
		{
			if (!hasBeenSetup)
			{
				this.entity = entity;
				hasBeenSetup = true;
				CustomSetup();

				if (onSetup == null)
				{
					onSetup = new UnityEvent();
				}

				onSetup?.Invoke();
			}
		}

		#endregion

		#region INTERNAL
		/// <summary>
		/// funzione chiamata alla distruzione del behaviour
		/// si consiglia di non sovrascriverla ma di utilizzare invece CustomOnDestroy
		/// per garantire il corretto funzionamento del behaviour
		/// </summary>
		private void OnDestroy()
		{
			CustomOnDestroy();
			onDestroy?.Invoke();
			entity?.RemoveBehaviour(this, false);
		}

		#endregion

	}

}