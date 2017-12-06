using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SO_Destructible_Base : ScriptableObject
{
    public abstract IEnumerator DestroyedEffect(GameObject building);

}
