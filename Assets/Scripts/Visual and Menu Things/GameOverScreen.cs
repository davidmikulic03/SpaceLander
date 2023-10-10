using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    PlayerValues playerValues;
    GameManager gameManager;
    [SerializeField] GameObject canvas;

    [SerializeField] float killTime;
    [SerializeField] float killSpeed;
    [SerializeField] float killDistance;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        playerValues = this.gameManager.playerValues;

        canvas.SetActive(false);
    }

    float t = 0;
    private void Update()
    {
        if (!activityCheck)
        {
            if (activityCheck)
                return;

            t += Time.deltaTime;

            if(t >= killTime)
                GameOver();
        }
        else
            t = 0;
    }

    private void GameOver()
    {
        canvas.SetActive(true);
        HighScoreManager highScoreManager = gameManager.highScoreManager;
        highScoreManager.highScoreClass.AssignScore(gameManager.sceneIndex - 1, gameManager.playerValues.score);
        highScoreManager.WriteFile();
        gameManager.Pause();
    }

    public void ResetGame()
    {
        gameManager.ResetGame();
    }

    public void NextLevel()
    {
        gameManager.NextLevel();
    }

    public void PreviousLevel()
    {
        gameManager.PreviousLevel();
    }

    public void ToMainMenu()
    {
        gameManager.ToMainMenu();
    }

    bool activityCheck
    {
        get
        {
            bool hasFuel = playerValues.currentFuel > 0;
            float sqrSpeed = playerValues.sqrSpeed;
            float sqrDistance = Vector3.SqrMagnitude(playerValues.ClosestBody().transform.position - playerValues.gameObject.transform.position);
            
            bool isActive = true;
            if (sqrSpeed <= killSpeed * killSpeed || sqrDistance >= killDistance * killDistance) isActive = false;



            return !(!isActive && !hasFuel);
        }
    }
}