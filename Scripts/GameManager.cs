using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;


public class GameManager : MonoBehaviour
{
    /* RULES */
    private int maxTimesInspectorPerGame;

    private bool RoundStarted;

    [SerializeField]
    private List<GameObject> thePlayers = new List<GameObject>(); // new List<Player>();

    //[SerializeField]
    //private GameObject prefabPlayer;

    [SerializeField]
    private int timesInspector = 2;

    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    private void Start()
    {
        maxTimesInspectorPerGame = 2;
        RoundStarted = false;
    }

    private void Update()
    {
        UpdateListPlayers();

        if (thePlayers.Count >=1)
        {
            //Debug.Log("tout le monde pret ? -> " + everyoneReady());

            List<GameObject> p = PlayersCanBeInspector();
            if(p.Count==0)
            {
                Debug.Log("Fin de partie");
            }
            else
            {
                if(RoundStarted==false && everyoneReady() == true)
                {
                    Debug.Log("La partie se lance");
                    RoundStarted = true;
                    disableReadyUIOnPlayers();/*
                    Debug.Log("joueurs inspecteurs peuvent être : " + p.Count);
                    GameObject inspectorForARound = ChooseAInspector(p);
                    Debug.Log("le joueur inspecteur sera : " + inspectorForARound);
                    Player scriptPlayerInspector = inspectorForARound.GetComponent<Player>();
                    scriptPlayerInspector.becomeInspector();

                    foreach (GameObject player in thePlayers)
                    {
                        if(player!=inspectorForARound)
                        {
                            Player scriptPlayerHidding = player.GetComponent<Player>();
                            scriptPlayerHidding.becomeHidding();
                            Debug.Log("Le joueur " + player.name + " est hidding ! ");
                        }
                    }*/
                }
            }
        }
    }

    private bool everyoneReady()
    {
        foreach(GameObject Go in thePlayers)
        {
            Player p = Go.GetComponent<Player>();
            if(p.isReady==false)
            {
                return false;
            }
        }
        return true;
    }

    public static void RegisterPlayer(string netID, Player player)
    {
        string playerId = playerIdPrefix + netID;
        players.Add(playerId, player);
        player.transform.name = playerId;
    }

    public static void UnregisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }

    public static Player GetPlayer(string playerId)
    {
        return players[playerId];
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 800, 500));
        GUILayout.BeginVertical();
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.black;

        foreach(string playerId in players.Keys)
        {
            GUILayout.Label(playerId + "-" + players[playerId].transform.name + "----" + players[playerId].isReady, style);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void StartGame()
    {

        //StartARound();
        //si tous les joueurs sont éliminés ou qu'il ne reste plus de temps alors 
        //fin du round
        //début d'un nouveau round
    }

    private List<GameObject> PlayersCanBeInspector()
    {
        List<GameObject> playersInspector = new List<GameObject>();
        int maxTimesInspector = 1;
        //cherche la valeur max du nombre de fois ou un joueur a été inspecteur
        foreach (GameObject Go in thePlayers)
        {
            Player p = Go.GetComponent<Player>();
            if (p.XTimesInspector>maxTimesInspector)
            {
                maxTimesInspector = p.XTimesInspector;
            }
        }
        //donne la liste de joueur pouvant être inspecteur
        foreach (GameObject Go in thePlayers)
        {
            Player p = Go.GetComponent<Player>();
            if (p.XTimesInspector < maxTimesInspector && p.XTimesInspector < timesInspector)
            {
                playersInspector.Add(Go);
            }
        }

        return playersInspector;
    }

    private GameObject ChooseAInspector(List<GameObject> p)
    {
        System.Random rnd = new System.Random();
        int nb = rnd.Next(0, p.Count);
        Debug.Log("le joueur inspecteur est : " + p[nb].name);
        return p[nb];
    }


    /// <summary>
    /// update the list of players which is connected to be access to each their variable
    /// </summary>
    private void UpdateListPlayers()
    {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        thePlayers.Clear();
        // putt the Player in a List<GameObject> and not in GameObject[] (array)
        foreach(GameObject player in p)
        {
            thePlayers.Add(player);
        }
    }

    private void disableReadyUIOnPlayers()
    {
        foreach(GameObject Go in thePlayers)
        {
            Hidding p = Go.GetComponent<Hidding>();
            p.DisableReadyUI();
        }
    }
}
