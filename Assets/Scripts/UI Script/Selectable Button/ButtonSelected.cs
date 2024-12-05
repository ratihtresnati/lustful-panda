using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// namespace LustfulPanda.Selectable
// {
    public class ButtonSelected : MonoBehaviour
    {
        public string namePage;
        public List<GameObject> buttonPage = new List<GameObject>();
        private void Awake()//awalnya awake 
        {
            foreach (var button in gameObject.GetComponentsInChildren<SelectableObject>())
            {
                buttonPage.Add(button.gameObject);
            }
        }
        public GameObject FirstButton()
        {
            return buttonPage.Count > 0 ? buttonPage[0] : null;
        }

        public GameObject[] Buttons()
        {
            return buttonPage.ToArray();
        }
    }
// }
