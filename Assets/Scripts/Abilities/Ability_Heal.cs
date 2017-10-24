using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Heal : Ability
{
    public float m_healAmount;
    public float m_scaleFactor = 1f;    //Currently as 1, needs to be adjusted when we 

    // Ability

    public override void OnAbilityCast()
    {
        base.OnAbilityCast();
    }

    public override void OnAbilityHit()
    {
        base.OnAbilityHit();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Cast a raycast again, then find if it has hit a player
        if (Physics.Raycast(ray, out hit, m_range))
        {
            // If so, instantiate particle system at that player's position
            if (hit.transform.GetComponent<PlayerController>())
            {
                var player = hit.transform.GetComponent<PlayerController>();
                var playerHealth = hit.transform.GetComponent<PlayerHealth>();
                var effect = player.GetComponentInChildren<ParticleSystem>();
                //var effect = Instantiate(m_castEffect, hit.point, Quaternion.identity);

                // Add the particle system object as their parent
                //effect.transform.parent = hit.transform;
                StartCoroutine(PlayEffect(effect));

                if (playerHealth.m_currentHealth < playerHealth.m_maxHealth)
                    playerHealth.Damage(-m_healAmount);


            }
        }
    }

    IEnumerator PlayEffect(ParticleSystem effect)
    {
        Debug.Log("STARTING THE EFFECT");
        effect.Play();
        yield return new WaitForSeconds(effect.duration);
        effect.Stop();
        Debug.Log("GONNA DESTROY NOW");
        yield return new WaitForSeconds(3f);

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //OnAbilityCast();
            OnAbilityHit();
        }
    }



}
