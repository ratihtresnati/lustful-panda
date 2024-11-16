// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DoorQuest : MonoBehaviour
// {
//     private TriggerDoor _triggerDoor;
//     private Outline _outline;

//     [SerializeField] private int questIndex;

//     private void Start()
//     {
//         _outline = GetComponent<Outline>();
//         _triggerDoor = gameObject.GetComponent<TriggerDoor>();
//     }

//     public bool QuestComplete()
//     {
//         if(_outline != null)
//         {
//             _outline.ApplyOutline(false);
//         }

//         return _triggerDoor.IsOpen;
//     }

//     public int GetQuestIndex() 
//     {
//         return questIndex;
//     }
// }
