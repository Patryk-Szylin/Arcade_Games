using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Desctructible_Effect : ScriptableObject
{
    public abstract IEnumerator DestroyedEffect(GameObject building);
}
