using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityStatus : MonoBehaviour, IDamageable
{
    [SerializeField]
    private StatusData statusData;

    protected Dictionary<StatusInfo, float> statusTable = new Dictionary<StatusInfo, float>();
    public bool IsDead { get; protected set; } = false;

    [SerializeField]
    public UnityEvent deathEvnet;
    [SerializeField]
    public UnityEvent<float, float> hpEvnet;

    protected virtual void Awake()
    {
        statusData.CreateStatusData(ref statusTable);
    }

    private void Start()
    {
        hpEvnet?.Invoke(statusTable[StatusInfo.Hp], statusData.Hp);
    }

    public virtual void OnDamage(ref DamageInfo damageInfo)
    {
        if (IsDead)
            return;

        statusTable[StatusInfo.Hp] -= damageInfo.damage;
        damageInfo.isHit = true;

        if (statusTable[StatusInfo.Hp] <= 0f)
        {
            statusTable[StatusInfo.Hp] = 0f;
            deathEvnet?.Invoke();
            IsDead = true;
        }

        hpEvnet?.Invoke(statusTable[StatusInfo.Hp], statusData.Hp);
    }

    public float GetStatus(StatusInfo statusInfo)
    {
        return statusTable[statusInfo];
    }

}
