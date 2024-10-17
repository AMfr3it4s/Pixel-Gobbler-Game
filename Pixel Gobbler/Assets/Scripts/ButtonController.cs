using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{   

    private GameManager1 gameManager;
    private int dificulty;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager1>();
    }

    // Update is called once per frame
     public void SetDificulty (){
        gameManager.StartGame(dificulty);
    }
}
