using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject[] segmentPrefab;
    [SerializeField] private int pointValue;
    [SerializeField] private float insert;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip gameOver;

    public bool isDead = false;
    private List<GameObject> segments = new List<GameObject>();
    private List<Vector3> previousPositions = new List<Vector3>();


    public float difficulty;
    
    private GameManager1 gameManager;
   
    private float speed;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Start()
    {   
        // Initialize the first segment
        previousPositions.Add(transform.position);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager1>();
        
      
        
    }

    void Update()
    {    
        
        // Movimenta a cobra
        transform.position += transform.forward * speed * Time.deltaTime;

        // Controle de direção
        float rotation = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotation * rotationSpeed *  Time.deltaTime);

        previousPositions.Insert(0, transform.position);

        // Add Segments to the  snake

        int i = 1; 
        foreach (var segment in segments)
        {
        // Position that first segment  should be in
        Vector3 targetPosition = previousPositions[Mathf.Min(i * Mathf.RoundToInt(insert), previousPositions.Count - 1)];
        Vector3 directionToMove = targetPosition - segment.transform.position;

        // Move segment to the postion
       
        //segment.transform.position += directionToMove * speed * Time.deltaTime;
    
        segment.transform.position += directionToMove;


        // Look at next segment
        segment.transform.LookAt(targetPosition);

        i++;
    }

    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Body"))
        {   
            audioSource.PlayOneShot(gameOver);
            isDead = true;
            Time.timeScale = 0f;  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {   
            audioSource.PlayOneShot(hitSound);
            Grow();
            if(gameManager.easy && !gameManager.medium && !gameManager.hard){
                gameManager.UpdateScore(pointValue);
            }else if(!gameManager.easy && gameManager.medium && !gameManager.hard){
                gameManager.UpdateScore(pointValue + 3);
            }else if(!gameManager.easy && !gameManager.medium && gameManager.hard){
                gameManager.UpdateScore(pointValue + 6);
            }
            
            Destroy(other.gameObject);
        }
    }

    //Increase  the length of the snake

    public void Grow()
    {    
         int index = Random.Range(0,9);
         GameObject snakePrefab = Instantiate(segmentPrefab[index]);
         segments.Add(snakePrefab);
    }

    public void SetCollisionsActive(bool active)
{
    // Desativa/Ativa as colisões do próprio corpo da cobra e dos segmentos
    Collider[] colliders = GetComponentsInChildren<Collider>();
    foreach (Collider collider in colliders)
    {
        collider.enabled = active;
    }
}
}
