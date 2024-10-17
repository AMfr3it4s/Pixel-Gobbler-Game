using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{   
    [SerializeField] private Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume")){
           
            Load();
        }
        else{
             PlayerPrefs.SetFloat("musicVolume", 0.15f);
            Load();
        }
    }

    // Update is called once per frame
    public void ChangeVolume (){
        AudioListener.volume = volumeSlider.value;
        Save();

    }

    public void Load (){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save (){
        
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);

    }
}
