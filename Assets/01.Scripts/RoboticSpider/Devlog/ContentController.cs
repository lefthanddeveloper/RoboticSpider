using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider.DevLog
{
public class ContentController : MonoBehaviour
{
    [SerializeField] private List<DevContent> contents = new List<DevContent>();



    public void ShowContent(int index)
    {
        for(int i=0; i<contents.Count; i++)
        {
            if(index == contents[i].index)
            {
                contents[i].gameObject.SetActive(true);
                contents[i].OpenUp();
            }
            else
            {
                contents[i].gameObject.SetActive(false);
                contents[i].Close();
            }
        }
    }
}

}
