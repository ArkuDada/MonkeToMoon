using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InteractionSO", menuName = "ScriptableObjects/Interaction")]
public class InteractionSO : ScriptableObject
{
    public List<ItemDataSO> items;
    public Era era;
}