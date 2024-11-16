// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerQuest : MonoBehaviour
// {
//     private QuestManager _questManager;
//     private QuestUI _questUI;
//     private List<DoorQuest> _doorQuest;
//     private InteractQuest _interactQuest;
//     private ItemQuest _itemQuest;
//     private bool _doorOpen;
//     private bool _itemCollected;
//     private bool _interacted;
    

//     public bool hintNext;

//     private void Awake()
//     {
//         _questManager = FindObjectOfType<QuestManager>();
//         _questUI = FindObjectOfType<QuestUI>();
//         _doorQuest = new List<DoorQuest>(FindObjectsOfType<DoorQuest>());
//         _interactQuest = FindObjectOfType<InteractQuest>();
//         _itemQuest = FindObjectOfType<ItemQuest>();
//     }

    
//     private void Update()
//     {
//         // Debug.Log(_doorQuest.QuestComplete());
//         hintNext = false;
//         TrackingQuest();
//     }

//     private void TrackingQuest()
//     {
//         var id = _questManager.IdQuest();
//         var index = _questManager.CurrentIndex();

//         if (id == 0)
//         {

//             foreach (var doorQuest in _doorQuest)
//             {
//                 if (doorQuest.QuestComplete() && doorQuest.GetQuestIndex() == index)
//                 {
//                     Debug.Log($"Door Quest at index {index} complete, moving to next quest.");
//                     _questUI.NextQuest();
//                     break; 
//                 }
//             }
            
//             if(index == 1 && _interactQuest.Interact() == true)
//             {
//                 _questUI.NextQuest();
//             }
//             else if (index == 2 && _itemQuest.QuestComplete() == true)
//             {
//                 _questUI.NextQuest();
//             }
//             else if (index == 3 && _interactQuest.GiveItem())
//             {
//                 _questUI.NextQuest();
//             }
//         }
//     }
// }

