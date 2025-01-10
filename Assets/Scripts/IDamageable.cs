public interface IDamageable
{
    public void OnDamage(ref DamageInfo damageInfo);
    public bool IsDead {  get; }
}
