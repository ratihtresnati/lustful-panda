// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class PopupWindow : MonoBehaviour
// {
//     public TMP_Text popupText;
//     private GameObject window;

//     private Queue<string> popupQueue;
//     private bool isActive;
//     private Coroutine queueChecker;

//     public void AddToQueue()
//     {
//         popupQueue.Enqueue(text);
//         if()
//     }

//     private void ShowPopup(string text)
//     {
//         isActive = true;
//         window.SetActive(true);
//         popupText.text = text;
//     }

//     private IEnumerator CheckQueue()
//     {
//         do{
//             ShowPopup(popupQueue.Dequeue());
//             do{
//                 yield return null;
//             }while(Debug.Log("haii"));
//         }while(popupQueue.Count > 0);
//         window.SetActive(false);
//         queueChecker = null;
//     }
// }
