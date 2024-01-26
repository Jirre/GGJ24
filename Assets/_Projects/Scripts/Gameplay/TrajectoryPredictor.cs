using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    private LineRenderer _TrajectoryLine;
    [SerializeField] private Transform _HitMarker;

    // Max number of points the linerenderer can have
    [SerializeField] private int _MaxPoints;
    [SerializeField, Range(0.01f, 0.5f)] private float _Increment = 0.025f;

    [SerializeField, Range(1.05f, 2f)] private float _RayOverlap = 1.1f;

    private CanonShooter _CanonShooter;


    private void Start()
    {
        _CanonShooter = GetComponent<CanonShooter>();

        if(_TrajectoryLine == null)
            _TrajectoryLine = GetComponent<LineRenderer>();
    }

    public void PredictTrajectory(AmmoProperties ammoProperties)
    {
        Vector3 velocity = ammoProperties.Direction * (ammoProperties.StartSpeed / ammoProperties.Mass);
        Vector3 pos = _CanonShooter._ShootPoint.position;
        Vector3 nextPos;
        float overlap;

        UpdateLineRenderer(_MaxPoints, 0, pos);

        for (int i = 0; i < _MaxPoints; i++)
        {
            velocity = CalculateNewVelocity(velocity, ammoProperties.Drag, _Increment);
            nextPos = pos + velocity * _Increment;

            overlap = Vector3.Distance(pos, nextPos) * _RayOverlap;

            if (Physics.Raycast(pos, velocity.normalized, out RaycastHit hit, overlap))
            {

                Debug.DrawRay(nextPos, hit.point);

                Debug.Log("Next pos: " + nextPos);
                Debug.Log("Hit point: " + hit.point);

                if (i - 1 >= 0)
                {
                    UpdateLineRenderer(i, i - 1, hit.point);
                }
                MoveHitMarker(hit);
                break;
            }

            _HitMarker.gameObject.SetActive(false);
            pos = nextPos;
            UpdateLineRenderer(_MaxPoints, i, pos);
        }
    }

    private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
    {
        velocity += Physics.gravity * increment;
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;
    }

    private void UpdateLineRenderer(int count, int point, Vector3 pos)
    {
        if (point >= 0 && point < count)
        {
            _TrajectoryLine.positionCount = count;
            _TrajectoryLine.SetPosition(point, pos);
        }
    }

    private void MoveHitMarker(RaycastHit hit)
    {
        _HitMarker.gameObject.SetActive(true);

        float offset = 0.025f;
        _HitMarker.position = hit.point + hit.normal * offset;
        _HitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
    }

    public void SetTrajectoryVisible(bool visible)
    {
        _TrajectoryLine.enabled = visible;
        _HitMarker.gameObject.SetActive(visible);
    }
}
