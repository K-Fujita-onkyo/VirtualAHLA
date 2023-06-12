using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour{
    // Update is called once per frame
    public void ClickStartButton(){
        SceneManager.LoadScene("GameScene");
    }
}
