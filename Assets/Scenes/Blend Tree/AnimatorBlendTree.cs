using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBlendTree : MonoBehaviour
{

    [SerializeField] private CharacterAnimatorControllerStateLama _characterAnimator;
    [SerializeField] private PlayerController _playerController;
    private float _animHorizontal;
    private float _animVertical;
    [SerializeField] private float _animeSmoothSpeed = 2;

    private void Start()
    {
        _characterAnimator = GetComponent<CharacterAnimatorControllerStateLama>();
        _playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        AnimateWalkRun(new Vector3(_playerController.GetInputX(), _playerController.GetInputY(), 0));

        // Jump
        if (_playerController.IsJump)
        {
            _characterAnimator.Jump();
        }
        else
        {
            _characterAnimator.Land();
        }

        // Roll
        if (_playerController.IsRooling)
        {
            _characterAnimator.Roll();
        }
        else
        {
            _characterAnimator.StopRoll();
        }

        // Rest
        if (InputManager.instance.RestInput)
        {
            _characterAnimator.Rest();
        }
        else
        {
            _characterAnimator.UpRest();
        }

        // Sit
        if (InputManager.instance.SitInput)
        {
            _characterAnimator.Sit();
        }
        else
        {
            _characterAnimator.UpSit();
        }
    }
    private void AnimateWalkRun(Vector3 input)
    {
        float multiplier = _playerController.IsRun ? 3 : 2f;
        float targetHorizontal = input.x * multiplier;
        float targetVertical = input.y * multiplier;

        _animHorizontal = Mathf.Lerp(_animHorizontal, targetHorizontal, Time.deltaTime * _animeSmoothSpeed);
        _animVertical = Mathf.Lerp(_animVertical, targetVertical, Time.deltaTime * _animeSmoothSpeed);

        _characterAnimator.WalkSpeed(_animHorizontal, _animVertical);




        // ---- hitung arah belok ----
        Vector3 currentForward = transform.forward;
        Quaternion toRotation = Quaternion.LookRotation(input, Vector3.up);
        Vector3 newForward = toRotation * Vector3.forward;
        Vector3 cross = Vector3.Cross(currentForward, newForward);

        // hitung sudut belok relatif terhadap arah hadap
        float angle = Vector3.SignedAngle(transform.forward, input, Vector3.up);

        // normalisasi jadi -1 (kiri) sampai 1 (kanan)
        float turn = 0f;
        if (angle > 5f) turn = 1f;        // kanan
        else if (angle < -5f) turn = -1f; // kiri

        Debug.Log($"CrossY: {cross.y} | Turn: {turn}");


        _characterAnimator.Turn(turn * 10f);


        // float turn = 0f;
        // if (cross.y > 0.2f) turn = 1f;       // kanan
        // else if (cross.y < -0.2f) turn = -1f; // kiri

        // Debug.Log(cross.y);
        
        // _characterAnimator.Turn(turn);
    }

}
