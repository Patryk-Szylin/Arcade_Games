using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Manager : MonoBehaviour
{
    public GameObject[] copsTeam;
    public GameObject[] thiefTeam;
    public GameObject[] enemyTeam;

    public int playerTeam;

    public int teamSize;
    //public int teamSize; copTeamSize, ThiefTeamSize
    

    // Use this for initialization
    void Start () {
        copsTeam = new GameObject[teamSize];
        thiefTeam = new GameObject[teamSize];
        enemyTeam = new GameObject[teamSize*2];

        copsTeam = GameObject.FindGameObjectsWithTag("Cop");
        thiefTeam = GameObject.FindGameObjectsWithTag("Thief");
        enemyTeam = GameObject.FindGameObjectsWithTag("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            copsTeam = GameObject.FindGameObjectsWithTag("Cop");
            thiefTeam = GameObject.FindGameObjectsWithTag("Thief");
            enemyTeam = GameObject.FindGameObjectsWithTag("Enemy");

            teamSettings();
        }
    }

    void teamSettings()
    {
        //0 = cops
        //1 = thief
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
