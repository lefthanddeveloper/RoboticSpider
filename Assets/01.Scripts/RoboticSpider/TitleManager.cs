using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace RoboticSpider
{

    public class TitleManager : MonoBehaviour
    {

        [Header("StartButton")]
        public StartBtnObj startBtn;
        private Vector3 startBtnOriginalPos;
        LineRenderer lineRenderer;
        private float lineWidth = 1.0f;
        [SerializeField] private Color enterColor;
        private Color normalColor = Color.white;
        private bool isStartBtnClicked; 

        private void OnEnable() {
            StartButtonEventInit();
            
        }

        private void OnDisable() {
            
        }

        void Start()
        {
            LineInit();

            StartCoroutine(ShowSpider(3.0f));
        }

        IEnumerator ShowSpider(float time)
        {   
            yield return new WaitForSeconds(time);

            startBtn.GetComponent<RectTransform>().DOLocalMoveY(-150.0f, 2.0f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                startBtn.SetReady();
            });
        }

        private void LineInit()
        {
            startBtnOriginalPos = startBtn.GetComponent<RectTransform>().position - Vector3.forward;
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            lineRenderer.alignment = LineAlignment.TransformZ;
            lineRenderer.SetPosition(0, startBtnOriginalPos);
            lineRenderer.SetPosition(1, startBtnOriginalPos);

        }

        private void StartButtonEventInit()
        {
            startBtn.pointerEnterEvent.AddListener(()=>{
                startBtn.GetComponent<Image>().color = enterColor;
                lineRenderer.material.color = enterColor;
            });

            startBtn.pointerExitEvent.AddListener(()=>{
                startBtn.GetComponent<Image>().color = normalColor;
                lineRenderer.material.color = normalColor;
            });

            startBtn.pointerClickEvent.AddListener(OnStartBtnClick);
        }

        private void StartButtonEventRemove()
        {
            startBtn.pointerEnterEvent.RemoveAllListeners();
            startBtn.pointerExitEvent.RemoveAllListeners();
            startBtn.pointerClickEvent.RemoveAllListeners();
        }

        private void OnStartBtnClick()
        {   
            isStartBtnClicked= true;
            startBtn.transform.DOMoveY(-300.0f, 2.0f).SetEase(Ease.InCubic).OnComplete(()=>{
                ChangeScene();
            });
        }

        private void ChangeScene()
        {
            SceneManager.LoadScene("Game");
        }

        // Update is called once per frame
        void Update()
        {
            if(!isStartBtnClicked)
            {
                LineUpdate();
            }
        }

        private void LineUpdate()
        {
            lineRenderer.SetPosition(0, (startBtnOriginalPos));
            lineRenderer.SetPosition(1, startBtn.GetComponent<RectTransform>().position - Vector3.forward);
        }
    }

}