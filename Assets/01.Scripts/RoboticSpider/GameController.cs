using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace RoboticSpider
{
	public class GameController : MonoBehaviour
	{
		[Header("UI")]
		[SerializeField] private MapObj mapObj;
		private RectTransform mapHolderRect;
		[SerializeField] private GameObject panel_InputGuideBtn;
		[SerializeField] private GameObject panel_InputGuide;


		private Player player;

		private void OnEnable()
		{
			mapHolderRect = mapObj.mapHolderPanel;

			mapObj.pointerEnterOnMapEvent.AddListener(() =>
			{
				mapHolderRect.DOSizeDelta(new Vector2(900,900), 0.5f).SetEase(Ease.OutBounce);
				//mapHolderRect.DOScaleY(3, 0.5f).SetEase(Ease.OutBounce);
			});

			mapObj.pointerExitFromMapEvent.AddListener(() =>
			{
				mapHolderRect.DOSizeDelta(new Vector2(300, 300), 0.5f).SetEase(Ease.OutBounce);
				//mapHolderRect.DOScaleX(1, 0.5f).SetEase(Ease.OutBounce);
				//mapHolderRect.DOScaleY(1, 0.5f).SetEase(Ease.OutBounce);
			});
		}

		private void OnDisable()
		{
			mapObj.pointerEnterOnMapEvent.RemoveAllListeners();
			mapObj.pointerExitFromMapEvent.RemoveAllListeners();
		}


		void Start()
		{
			player = FindObjectOfType<Player>();

			InitSetting();
		}

		private void InitSetting()
		{
			CloseInputGuideButtonEvent();
		}

		public void OnResetButtonClickEvent()
		{
			player.transform.position = Vector3.zero;
		}

		public void InputGuideButtonEvent()
		{
			panel_InputGuideBtn.SetActive(false);
			panel_InputGuide.SetActive(true);
		}

		public void CloseInputGuideButtonEvent()
		{
			panel_InputGuideBtn.SetActive(true);
			panel_InputGuide.SetActive(false);
		}


	}

}