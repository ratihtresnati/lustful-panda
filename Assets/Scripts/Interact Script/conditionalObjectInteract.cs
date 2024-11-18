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
    [SerializeField] Vector3 _dialogPosition = new Vector3(0.5f, 2.0f, 0f);
    private GameObject _player;
    private bool _isCarryingTheItem = false;
    private Outline _outlineGameObject;
    private Outline _itemRequireOutline;
    private PlayerHoldPosition _playerHoldPosition;
    private ParentPosition _parentPosition;
    [SerializeField] private bool _questDoor = false;
    private Rigidbody _rigidbody;


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
        if (distance <= _interactionRadius && InputManager.instance.InteractInput)
        {
            if (_isCarryingTheItem)
            {
                Interact();
            }
            else
            {
                if(_itemRequireOutline != null)
                {
                    _itemRequireOutline.ApplyOutline(true);
                }   
                
                _dialogAsset.transform.position = _parentPosition.PositionParent().position + _dialogPosition;
                _dialogAsset.SetActive(true);
                QuestManager.instance.NextQuest();
            }
        }
    }

    void Interact()
    {
        switch (_questNum) {
        case 1: //npc panda
            NPCPandaStateController npcPanda = GetComponent<NPCPandaStateController>();
            if (npcPanda != null)
            {
                npcPanda._isComplete = true;
                QuestManager.instance.NextQuest();
                _outlineGameObject.ApplyOutline(false);
            }
            else
            {
                Debug.LogWarning("NPCPandaStateController tidak ditemukan pada objek ini.");
            }

            if(_questDoor == true)
            {
                _rigidbody.isKinematic = false;
            }
        break;
        case 2: //final door
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null){
                boxCollider.enabled = false;
            }
        break;
        }
        Destroy(_taskItem);
        Destroy(_dialogAsset);
    }
}