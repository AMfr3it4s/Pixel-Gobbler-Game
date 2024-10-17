using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{   [SerializeField] private  GameObject player;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject startGame;
    [SerializeField] private GameObject food;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject scoreGameObject;
    [SerializeField] private GameObject pauseGame;
    [SerializeField] private SoundManager soundManager;
    private float boundX = 18.0f;
    private float boundZ = 21.0f;
    private Vector3 position;
    //private Vector3 positionFood;
    public int enemyCount;
    public bool gameStarted = false;
    public AudioSource audioSourceCamera;
    public float speed = 6.0f;
    private int score;
    private int highscore;
    private bool gameIsPaused;
    public bool easy = false;
    public bool medium = false;
    public bool hard = false;

    

    // Start is called before the first frame update
    void Start()
    {   
        position =  new Vector3(Random.Range(-boundX, boundX), 0, Random.Range(-boundZ, boundZ));
       
       
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highscore = PlayerPrefs.GetInt("HighScore");
        }
        highScoreText.text = "HighScore: " +highscore;
        
         if(PlayerPrefs.HasKey("musicVolume")){
           
            soundManager.Load();
        }
        else{
             PlayerPrefs.SetFloat("musicVolume", 0.15f);
            soundManager.Load();
        }
        
    }

    // Update is called once per frame
    void Update()
    {    
        enemyCount = FindObjectsOfType<FoodController>().Length;
        if(enemyCount == 0 && gameStarted){
            Instantiate(food.gameObject, GenerateSpawnPosition(), food.transform.rotation);
        }

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if(playerController != null && playerController.isDead){
            gameOver.SetActive(true);

        }

        if(playerController != null && playerController.isDead)
        {
            audioSourceCamera.Stop();
        }

        if(Input.GetKeyDown(KeyCode.P)){
            if(gameIsPaused && !playerController.isDead){
                ResumeGame();
                playerController.SetCollisionsActive(true);
            }else if (!playerController.isDead){
                PauseGame();
                playerController.SetCollisionsActive(false);
            }
        }

        if(gameStarted){
            if (score > highscore)
        {
            highscore = score;
            // Salvar o novo highscore de forma permanente
            PlayerPrefs.SetInt("HighScore", highscore);
            PlayerPrefs.Save();
            highScoreText.text = "HighScore: " +highscore;
        }
        }
      
        
    }


    public void UpdateScore (int scoreAdd){
        score += scoreAdd;
        scoreText.text = "Score: " + score;
    }

    public void Restart (){
        Time.timeScale =1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Quit(){
        Application.Quit();
    }

    private Vector3 GenerateSpawnPosition(){
        float  spawnX = Random.Range(-boundX, boundX);
        float spawnZ = Random.Range(-boundZ, boundZ);
        Vector3 randomPos = new Vector3(spawnX,0,spawnZ);
        return  randomPos;

    }

    void PauseGame()
    {   
        pauseGame.SetActive(true);
        Time.timeScale = 0f; // Pausa o tempo no jogo
        gameIsPaused = true;
       
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Retoma o tempo normal no jogo
        gameIsPaused = false;
        pauseGame.SetActive(false);
        
    }
    public void StartGame(int difficulty){
        gameStarted = true;
        GameObject playerInstance = Instantiate(player.gameObject, position, player.transform.rotation);
        PlayerController playerController = playerInstance.GetComponent<PlayerController>();
        float adjustedSpeed = speed;
        if(difficulty ==1){
            adjustedSpeed = speed * difficulty * 1f;
            easy = true;
        }
        else if (difficulty == 2){
            adjustedSpeed = speed * difficulty /1.33f;
            medium = true;
        }else if (difficulty ==3) {
           adjustedSpeed =  speed * difficulty /1.4f;
           hard = true;
        }
        playerController.SetSpeed(adjustedSpeed);
        audioSourceCamera.Play();
        startGame.SetActive(false);
        scoreGameObject.SetActive(true);
        int score = 0;
        
        UpdateScore(score);
        
        
        
    }

   
}
