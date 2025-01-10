using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Data/StatusData", order = 1)]
public class StatusData : ScriptableObject
{
    [field: SerializeField]
    public float Hp { private set; get; }

    [field: SerializeField]
    public float MoveSpeed { private set; get; }

    [field: SerializeField]
    public float AttackSpeed { private set; get; }

    public void CreateStatusData(ref Dictionary<StatusInfo, float> statusTable)
    {
        statusTable.Add(StatusInfo.Hp, Hp);
        statusTable.Add(StatusInfo.Speed, MoveSpeed);
        statusTable.Add(StatusInfo.AttackSpeed, AttackSpeed );
    }
}
