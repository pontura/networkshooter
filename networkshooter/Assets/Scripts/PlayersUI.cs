using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersUI : MonoBehaviour
{
    public PlayerUI playerUIToInstantiate;
    public Transform container;
    public List<PlayerUI> all;

    private void Awake()
    {
        Events.OnAddPlayer += OnAddPlayer;
        Events.OnRemovePlayer += OnRemovePlayer;
    }
    private void OnDestroy()
    {
        Events.OnAddPlayer -= OnAddPlayer;
        Events.OnRemovePlayer -= OnRemovePlayer;
    }
    public void OnAddPlayer(NetworkIdentity ni)
    {
        PlayerUI playerUI = Instantiate(playerUIToInstantiate, container);
        playerUI.Init(ni);
        all.Add(playerUI);
    }
    public void OnRemovePlayer(NetworkIdentity ni)
    {
        PlayerUI playerUI = null;
        Debug.Log("Borra player " + ni.GetID());
        string userID = ni.GetID();
        foreach (PlayerUI p in all)
            if (p.userID == userID)
                playerUI = p;
        if(playerUI != null)
        {
            all.Remove(playerUI);
            Destroy(playerUI.gameObject);
            Debug.Log("Destroy playerUI " + playerUI);
        }
    }
}
