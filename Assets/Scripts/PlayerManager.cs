using UnityEngine;

class PlayerManager : MonoBehaviour
{
    public Player Kierth;
    public Player Konara;
    public Player Naratir;
    public Player Natar;

    // INSTANCES

    private Player kierth;
    private Player konara;
    private Player naratir;
    private Player natar;

    public void Start()
    {

    }

    public Player Summon(PlayerType playerType)
    {
        GameObject obj = GameObject.Instantiate(GetPrefab(playerType));
        Player player = obj.GetComponent<Player>();



        return player;
    }

    public void Destroy(Player player)
    {
        player.Destroy();
        
    }

    private GameObject GetPrefab(PlayerType player)
    {

        if (player == PlayerType.Kierth)
            return Kierth.gameObject;

        if (player == PlayerType.Konara)
            return Konara.gameObject;

        if (player == PlayerType.Naratir)
            return Naratir.gameObject;

        if (player == PlayerType.Natar)
            return Natar.gameObject;
        
        return null;
    }
}