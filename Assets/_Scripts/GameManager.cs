using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates { None,Setup, Playing ,PlayerWon,PlayerDied}

public class GameManager : MonoBehaviour
{
    [SerializeField]GameStates currentState;
    [SerializeField] Vector3 PlayerStartPos;
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] Portal[] portals;

    [SerializeField] Portal spawnPortal;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] Transition transition;


    bool isLoading;
    Player player;
    public static GameManager inst;

    void Awake()
    {
        if(GameManager.inst != null && GameManager.inst != this) Destroy(gameObject); // destorys itself if a reference already exists
        else inst = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Setup_Wrapper;
    }

    void onDisable()=> SceneManager.sceneLoaded -= Setup_Wrapper;



    void Update()
    {
        if(currentState == GameStates.Setup) Handle_Setup();
        if(currentState == GameStates.PlayerDied) StartCoroutine(CO_Handle_PlayerDied());
        if(currentState == GameStates.PlayerWon) Handle_PlayerWon();
    }


    public void LoadNextLevel()
    {
        StartCoroutine(CO_LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
    }

    public IEnumerator CO_LoadLevel(int buildIndex)
    {

        Debug.Log("LOAD");
        if(isLoading) yield break;
        isLoading = true;

        if(transition != null)
        {
            transition.SlideIn();
            yield return new WaitForSeconds(1.5f);
        }
        SceneManager.LoadScene(buildIndex);
        isLoading = false;

    }

    public static void ExitGame() => Application.Quit();



    #region  HandleStates

    void Setup_Wrapper(Scene loadScene, LoadSceneMode mode) => SetState(GameStates.Setup);

    void Handle_Setup()
    {

        portals = FindObjectsOfType<Portal>();
        player = FindObjectOfType<Player>();
        transition = FindObjectOfType<Transition>();

        if (player == null)
        {
            player = Instantiate(PlayerPrefab, PlayerStartPos, Quaternion.identity).GetComponent<Player>();
        }

        spawnPortal = GetSpawnPortal();
        if(spawnPortal != null ) spawnPortal.TeleportObject(player.transform);
        else player.transform.position = spawnPos;

        if(SceneManager.GetActiveScene().name != "Menu"  && (transition!=null))transition.SlideOut();

        SetState(GameStates.Playing);
    }

    void Handle_PlayerWon()
    {
        StartCoroutine(CO_LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); //load the next level
    }

    IEnumerator CO_Handle_PlayerDied()
    {
        SetState(GameStates.None);
        Debug.Log("GAME OVEr");
        yield return new WaitForSeconds(1f);
        StartCoroutine(CO_LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    #endregion

    public void SetState(GameStates newState)
    {
        if(currentState == newState) return;
        currentState = newState;
    }

    Portal GetSpawnPortal()
    {
        Portal selected = null;
        foreach (Portal portal in portals)
        {
            if(portal.isSpawnPoint) selected = portal;
        }

        return selected;
    }
}
