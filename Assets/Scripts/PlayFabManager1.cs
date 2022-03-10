using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class high_score
{
    public string name;
    public int score;
}

public class PlayFabManager1 : MonoBehaviour
{
    public hsDisplay[] hsDisplayArray;
    //public InputField nameInput;
    public Text username;

    public string leaderboardName = "PickLeaderboard";

    [Header("Windows")]
    public GameObject nameWindow;
    public GameObject leaderboardWindow;

    [Header("Display name window")]
    public GameObject nameError;
    public InputField nameInput;

    [Header("Leaderboard")]
    public GameObject rowPrefab;
    public Transform rowsParent;



    //List<high_score> scores = new List<high_score>();
    //public Text player1;
    //public Text score1;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        Login();
        i = 0;
        //displayScores();

    }

    // USER LOGIN LEADERBOARD SEND IDK
    void Login()
    {
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result){
        Debug.Log("Successful login/account create!");
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null){
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if(name == null){
            nameWindow.SetActive(true);
        }
        else{
            leaderboardWindow.SetActive(true);
            GetLeaderboard();
        }
        
    }


    //save username/name
    public void SubmitName(){
        var request = new UpdateUserTitleDisplayNameRequest{
            DisplayName = nameInput.text,

        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result){
        Debug.Log("Updated display name!");
        leaderboardWindow.SetActive(true);
        GetLeaderboard();
    }



    void OnError(PlayFabError error){
        Debug.Log("Error while logging in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }
    
    public void SendLeaderboard(int score){
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = leaderboardName,
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);

    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Successful leaderboard sent");
    }

    public void GetLeaderboard(){
        var request = new GetLeaderboardRequest{
            StatisticName = "TiltLeaderboard",
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result){
        foreach (var item in result.Leaderboard){
            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
            //AddScore(item.PlayFabId, item.StatValue);
            hsDisplayArray[i].DisplayHS(item.DisplayName, item.StatValue);
            i++;
            //player1.text = item.PlayFabId;
            //score1.text =
        }
    }

    /*public void AddScore(string entryName, int entryScore){
        scores.Add(new high_score {name = entryName, score = entryScore});
    }

    /*void displayScores(){
        for (int i = 0; i < hsDisplayArray.Length; i++){
            if(i < scores.Count){
                hsDisplayArray[i].DisplayHS(scores[i].name, scores[i].score);
            }
            else{
                hsDisplayArray[i].HideDisplay();
            }
        }
    }*/



    // send data
    public void SaveAppearance(){
        var request = new UpdateUserDataRequest{
            Data = new Dictionary<string, string>{
                {"name", nameInput.text}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult result){
        Debug.Log("Successful user data send!");
    }

    public void GetAppearance(){
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result){
        Debug.Log("Received user data");
        if(result.Data != null && result.Data.ContainsKey("name")){
            //characterEditor.SetAppearance(result.Data["name"].Value);
            username.text = result.Data["name"].Value;

        }
        else{
            Debug.Log("pplayer data not complete!");
        }
    }





    // User Registration
    /*
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;

    public void RegisterButton(){
        var request = new RegisterPlayFabUserRequest{
            Email = emailInput.text, 
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result){
        messageText.text = "registered and logged in";
    }

    public void LoginButton(){

    }

    public void ResetPasswordButton(){

    }*/
    //void OnPasswordReset()
}

