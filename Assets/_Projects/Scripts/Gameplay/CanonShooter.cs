using JvLib.Pooling.Data.Objects;
using JvLib.Pooling.Objects;
using JvLib.Services;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrajectoryPredictor))]
public class CanonShooter : MonoBehaviour
{
    


    public Transform _ShootPoint;

    private void Update()
    {
        Predict();
    }

    private void Predict()
    {
        
    }



}
