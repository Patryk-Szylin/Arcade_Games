using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Utility : MonoBehaviour
{

    public void ApplyDotEffect(int maxTicks, float tickInterval, Action callback)
    {
        StartCoroutine(DotEffect(maxTicks, tickInterval, callback));
    }

    public void ApplySpeedEffect(Action effect, int duration, Action callback)
    {
        StartCoroutine(SpeedEffect(effect, duration, callback));
    }

    public IEnumerator DotEffect(int maxTicks, float tickInterval, Action callback)
    {        
        for (int i = 0; i < maxTicks; i++)
        {
            yield return new WaitForSeconds(tickInterval);
            callback();
        }
    }

    public IEnumerator SpeedEffect(Action effect, int duration, Action callback)
    {
        effect();
        yield return new WaitForSeconds(duration);
        callback();
    }




}
