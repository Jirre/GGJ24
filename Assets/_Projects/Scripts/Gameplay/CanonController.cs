using JvLib.Pooling.Data.Objects;
using JvLib.Pooling.Objects;
using JvLib.Services;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanonController : MonoBehaviour
{
    [SerializeField] private Transform _Pivot;
    [SerializeField] private int _PlayerIndex;
    [SerializeField, TabGroup("Movement")] private InputActionAsset _InputMap;
    [SerializeField, TabGroup("Movement")] private InputActionReference _Movement;
    [SerializeField, TabGroup("Movement")] private float _RotationSpeed;
    [SerializeField, TabGroup("Movement")] private Vector2 _ClampX;
    [SerializeField, TabGroup("Movement")] private Vector2 _ClampY;

    [SerializeField, TabGroup("Shooting")] private InputActionReference _Fire;
    [SerializeField, TabGroup("Shooting")] private PooledObjectConfig _AmmoConfig;
    [SerializeField, TabGroup("Shooting")] private Transform _TargetPoint;
    [SerializeField, Range(0.0f, 50.0f), TabGroup("Shooting")]
    private float _ShootForce;
    private float _shootCooldown;

    private LineRenderer _lineRenderer;
    private ObjectPool _AmmoPool;

    private Vector2 _RotationDirection;

    private PlayerInput _PlayerInput;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private IEnumerator Start()
    {
        yield return Svc.Ref.Input.WaitForInstanceReadyAsync();
        yield return new WaitForSeconds(0.2f);
        _PlayerInput = Svc.Input.FindPlayer(_PlayerIndex).Input;

        _PlayerInput.actions[_Movement.name].AddListeners(Movement);
        _PlayerInput.actions[_Fire.name].AddListeners(ShootObject);
    }

    private void Movement(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        _RotationDirection = dir;
    }

    private void Update()
    {
        _shootCooldown -= Time.deltaTime;
        SetRotation();

        _ShootForce = AmmoProperties.Instance.StartSpeed;
        DrawLine();
    }

    private void SetRotation()
    {
        _Pivot.localEulerAngles += new Vector3(_RotationDirection.y, _RotationDirection.x, 0);


        float rotX = Mathf.Clamp((_Pivot.localEulerAngles.x + 360f) % 360, _ClampX.x, _ClampX.y);
        float rotY = Mathf.Clamp((_Pivot.localEulerAngles.y + 360f) % 360, _ClampY.x, _ClampY.y);
        float rotZ = 0;

        _Pivot.localEulerAngles = new Vector3(rotX, rotY, rotZ);

    }

    private void ShootObject(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (_shootCooldown > 0)
            return;

        _AmmoPool = Svc.ObjectPools.GetPool(_AmmoConfig);
        if(_TargetPoint != null)
        {
            GameObject shootObj = _AmmoPool.Activate(_TargetPoint.position, _TargetPoint.rotation);
            Rigidbody shootRb = shootObj.GetComponent<Rigidbody>();
            shootRb.AddForce(_TargetPoint.forward * _ShootForce, ForceMode.Impulse);
        }

    }

    private void DrawLine()
    {
        
        Vector3 startPoint = _TargetPoint.position;
        Vector3 startVelocity = (_ShootForce * _TargetPoint.forward) / AmmoProperties.Instance.Mass;
        _lineRenderer.SetPosition(0, startPoint);
        float j = 0;
        for (int i = 1; i < _lineRenderer.positionCount; i++)
        {
            Vector3 linePos = startPoint + j * startVelocity;
            linePos.y = startPoint.y + startVelocity.y * j + 0.55f * Physics.gravity.y * j * j;
            _lineRenderer.SetPosition(i, linePos);
            j += 0.1f;
        }
    }
}
