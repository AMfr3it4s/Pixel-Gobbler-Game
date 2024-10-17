using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    [SerializeField] private Button turnOnButton;
    [SerializeField] private GameObject informationText;
    [SerializeField] private GameObject windowsStartup;
    [SerializeField] private GameObject windowsBackground;
    [SerializeField] private GameObject computerLights;
    private float waitTime = 12.36f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnComputer (){
        informationText.SetActive(false);
        windowsStartup.SetActive(true);
        turnOnButton.interactable = false;
        computerLights.SetActive(false);
        StartCoroutine(WaitStartUp());
        
    }

    IEnumerator WaitStartUp(){
        yield return new WaitForSeconds(waitTime);
        windowsBackground.SetActive(true);
    }

    

    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }


}
