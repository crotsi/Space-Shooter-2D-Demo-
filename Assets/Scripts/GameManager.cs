using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject uzayAraci;    
    public GameObject enemySpawner;   
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    
    public enum GameManagerState
    {
        Opening,
        GamePlay,
        GameOver,
    }

    GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                playButton.SetActive(true);
                break;
            case GameManagerState.GamePlay:
                //Reset the score
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                playButton.SetActive(false);
                uzayAraci.GetComponent<PlayerControl>().Init();
                //Start enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                enemySpawner.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                enemySpawner.GetComponent<EnemySpawner1>().ScheduleEnemySpawner();
                break;
            case GameManagerState.GameOver:
                GameOverGO.SetActive(true);
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                enemySpawner.GetComponent<EnemySpawner2>().UnscheduleEnemySpawner();
                enemySpawner.GetComponent<EnemySpawner1>().UnscheduleEnemySpawner();
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.GamePlay;
        UpdateGameManagerState();
    }
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
