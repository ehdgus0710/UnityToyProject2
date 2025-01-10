using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct DamageInfo
{
    public Vector3      hitPoint;
    public Vector3      normalPoint;
    public Vector3      hitDirection;
    public float        damage;
    public UnityEvent   hitEvent;

    public bool         isHit;
}
