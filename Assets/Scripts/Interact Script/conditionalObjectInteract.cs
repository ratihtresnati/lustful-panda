using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class conditionalObjectInteract : MonoBehaviour
{
    [SerializeField] private int _questNum = 1;
    [SerializeField] private float _interactionRadius = 3f;
    [SerializeField] private GameObject _taskItem;
    [SerializeField] private GameObject _dialogAsset;
    [SerializeField] Vector3 _dialogPosition = new Vector3(0f, 2.0f, 0f);
    private GameObject _player;
    private bool _isCarryingTheItem = false;
    private Outline _outlineGameObject;
    private Outline _itemRequireOutline;
    private PlayerHoldPosition _playerHoldPosition;
    private ParentPosition _parentPosition;
    [SerializeField] private bool _questDoor = false;
    private Rigidbody _rigidbody;

    public bool IsInteract { get; private set; }
    public bool IsGiveItem { get; private set; }


    void Start()
    {
        _playerHoldPosition = FindObjectOfType<PlayerHoldPosition>();
        _outlineGameObject = gameObject.GetComponent<Outline>();
        _itemRequireOutline = _taskItem.GetComponent<Outline>();
        _parentPosition = gameObject.GetComponent<ParentPosition>();

        _rigidbody = GetComponent<Rigidbody>();

        if (_dialogAsset != null)
        {
            _dialogAsset.SetActive(false);
        }

        // IsInteract = false;
        // IsGiveItem = false;

        _player = GameObject.Find("Panda Bayik");
    }
    void Update()
    {
        // Check bawaan item
        if (_taskItem != null && _taskItem.transform.parent == _playerHoldPosition.PositionParent())
        {
            _isCarryingTheItem = true;
        }
        else
        {
            _isCarryingTheItem = false;
        }
        // Menghitung jarak pemain n objek
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        
            if (distance <= _interactionRadius)
            {
                Interaction();
            }
            else if(_questDoor == true)
            {
                if(_dialogAsset != null)
                {
                    _dialogAsset.SetActive(false);
                }
            }
        
    }

    private void Interaction()
    {
        if(InputManager.instance.InteractInput)
        {
            // if (isCarryingTheItem == true && IsInteract == true)
            if (_isCarryingTheItem)
            {
                Interact();
            }
            else if (_isCarryingTheItem == false)
            {
                ShowDialogAfterInteract();
            }
        }

        if(_questDoor == true)
        {
            if(_dialogAsset != null)
            {
                ShowQuestDoorDialog(); 
            }
            
        }
    }

    void Interact()
    {
        switch (_questNum) {
        case 1: //npc panda
            InteractNPC();
            InteractDoor();
        break;
        case 2: //final door
             BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null){
                boxCollider.enabled = false;
            }
            InteractDoor();
        break;
        case 3: //final door
            Destroy(gameObject);
        break;
        }
        Destroy(_taskItem);
        Destroy(_dialogAsset);
    }

    private void InteractNPC()
    {
        // IsGiveItem = true;
        NPCPandaStateController npcPanda = GetComponent<NPCPandaStateController>();
        if (npcPanda != null)
        {
            npcPanda._isComplete = true;
                // QuestManager.instance.NextQuest();
            _outlineGameObject.ApplyOutline(false);
        }
        else
        {
            Debug.LogWarning("NPCPandaStateController tidak ditemukan pada objek ini.");
        }
    }

    private void InteractDoor()
    {
        if(_questDoor == true)
        {
            _rigidbody.isKinematic = false;
            AudioManager.Instance.Play("BukaPintu");
        }
    }

    private void ShowDialogAfterInteract()
    {
        if(_itemRequireOutline != null)
        {
            _itemRequireOutline.ApplyOutline(true);
        }   

        if(_dialogAsset != null && _questDoor == false)
        {
            _dialogAsset.transform.position = _parentPosition.PositionParent().position + _dialogPosition;
            _dialogAsset.SetActive(true);
        }
                    // QuestManager.instance.NextQuest();

        if(_questDoor == true)
        {
            AudioManager.Instance.Play("BukaKunci");
        }
        else
        {
            AudioManager.Instance.Play("NPCPanda");
        }
    }

    private void ShowQuestDoorDialog()
    {
        var position = _player.GetComponent<ParentPosition>();
        _dialogAsset.transform.position = position.PositionParent().position + _dialogPosition;
        _dialogAsset.transform.SetParent(position.PositionParent());
        _dialogAsset.SetActive(true);
    }
    
    
}