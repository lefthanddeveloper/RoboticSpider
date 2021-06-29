using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform standardTr;
        public Transform StandardTr => standardTr;
        [SerializeField] private Transform povTr;
        public Transform PovTr => povTr;

    }
}
