using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RoboticSpider
{
	public class MapObj : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{

		[SerializeField] private RectTransform _mapHolderPanel;
		public RectTransform mapHolderPanel => _mapHolderPanel;
		

		public UnityEvent pointerEnterOnMapEvent;
		public UnityEvent pointerExitFromMapEvent;

		public void OnPointerEnter(PointerEventData eventData)
		{
			pointerEnterOnMapEvent?.Invoke();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			pointerExitFromMapEvent?.Invoke();
		}
	}

}