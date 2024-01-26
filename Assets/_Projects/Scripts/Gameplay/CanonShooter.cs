using JvLib.Pooling.Data.Objects;
using JvLib.Pooling.Objects;
using JvLib.Services;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrajectoryPredictor))]
public class CanonShooter : MonoBehaviour
{
    [SerializeField] private PooledObjectConfig _AmmoConfig;

    [SerializeField] private InputActionReference _Fire;
    [SerializeField] private InputActionReference _Move;

    public Transform _ShootPoint;

    [SerializeField, Range(0.0f, 50.0f)]
    private float _ShootForce;

    private TrajectoryPredictor _Predictor;
    private ObjectPool _AmmoPool;

    private void Update()
    {
        Predict();
    }

    private void Predict()
    {
        _ShootForce = AmmoProperties.Instance.StartSpeed;
        _Predictor.PredictTrajectory(AmmoProperties.Instance);
    }

    private void OnEnable()
    {
        _Predictor = GetComponent<TrajectoryPredictor>();

        _Fire.action.Enable();
        _Fire.action.performed += ShootObject;
    }

    private void ShootObject(InputAction.CallbackContext context)
    {
        _AmmoPool = Svc.ObjectPools.GetPool(_AmmoConfig);
        if (_ShootPoint != null)
        {
            GameObject shootObj = _AmmoPool.Activate(_ShootPoint.position, _ShootPoint.rotation);
            Rigidbody shootRb = shootObj.GetComponent<Rigidbody>();
            shootRb.AddForce(_ShootPoint.forward * _ShootForce, ForceMode.Impulse);
        }
        if(this != null) 
        {
            TurnOffPlayer();
            Invoke(nameof(TurnOnPlayer), .7f);
        }

    }

    private void TurnOffPlayer()
    {
        _Move.action.Disable();
        _Fire.action.Disable();
    }
    private void TurnOnPlayer()
    {
        _Move.action.Enable();
        _Fire.action.Enable();
    }
}
