using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MyCustomSystem.Events;
using MyCustomSystem.Events.EntityBehaviour;

namespace MyCustomSystem.EntityBehaviour.GenericBehaviour
{
	[RequireComponent(typeof(Collider))]
	public class ColliderBehaviour : AbsCustomBehaviour
	{
		[Space][SerializeField, ReadOnly] Collider _thisCollider;

		//public bool checkLayerFlag = false;
		//[ShowIf("checkLayerFlag")] public LayerMask collisionLayer;
		[Space] public bool checkTagFlag = false;
		[ShowIf("checkTagFlag")] public string tagToCheck = "";

		/*[HideIf("isTrigger")] public Collision_UnityEvent onColliderEnter;
		[HideIf("isTrigger")] public Collision_UnityEvent onColliderStay;
		[HideIf("isTrigger")] public Collision_UnityEvent onColliderExit;
		[ShowIf("isTrigger")] public Collider_UnityEvent onTriggerEnter;
		[ShowIf("isTrigger")] public Collider_UnityEvent onTriggerStay;
		[ShowIf("isTrigger")] public Collider_UnityEvent onTriggerExit;*/
		[HideIf("isTrigger"), FoldoutGroup("Events")] public GenericEntity_UnityEvent onColliderEnter;
		[HideIf("isTrigger"), FoldoutGroup("Events")] public GenericEntity_UnityEvent onColliderStay;
		[HideIf("isTrigger"), FoldoutGroup("Events")] public GenericEntity_UnityEvent onColliderExit;
		[ShowIf("isTrigger"), FoldoutGroup("Events")] public GenericEntity_UnityEvent onTriggerEnter;
		[ShowIf("isTrigger"), FoldoutGroup("Events")] public GenericEntity_UnityEvent onTriggerStay;
		[ShowIf("isTrigger"), FoldoutGroup("Events")] public GenericEntity_UnityEvent onTriggerExit;

		Collider thisCollider 
		{
			get 
			{
				if(_thisCollider == null)
				{
					_thisCollider = GetComponent<Collider>();
				}
				return _thisCollider;
			}
		}

		bool isTrigger 
		{
			get
			{
				return thisCollider != null ? thisCollider.isTrigger : false;
			}
		}



		protected override void CustomSetup()
		{
			_thisCollider = GetComponent<Collider>();

			base.CustomSetup();
		}



		private bool CheckCollisionData(Collider other)
		{
			return checkTagFlag ? other.gameObject.tag == tagToCheck : true;
		}
		private bool CheckCollisionData(Collision collision)
		{
			return CheckCollisionData(collision.collider);
		}


		#region COLLIDER API
		public void OnCollisionEnter(Collision collision)
		{
			if (CheckCollisionData(collision))
			{
				ColliderBehaviour colliderBh = collision.collider.GetComponent<ColliderBehaviour>();
				if (colliderBh != null)
				{
					AbsGenericEntity genericEntity = colliderBh.entity;
					if (colliderBh.entity != null)
					{
						onColliderEnter.Invoke(colliderBh.entity);
					}
				}
			}
		}

		public void OnCollisionStay(Collision collision)
		{
			if (CheckCollisionData(collision))
			{
				ColliderBehaviour colliderBh = collision.collider.GetComponent<ColliderBehaviour>();
				if (colliderBh != null)
				{
					AbsGenericEntity genericEntity = colliderBh.entity;
					if (colliderBh.entity != null)
					{
						onColliderStay.Invoke(colliderBh.entity);
					}
				}
			}
		}

		public void OnCollisionExit(Collision collision)
		{
			if (CheckCollisionData(collision))
			{
				ColliderBehaviour colliderBh = collision.collider.GetComponent<ColliderBehaviour>();
				if (colliderBh != null)
				{
					AbsGenericEntity genericEntity = colliderBh.entity;
					if (colliderBh.entity != null)
					{
						onColliderExit.Invoke(colliderBh.entity);
					}
				}
			}
		}



		public void OnTriggerEnter(Collider other)
		{
			if (CheckCollisionData(other))
			{
				ColliderBehaviour colliderBh = other.GetComponent<ColliderBehaviour>();
				if (colliderBh != null)
				{
					AbsGenericEntity genericEntity = colliderBh.entity;
					if (colliderBh.entity != null)
					{
						onTriggerEnter.Invoke(colliderBh.entity);
					}
				}
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (CheckCollisionData(other))
			{
				ColliderBehaviour colliderBh = other.GetComponent<ColliderBehaviour>();
				if (colliderBh != null)
				{
					AbsGenericEntity genericEntity = colliderBh.entity;
					if (colliderBh.entity != null)
					{
						onTriggerStay.Invoke(colliderBh.entity);
					}
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (CheckCollisionData(other))
			{
				ColliderBehaviour colliderBh = other.GetComponent<ColliderBehaviour>();
				if (colliderBh != null)
				{
					AbsGenericEntity genericEntity = colliderBh.entity;
					if (colliderBh.entity != null)
					{
						onTriggerExit.Invoke(colliderBh.entity);
					}
				}
			}
		}

		#endregion


	}
}