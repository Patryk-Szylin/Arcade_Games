using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Manager : MonoBehaviour
{
    public bool teamMatch = false;
    public GameObject localPlayer;
    public GameObject[] copsTeam;
    public GameObject[] thiefTeam;
    public GameObject[] enemyTeam;

    public int playerTeam;
    public int teamSize;

    // Use this for initialization
    void Start () {
        StartCoroutine(startInfo());
    }
	
	// Update is called once per frame
	void getUpdate () {
        if (teamMatch)
        {
            localPlayer = GameObject.FindGameObjectWithTag("Player");
            copsTeam = new GameObject[teamSize];
            thiefTeam = new GameObject[teamSize];

            copsTeam = GameObject.FindGameObjectsWithTag("Cop");
            thiefTeam = GameObject.FindGameObjectsWithTag("Thief");
        }
        else
        {
            localPlayer = GameObject.FindGameObjectWithTag("Player");
            enemyTeam = new GameObject[teamSize];
            enemyTeam = GameObject.FindGameObjectsWithTag("Enemy");
        }
        teamSettings();
    }

    void teamSettings()
    {
        //0 = cops
        //1 = thief
        if (teamMatch)
        {
            foreach (GameObject cops in copsTeam)
            {
                if(playerTeam == 0)
                    cops.GetComponent<PlayerHealth>().setTeam(true);
                else
                    cops.GetComponent<PlayerHealth>().setTeam(false);
            }

            foreach (GameObject thief in thiefTeam)
            {
                if (playerTeam == 1)
                    thief.GetComponent<PlayerHealth>().setTeam(true);
                else
                    thief.GetComponent<PlayerHealth>().setTeam(false);
            }
        }
        else
        {
            //0 = player
            //1 = enemy
            foreach (GameObject enemy in enemyTeam)
            {
                if (playerTeam == 2)
                    enemy.GetComponent<PlayerHealth>().setTeam(true);
                else
                    enemy.GetComponent<PlayerHealth>().setTeam(false);
            }
        }
    }

    IEnumerator startInfo()
    {
        yield return new WaitForSeconds(0.5f);
        getUpdate();
    }


}
