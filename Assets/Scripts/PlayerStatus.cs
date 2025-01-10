using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : EntityStatus
{
    [SerializeField] private Slider hpbar;


    protected override void Awake()
    {
        base.Awake();

        hpEvnet.AddListener(OnChangeHp);
    }


    public void OnChangeHp(float hp, float maxHp)
    {
        hpbar.value = hp / maxHp;
        hpbar.maxValue = maxHp / maxHp;
    }
}
