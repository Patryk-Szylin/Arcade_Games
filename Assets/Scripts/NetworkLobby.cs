using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class NetworkLobby : LobbyHook
{

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        base.OnLobbyServerSceneLoadedForPlayer(manager, lobbyPlayer, gamePlayer);

        LobbyPlayer lp = lobbyPlayer.GetComponent<LobbyPlayer>();
        var pSetup = gamePlayer.GetComponent<PlayerSetup>();

        pSetup.m_playerName = lp.playerName;
        pSetup.m_playerColor = lp.playerColor;

        // Get player component from lobby and add it to the game manager's m_allPlayers variable.
        // This will be used for scoreboards and keep count of how many players are in the game
        Player p = gamePlayer.GetComponent<Player>();

        if(p != null)
        {
            GameManager.m_allPlayers.Add(p);
        }



    }


}
