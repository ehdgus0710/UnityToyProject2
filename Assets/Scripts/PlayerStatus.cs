using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : EntityStatus
{
    [SerializeField] private Color  effectEndColor;
    [SerializeField] private Image  hitImage;
    [SerializeField] private Slider hpbar;
    [SerializeField] private float  hitTime;
    private bool                    isHit = false;


    protected override void Awake()
    {
        base.Awake();

        hpEvnet.AddListener(OnChangeHp);
    }

    public override void OnDamage(ref DamageInfo damageInfo)
    {
        if (IsDead)
            return;

        if(isHit)
        {
            damageInfo.isHit = false;
            return;
        }

        statusTable[StatusInfo.Hp] -= damageInfo.damage;
        damageInfo.isHit = true;
        StartCoroutine(CoHitEffect());

        if (statusTable[StatusInfo.Hp] <= 0f)
        {
            statusTable[StatusInfo.Hp] = 0f;
            deathEvnet?.Invoke();
            IsDead = true;
        }

        hpEvnet?.Invoke(statusTable[StatusInfo.Hp], statusData.Hp);
    }

    public void OnChangeHp(float hp, float maxHp)
    {
        hpbar.value = hp / maxHp;
        hpbar.maxValue = maxHp / maxHp;
    }

    private IEnumerator CoHitEffect()
    {
        hitImage.gameObject.SetActive(true);
        Color originalColor = hitImage.color;
        Color currentColor = originalColor;

        isHit = true;

        float currentHitTime = 0f;

        while (currentHitTime < hitTime)
        {
            currentHitTime += Time.deltaTime;

            currentColor = Color.Lerp(originalColor, effectEndColor, currentHitTime / hitTime);
            hitImage.color = currentColor;

            yield return new WaitForEndOfFrame();
        }

        isHit = false;
        hitImage.gameObject.SetActive(false);
        hitImage.color = originalColor;
    }
}
