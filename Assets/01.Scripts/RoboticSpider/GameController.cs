using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class GameController : MonoBehaviour
    {
        private Player player;
        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Player>();
        }

        public void OnResetButtonClickEvent()
        {
            player.transform.position = Vector3.zero;
        }
    }

}