using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetCondition", menuName = "ScriptableObjects/TargetCondition", order = 1)]
public class TargetCondition : ScriptableObject
{
    public string targetName;

    public int RequireTypeZoi;
    public int RequireTypeWorry;
}
