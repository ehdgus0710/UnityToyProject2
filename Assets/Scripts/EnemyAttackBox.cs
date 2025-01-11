using System.Collections;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    [SerializeField] private EnemyStatus status;
    [SerializeField] private float reloadTime;
    [SerializeField] private float damage;

    [SerializeField]
    private LayerMask targetLayer;

    private Collider[] targetColliders = new Collider[1];
    private float currentHitTime = 0f;

    private bool IsReloadAttack { get; set; } = false;

    private void Update()
    {
        if (IsReloadAttack)
        {
            if (currentHitTime + reloadTime <= Time.time)
                IsReloadAttack = false;
        }
    }

    private void FixedUpdate()
    {
        if (status.IsDead)
            return;

        if (!IsReloadAttack && Physics.OverlapBoxNonAlloc(transform.position, transform.localScale, targetColliders, transform.rotation, targetLayer.value) != 0)
        {
            Vector3 hitPoint = targetColliders[0].ClosestPoint(transform.position);
            Vector3 overlapDirection = (hitPoint - transform.position).normalized;

            DamageInfo damageInfo = new DamageInfo();
            damageInfo.damage = damage;
            damageInfo.hitPoint = hitPoint;
            damageInfo.hitDirection = overlapDirection;

            targetColliders[0].GetComponent<IDamageable>()?.OnDamage(ref damageInfo);

            if (!damageInfo.isHit)
                return;

            currentHitTime = Time.time;
            IsReloadAttack = true;
        }
    }
    private IEnumerator CoReloadAttack()
    {
        IsReloadAttack = true;
        yield return new WaitForSeconds(1f);
        IsReloadAttack = false;
    }
}
