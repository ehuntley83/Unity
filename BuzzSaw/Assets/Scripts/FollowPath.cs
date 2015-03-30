using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour 
{
    public enum FollowType
    {
        MoveTowards,
        Lerp
    }

    public FollowType Type = FollowType.MoveTowards;
    public PathDefinition Path;
    public float Speed = 1.0f;
    public float MaxDistanceToGoal = 0.1f;

    private IEnumerator<Transform> _currentPoint;   // the current point that we are *heading to*

    public void Start()
    {
        if (Path == null)
        {
            Debug.LogError("Path cannot be null", gameObject);
            return;
        }

        _currentPoint = Path.GetPathEnumerator();
        _currentPoint.MoveNext();

        if (_currentPoint.Current == null)
            return;

        transform.position = _currentPoint.Current.position;
    }

    public void Update()
    {
        if (_currentPoint == null || _currentPoint.Current == null)
            return;

        if (Type == FollowType.MoveTowards)
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
        else if (Type == FollowType.Lerp)
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);

        // if we are close enough to our destination point, move our destination to the next point in the sequence.
        // we use sqare magnitude to gain the performance benefit of not having to perform the sqrt in the
        // distance calculation, but that means that we have to square the MaxDistanceToGoal to be uniform.
        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
            _currentPoint.MoveNext();
    }
}
