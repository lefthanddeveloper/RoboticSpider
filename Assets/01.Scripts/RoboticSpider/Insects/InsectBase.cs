using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class InsectBase : MonoBehaviour
    {
        [SerializeField] protected Animator[] animators;

		Rigidbody rigidbody;
		private void Start()
		{
			rigidbody = GetComponent<Rigidbody>();
		}

		private void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.tag == "Web")
			{
				WebLine webLine = collision.collider.GetComponent<WebLine>();
				OnTouchedWeb(webLine, collision.GetContact(0).point, collision.GetContact(0).normal);
			}
		}

		protected abstract void OnTouchedWeb(WebLine touchedLine, Vector3 contactPoint, Vector3 contactNormal);
	}

}
