using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CannonState : short
{
    IDLE = 0, 
    MOVING = 1,
    CHARGING = 2,
    FIRE = 3
}

public class CannonController : MonoBehaviour
{

    [SerializeField]
    private float _rotateSpeed = 5f, _maxFirePower = 800f, _chargingSpeed = 300f;

    [SerializeField] private CannonState _state = CannonState.IDLE;

    //각종이벤트 여기에

    private Transform _barrelTrm;
    private Transform _firePosTrm;

    private float _currentFirePower = 0f;
    private float _currentRotate = 0f;

    [SerializeField]
    private CannonBall _ballPrefab;

    private void Awake()
    {
        _barrelTrm = transform.Find("Barrel");
        _firePosTrm = transform.Find("Barrel/FirePos");
    }

    private void Update()
    {
        HandleMove();
        HandleFire();
    }

    private void HandleFire()
    {
        if (Input.GetButtonDown("Jump") && (int)_state < 2)
        {
            _state = CannonState.CHARGING;
            _currentFirePower = 0;
        }
        if (Input.GetButton("Jump") && _state == CannonState.CHARGING)
        {
            _currentFirePower += _chargingSpeed * Time.deltaTime;
            _currentFirePower = Mathf.Clamp(_currentFirePower, 0f, _maxFirePower);
            
        }
        if (Input.GetButtonUp("Jump") && _state == CannonState.CHARGING)
        {
            ReadyToFire();
        }
    }

    private void ReadyToFire()
    {
        _state = CannonState.FIRE;

        //여기서 카메라 옮겨오고 딜레이
        FireCannon();   
    }
    private void FireCannon()
    {
        CannonBall ball = Instantiate(_ballPrefab, _firePosTrm.position, Quaternion.identity) as CannonBall;
        ball.Fire(_firePosTrm.right * _currentFirePower);
        

        ball.OnCompleteExplosion += () =>
        {
            _state = CannonState.IDLE;
        };
    }

    private void HandleMove()
    {
   
        if(_state == CannonState.IDLE || _state == CannonState.MOVING)
        {
            float y = Input.GetAxisRaw("Vertical");
            _currentRotate += y * Time.deltaTime * _rotateSpeed;
            _currentRotate = Mathf.Clamp(_currentRotate, 0f, 90f);

            _barrelTrm.transform.rotation = Quaternion.Euler(0, 0, _currentRotate);
            
            if( Mathf.Abs(y) > 0f)
            {
                _state = CannonState.MOVING;
            }
            else
            {
                _state = CannonState.IDLE;
            }
        }
    }
}
