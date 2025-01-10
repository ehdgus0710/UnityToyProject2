using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float damage;

    [SerializeField]
    private float lifeTime;

    private DamageInfo damageInfo;
    private Vector3 moveDirection;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Shoot(Vector3 dirction, UnityAction? hitAction = null)
    {
        moveDirection = dirction;
        damageInfo.damage = damage;

        if(hitAction != null)
            damageInfo.hitEvent.AddListener(hitAction);
    }

    void Update()
    {
        transform.position += moveDirection * (moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & targetLayer.value) != 0)
        {
            var damageable = other.GetComponent<IDamageable>();

            if(damageable != null && !damageable.IsDead)
            {
                damageInfo.hitPoint = other.ClosestPoint(transform.position);
                damageInfo.hitDirection = (damageInfo.hitPoint  - transform.position).normalized;
                damageable.OnDamage(ref damageInfo);

                if (damageInfo.isHit)
                {
                    damageInfo.hitEvent?.Invoke();
                    Destroy(gameObject);
                }
            }

        }
    }
}
