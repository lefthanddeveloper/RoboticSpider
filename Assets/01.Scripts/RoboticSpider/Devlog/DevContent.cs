using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace RoboticSpider.DevLog
{
    public class DevContent : MonoBehaviour
    {
        [SerializeField] private int _index; //0: MOVE
                                            //1: ANIMATION
                                            //2: RENDER TEXTURE
                                            //3: INTERACTION
                                            //4: RAYCAST
        public int index => _index;
                    
        [SerializeField] private VideoPlayer videoPlayer;
        public void OpenUp()
        {
            if(videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.Play();
        }

        public void Close()
        {
            if(videoPlayer.isPlaying) videoPlayer.Stop();
        }
        
    }

}
