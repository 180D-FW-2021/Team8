using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public GameObject ChopPanel;
    public GameObject IngredientPanel;


    // Start is called before the first frame update
    void Start()
    {
        ChopPanel.SetActive(false);
        IngredientPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectChop(){
        Debug.Log("Chopping minigame description");
        if(ChopPanel != null){
            ChopPanel.SetActive(true);
        }
        
        //SceneManager.LoadScene("ChoppingMinigame", LoadSceneMode.Single);
    }

    public void selectIngredient(){
        Debug.Log("Ingredient minigame description");
        if(IngredientPanel != null){
            IngredientPanel.SetActive(true);
        }
        
        //SceneManager.LoadScene("SpeechTest", LoadSceneMode.Single);
    }

    public void ingredientStart(){
        Debug.Log("Ingredient minigame loading");
        SceneManager.LoadScene("3D_Shelf", LoadSceneMode.Single);
        if(IngredientPanel != null){
            IngredientPanel.SetActive(false);
        }
        if(ChopPanel != null){
            ChopPanel.SetActive(false);
        }
    }

    public void chopStart(){
        Debug.Log("Chopping minigame loading");        
        SceneManager.LoadScene("3D_Chop", LoadSceneMode.Single);
        if(IngredientPanel != null){
            IngredientPanel.SetActive(false);
        }
        if(ChopPanel != null){
            ChopPanel.SetActive(false);
        }
    }

    public void returnToGameSelection(){
        if(IngredientPanel != null){
            IngredientPanel.SetActive(false);
        }
        if(ChopPanel != null){
            ChopPanel.SetActive(false);
        }
    }
}
