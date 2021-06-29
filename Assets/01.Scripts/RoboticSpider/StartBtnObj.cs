using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartBtnObj : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent pointerEnterEvent;
    public UnityEvent pointerExitEvent;

    public UnityEvent pointerClickEvent;

    private Image image;

    private bool isReady;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isReady)
        {
            pointerClickEvent?.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isReady)
        {
            pointerEnterEvent?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isReady)
        {
            pointerExitEvent?.Invoke();
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetReady()
    {
        isReady= true;
    }
}
