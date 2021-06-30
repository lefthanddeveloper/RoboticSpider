using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RoboticSpider.DevLog
{
    public class DevLogMgr : MonoBehaviour
    {
        [SerializeField] private List<FeatureButtonObj> buttonObjs = new List<FeatureButtonObj>();
        [SerializeField] private ContentController contentController;
        [SerializeField] private Color activeColor;
        private Color inactiveColor = Color.white;

        [SerializeField] private Button backToGameBtn; 

        private void Start()
        {
            ButtonInit();
        }

        private void ButtonInit()
        {
            backToGameBtn.onClick.AddListener(()=>{
                    SceneManager.LoadScene("Game");
            });


            foreach(var buttonObj in buttonObjs)
            {
                buttonObj.SetColor(activeColor, inactiveColor);

                buttonObj.button.onClick.AddListener(()=>{
                    contentController.ShowContent(buttonObj.buttonIndex);                    
                });

                buttonObj.button.onClick.AddListener(()=>{
                    for(int i=0;i<buttonObjs.Count;i++)
                    {
                        if(buttonObjs[i] == buttonObj)
                        {
                            buttonObjs[i].ChangeImageColor(activeColor);
                        }
                        else
                        {
                            buttonObjs[i].ChangeImageColor(inactiveColor);
                        }
                    }    
                });
            }
        }
    }

}
