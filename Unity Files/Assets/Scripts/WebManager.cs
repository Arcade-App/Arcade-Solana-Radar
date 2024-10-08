using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text.RegularExpressions; // For email validation
using System.Collections.Generic;

public class WebManager : MonoBehaviour
{

    //string servername = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/";

    [Header("Server Settings")]
    string registerUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/RegisterUser.php";
    string updateWalletUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/UpdateWallet.php";
    string signInUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/SignInUser.php";
    string storeGameUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/StoreCreatedGame.php";
    string getUserGamesUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetUserGames.php";
    string storeTournamentUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/StoreCreatedTournament.php";
    string getTournamentManagerUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetTournamentManagerAddress.php";
    string getUserTournamentsUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetUserTournaments.php";
    string getTop30GamesUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetTop30Games.php";
    string getAllTournamentsUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetAllTournaments.php";
    string getUserJoinedTournamentDetailsUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetUserJoinedTournaments.php";
    string getTournamentScoresUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetTournamentScores.php";
    string storeTournamentScoreUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/StoreTournamentScore.php";
    string getGameTemplateIdUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetGameTemplateId.php";
    string getOrCreateGameScoreUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetOrCreateGameScore.php";
    string updateGameScoreUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/UpdateGameScore.php";
    string incrementGamePlayCountUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/IncrementGamePlayCount.php";
    string getGameDetailsUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetGameDetails.php";
    string getOrCreateTournamentScoreUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/GetOrCreateTournamentScore.php";
    string incrementTournamentPlayCountUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/IncrementTournamentPlayCount.php";
    string incrementTournamentUserCountUrl = "https://bisque-leopard-560027.hostingersite.com/solanahackbackend/IncrementTournamentUserCount.php";

    //[Header("Server Settings")]
    //string registerUrl = "http://localhost/AptosHackBackendScripts/RegisterUser.php";
    //string updateWalletUrl = "http://localhost/AptosHackBackendScripts/UpdateWallet.php";
    //string signInUrl = "http://localhost/AptosHackBackendScripts/SignInUser.php";
    //string storeGameUrl = "http://localhost/AptosHackBackendScripts/StoreCreatedGame.php";
    //string getUserGamesUrl = "http://localhost/AptosHackBackendScripts/GetUserGames.php";
    //string storeTournamentUrl = "http://localhost/AptosHackBackendScripts/StoreCreatedTournament.php";
    //string getTournamentManagerUrl = "http://localhost/AptosHackBackendScripts/GetTournamentManagerAddress.php";
    //string getUserTournamentsUrl = "http://localhost/AptosHackBackendScripts/GetUserTournaments.php";
    //string getTop30GamesUrl = "http://localhost/AptosHackBackendScripts/GetTop30Games.php";
    //string getAllTournamentsUrl = "http://localhost/AptosHackBackendScripts/GetAllTournaments.php";
    //string getUserJoinedTournamentDetailsUrl = "http://localhost/AptosHackBackendScripts/GetUserJoinedTournaments.php";
    //string getTournamentScoresUrl = "http://localhost/AptosHackBackendScripts/GetTournamentScores.php";
    //string storeTournamentScoreUrl = "http://localhost/AptosHackBackendScripts/StoreTournamentScore.php";
    //string getGameTemplateIdUrl = "http://localhost/AptosHackBackendScripts/GetGameTemplateId.php";
    //string getOrCreateGameScoreUrl = "http://localhost/AptosHackBackendScripts/GetOrCreateGameScore.php";
    //string updateGameScoreUrl = "http://localhost/AptosHackBackendScripts/UpdateGameScore.php";
    //string incrementGamePlayCountUrl = "http://localhost/AptosHackBackendScripts/IncrementGamePlayCount.php";
    //string getGameDetailsUrl = "http://localhost/AptosHackBackendScripts/GetGameDetails.php";
    //string getOrCreateTournamentScoreUrl = "http://localhost/AptosHackBackendScripts/GetOrCreateTournamentScore.php";
    //string incrementTournamentPlayCountUrl = "http://localhost/AptosHackBackendScripts/IncrementTournamentPlayCount.php";
    //string incrementTournamentUserCountUrl = "http://localhost/AptosHackBackendScripts/IncrementTournamentUserCount.php";





    public void RegisterUser(string username, string email, string password)
    {
        // Validate the input before sending the request
        if (ValidateInput(username, email, password))
        {
            Debug.Log("Register User: " + "Username: " + username + " Email: " + email + " Password: " + password);
            // Start the coroutine to register the user
            StartCoroutine(RegisterUserCoroutine(username, email, password));
        }
    }


    public void SignInUser(string username, string password)
    {        
        // Start the sign-in process
        StartCoroutine(SignIn(username, password));
    }



    private bool ValidateInput(string username, string email, string password)
    {
        // Check if username is empty or too short
        if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
        {
            Debug.LogError("Username must be at least 3 characters long.");
            Manager.instance.canvasManager.ShowErrorPopup("Username must be at least 3 characters long.");
            return false;
        }

        // Check if email is empty or not in valid format
        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
        {
            Debug.LogError("Please enter a valid email address.");
            Manager.instance.canvasManager.ShowErrorPopup("Please enter a valid email address.");
            return false;
        }

        // Check if password is empty or too short
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
        {
            Debug.LogError("Password must be at least 6 characters long.");
            Manager.instance.canvasManager.ShowErrorPopup("Password must be at least 6 characters long.");
            return false;
        }


        // If all checks pass
        return true;
    }

    private bool IsValidEmail(string email)
    {
        // Regular expression for basic email validation
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }


    // We're not providing wallet address and wallet balance while registering the user, those will be added after wallet creation
    public IEnumerator RegisterUserCoroutine(string username, string email, string password)
    {

        Debug.Log("Register User Coroutine: " + "Username: " + username + " EMail: " + email + " Password: " + password);


        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("email", email);
        form.AddField("password", password);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(registerUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                RegisterUserResponse responseData = JsonUtility.FromJson<RegisterUserResponse>(responseText);

                if (responseData.status == "success")
                {
                    Debug.Log("User registered successfully! User ID: " + responseData.userId);

                    // Store the user details in the userInfoManager
                    Manager.instance.userInfoManager.userId = responseData.userId;
                    Manager.instance.userInfoManager.username = username;
                    Manager.instance.userInfoManager.email = email;
                    Manager.instance.userInfoManager.password = password;


                    //Get Tournament Manager Address
                    yield return StartCoroutine(GetTournamentManagerDataCoroutine());


                    // Proceed with account creation progress
                    StartCoroutine(Manager.instance.canvasManager.CreateAccountProgress());
                }
                else
                {
                    // Handle error response from the server
                    Debug.LogError("Registration failed: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Registration failed: " + responseData.message);

                }
            }
        }
    }

    private IEnumerator SignIn(string username, string password)
    {
        // Create a form and add the input fields
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        // Send the request to the PHP server
        using (UnityWebRequest www = UnityWebRequest.Post(signInUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // Display network error
                Debug.LogError("Connection error: " + www.error);
                Manager.instance.canvasManager.ShowErrorPopup("Connection error: " + www.error);

                // Optionally display error in UI
                // errorMessageText.text = "Connection error: " + www.error;
            }
            else
            {
                // Handle the server response
                string responseText = www.downloadHandler.text;
                Debug.Log("Server response: " + responseText);

                // Deserialize the JSON response
                SignInResponseData responseData = JsonUtility.FromJson<SignInResponseData>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    Debug.Log("User signed in successfully!");
                    Debug.Log("User ID: " + responseData.userId);
                    Debug.Log("Username: " + responseData.username);
                    Debug.Log("Email: " + responseData.email);
                    Debug.Log("Wallet Address: " + responseData.walletAddress);
                    Debug.Log("Wallet Mnemonics: " + (responseData.walletMnemonics ?? "No Mnemonics"));

                    // Store user info in Manager's userInfoManager
                    Manager.instance.userInfoManager.userId = responseData.userId;
                    Manager.instance.userInfoManager.username = responseData.username;
                    Manager.instance.userInfoManager.email = responseData.email;
                    Manager.instance.userInfoManager.walletAddress = responseData.walletAddress;
                    Manager.instance.userInfoManager.walletMnemonics = responseData.walletMnemonics;

                    // If wallet mnemonics are available, process them
                    if (!string.IsNullOrEmpty(responseData.walletMnemonics))
                    {
                        // Get Wallet, Wallet Balance, and Store Wallet Balance in userInfoManager
                        //Manager.instance.walletManager.GetWalletFromMenmonicsKey(Manager.instance.userInfoManager.walletMnemonics);
                        Manager.instance.solanaWalletManager.LoadAccount(Manager.instance.userInfoManager.walletMnemonics);
                    }

                    //Get Tournament Manager Address
                    yield return StartCoroutine(GetTournamentManagerDataCoroutine());

                    // Proceed to home screen or any other post-sign-in actions
                    StartCoroutine(Manager.instance.canvasManager.OnSuccessfulSignInProceedHome());
                }
                else
                {
                    // Show the error message from the server
                    Debug.LogError("Login failed: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Login failed: " + responseData.message);

                    // Optionally display error in UI
                    // errorMessageText.text = "Login failed: " + responseData.message;
                }
            }
        }
    }




    public IEnumerator UpdateWallet(int userId, string walletAddress, int walletBalance, string walletMnemonics)
    {
        // Start the coroutine to update the user's wallet information
        yield return StartCoroutine(UpdateWalletCoroutine(userId, walletAddress, walletBalance, walletMnemonics));
    }

    private IEnumerator UpdateWalletCoroutine(int userId, string walletAddress, int walletBalance, string walletMnemonics)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        form.AddField("walletAddress", walletAddress);
        form.AddField("walletBalance", walletBalance);
        form.AddField("walletMnemonics", walletMnemonics);  // Add the wallet mnemonics

        Debug.Log("Updating wallet: " + walletBalance + " Wallet Address: " + walletAddress);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(updateWalletUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Parse the JSON response
                UpdateWalletResponse responseData = JsonUtility.FromJson<UpdateWalletResponse>(responseText);

                // Check if the update was successful
                if (responseData.status == "success")
                {
                    Debug.Log("User wallet information updated successfully!");
                }
                else
                {
                    Debug.LogError("Error updating user wallet information: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Error updating user wallet information: " + responseData.message);

                }
            }
        }
    }

    public IEnumerator GetTournamentManagerDataCoroutine()
    {
        // Send a GET request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Get(getTournamentManagerUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Deserialize the JSON response
                TournamentManagerResponse responseData = JsonUtility.FromJson<TournamentManagerResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    string tournamentManagerAddress = responseData.tournamentManagerAddress;
                    string tournamentContractName = responseData.contractName;

                    Debug.Log("Tournament Manager Address: " + tournamentManagerAddress);
                    Debug.Log("Contract Name: " + tournamentContractName);

                    Manager.instance.tournamentDataManager.tournamentManagerAddress = tournamentManagerAddress;
                    Manager.instance.tournamentDataManager.tournamentContractName = tournamentContractName;
                    Manager.instance.solanaWalletManager.SetTournamentManagerData(tournamentManagerAddress, tournamentContractName);
                }
                else
                {
                    Debug.LogError("Failed to fetch Tournament Manager Address: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Failed to fetch Tournament Manager Address: " + responseData.message);

                }
            }
        }
    }


    public IEnumerator StoreGameData(
                                int userId,
                                int templateId,
                                int faceId,
                                int backgroundId,
                                int jumpAudioId,
                                int backgroundAudioId,
                                int gameOverAudioId,
                                string gameName,
                                int playCount)
    {
        yield return StartCoroutine(StoreGameDataCoroutine(userId, templateId, faceId, backgroundId, jumpAudioId, backgroundAudioId, gameOverAudioId, gameName, playCount));
    }


    public IEnumerator StoreGameDataCoroutine(
                                                int userId,
                                                int templateId,
                                                int faceId,
                                                int backgroundId,
                                                int jumpAudioId,
                                                int backgroundAudioId,
                                                int gameOverAudioId,
                                                string gameName,
                                                int playCount)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        form.AddField("templateId", templateId);
        form.AddField("faceId", faceId);
        form.AddField("backgroundId", backgroundId);
        form.AddField("jumpAudioId", jumpAudioId);
        form.AddField("backgroundAudioId", backgroundAudioId);
        form.AddField("gameOverAudioId", gameOverAudioId);
        form.AddField("gameName", gameName);
        form.AddField("playCount", playCount);

        // Log the game data for debugging
        Debug.Log("Storing Game Data: " + "UserId: " + userId + " GameName: " + gameName);

        // Send a POST request to the PHP script to store the game data
        using (UnityWebRequest www = UnityWebRequest.Post(storeGameUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Deserialize the JSON response from the server
                StoreGameResponse responseData = JsonUtility.FromJson<StoreGameResponse>(responseText);

                // Check if the game data was stored successfully
                if (responseData.status == "success")
                {
                    Debug.Log("Game data stored successfully! Game ID: " + responseData.gameId);
                    Manager.instance.gameDataManager.gameId = responseData.gameId;

                    Manager.instance.canvasManager.ShowGameSharePanel();
                }
                else
                {
                    Debug.LogError("Failed to store game data: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Failed to store game data: " + responseData.message);

                }
            }
        }
    }


    public IEnumerator GetUserGames(int userId)
    {

        yield return StartCoroutine(GetUserGamesCoroutine(userId));

    }

    public IEnumerator GetUserGamesCoroutine(int userId)
    {

        Debug.Log("1. Starting to retrieve User Games Data");

        // Create a form and add the userId field
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(getUserGamesUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("2. Response from server: " + responseText);

                // Deserialize the JSON response
                UserGamesResponse responseData = JsonUtility.FromJson<UserGamesResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    // Clear the existing lists
                    ClearUserGameLists();

                    if (responseData.games.Count == 0)
                    {
                        Debug.Log("No games found for this user.");
                        // Handle the empty list scenario here, if needed
                    }
                    else
                    {
                        // Store the game data in the appropriate lists
                        foreach (var game in responseData.games)
                        {
                            Manager.instance.gameDataManager.userGameIdList.Add(game.gameId);
                            Manager.instance.gameDataManager.userGameTemplateIdList.Add(game.templateId);
                            Manager.instance.gameDataManager.userGameFaceIdList.Add(game.faceId);
                            Manager.instance.gameDataManager.userGameBackgroundIdList.Add(game.backgroundId);
                            Manager.instance.gameDataManager.userGameJumpAudioIdList.Add(game.jumpAudioId);
                            Manager.instance.gameDataManager.userGameBGAudioIdList.Add(game.backgroundAudioId);
                            Manager.instance.gameDataManager.userGameGameOverAudioIdList.Add(game.gameOverAudioId);
                            Manager.instance.gameDataManager.userGameNameList.Add(game.gameName);
                            Manager.instance.gameDataManager.userGamePlayCountList.Add(game.playCount);
                        }
                    }



                    // Optionally, log the result
                    Debug.Log("3. Game data retrieved and stored successfully.");

        
                }
                else
                {
                    // Handle error response from the server
                    Debug.LogError("Failed to fetch user games: " + responseData.message);



                    if (responseData.message == "No games found for the user.")
                    {
                        //Do nothing for now
                    }
                }
            }
        }
    }

    // Clear all game-related lists
    private void ClearUserGameLists()
    {
        Manager.instance.gameDataManager.userGameIdList.Clear();
        Manager.instance.gameDataManager.userGameTemplateIdList.Clear();
        Manager.instance.gameDataManager.userGameFaceIdList.Clear();
        Manager.instance.gameDataManager.userGameBackgroundIdList.Clear();
        Manager.instance.gameDataManager.userGameJumpAudioIdList.Clear();
        Manager.instance.gameDataManager.userGameBGAudioIdList.Clear();
        Manager.instance.gameDataManager.userGameGameOverAudioIdList.Clear();
        Manager.instance.gameDataManager.userGameNameList.Clear();
        Manager.instance.gameDataManager.userGamePlayCountList.Clear();
    }


    public IEnumerator StoreTournamentData(int userId, int gameId, string tournamentName, string tournamentHostName,
                                           string socialLink, float playerJoiningFee, int startDate, int startTime, int endDate,
                                           int endTime, float prizePool, int status, int playCount, int userCount)
    {
        yield return StoreTournamentDataCoroutine(userId, gameId, tournamentName, tournamentHostName,
                                     socialLink, playerJoiningFee, startDate, startTime, endDate,
                                     endTime, prizePool, status, playCount, userCount);
    }

    public IEnumerator StoreTournamentDataCoroutine(int userId, int gameId, string tournamentName, string tournamentHostName,
                                                    string socialLink, float playerJoiningFee, int startDate, int startTime, int endDate, 
                                                    int endTime, float prizePool, int status, int playCount, int userCount)
    {



        //Debug.Log($"Sending Tournament Data: userId={userId}, gameId={gameId}, tournamentName={tournamentName}, " +
        //      $"tournamentHostName={tournamentHostName}, socialLink={socialLink}, playerJoiningFee={playerJoiningFee}, startDate={startDate}, " +
        //      $"startTime={startTime}, endDate={endDate}, endTime={endTime}, prizePool={prizePool}, status={status}, " +
        //      $"playCount={playCount}, userCount={userCount}");


        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        form.AddField("gameId", gameId);
        form.AddField("tournamentName", tournamentName);
        form.AddField("tournamentHostName", tournamentHostName);
        form.AddField("socialLink", socialLink);
        form.AddField("playerJoiningFee", playerJoiningFee.ToString()); // Convert float to string for form submission
        form.AddField("startDate", startDate);
        form.AddField("startTime", startTime);
        form.AddField("endDate", endDate);
        form.AddField("endTime", endTime);
        form.AddField("prizePool", prizePool.ToString());  // Convert float to string for form submission
        form.AddField("status", status);
        form.AddField("playCount", playCount);
        form.AddField("userCount", userCount);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(storeTournamentUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Deserialize the JSON response
                TournamentResponse responseData = JsonUtility.FromJson<TournamentResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    Debug.Log("Tournament data stored successfully! Tournament ID: " + responseData.tournamentId);

                    Manager.instance.tournamentDataManager.tournamentId = responseData.tournamentId;
                    StartCoroutine(Manager.instance.canvasManager.CreateOnChainTournament());
                }
                else
                {
                    Debug.LogError("Failed to store tournament data: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Failed to store tournament data: " + responseData.message);

                }
            }
        }
    }


    public IEnumerator GetUserTournaments(int userId)
    {

        yield return StartCoroutine(GetUserTournamentsCoroutine(userId));

    }

    public IEnumerator GetUserTournamentsCoroutine(int userId)
    {
        // Create a form and add the userId field
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(getUserTournamentsUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Deserialize the JSON response
                UserTournamentsResponse responseData = JsonUtility.FromJson<UserTournamentsResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    ClearUserTournamentLists();


                    if (responseData.tournaments.Count == 0)
                    {
                        Debug.Log("No tournaments found.");
                        // Handle the empty list scenario here, if needed
                    }
                    else
                    {
                        foreach (var tournament in responseData.tournaments)
                        {

                            Manager.instance.tournamentDataManager.userTournamentIdList.Add(tournament.tournamentId);
                            Manager.instance.tournamentDataManager.userGameIdList.Add(tournament.gameId);
                            Manager.instance.tournamentDataManager.userTournamentNameList.Add(tournament.tournamentName);
                            Manager.instance.tournamentDataManager.userTournamentHostNameList.Add(tournament.tournamentHostName);
                            Manager.instance.tournamentDataManager.userSocialLinkList.Add(tournament.socialLink);
                            Manager.instance.tournamentDataManager.userPlayerJoiningFeeList.Add(tournament.playerJoiningFee);
                            Manager.instance.tournamentDataManager.userStartDateList.Add(tournament.startDate);
                            Manager.instance.tournamentDataManager.userStartTimeList.Add(tournament.startTime);
                            Manager.instance.tournamentDataManager.userEndDateList.Add(tournament.endDate);
                            Manager.instance.tournamentDataManager.userEndTimeList.Add(tournament.endTime);
                            Manager.instance.tournamentDataManager.userPrizePoolList.Add(tournament.prizePool);
                            Manager.instance.tournamentDataManager.userStatusList.Add(tournament.status);
                            Manager.instance.tournamentDataManager.userPlayCountList.Add(tournament.playCount);
                            Manager.instance.tournamentDataManager.userUserCountList.Add(tournament.userCount);
                            Manager.instance.tournamentDataManager.userWinnerIdList.Add(tournament.winnerId);
                            Manager.instance.tournamentDataManager.userRunnerUpIdList.Add(tournament.runnerUpId);
                            Manager.instance.tournamentDataManager.userSecondRunnerUpIdList.Add(tournament.secondRunnerUpId);

                        }
                    }


                    
                }
                else
                {
                    Debug.LogError("Failed to fetch tournaments: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Failed to fetch tournaments: " + responseData.message);

                }
            }
        }
    }

    private void ClearUserTournamentLists()
    {
        Manager.instance.tournamentDataManager.userTournamentIdList.Clear();
        Manager.instance.tournamentDataManager.userGameIdList.Clear();
        Manager.instance.tournamentDataManager.userTournamentNameList.Clear();
        Manager.instance.tournamentDataManager.userTournamentHostNameList.Clear();
        Manager.instance.tournamentDataManager.userSocialLinkList.Clear();
        Manager.instance.tournamentDataManager.userPlayerJoiningFeeList.Clear();
        Manager.instance.tournamentDataManager.userStartDateList.Clear();
        Manager.instance.tournamentDataManager.userStartTimeList.Clear();
        Manager.instance.tournamentDataManager.userEndDateList.Clear();
        Manager.instance.tournamentDataManager.userEndTimeList.Clear();
        Manager.instance.tournamentDataManager.userPrizePoolList.Clear();
        Manager.instance.tournamentDataManager.userStatusList.Clear();
        Manager.instance.tournamentDataManager.userPlayCountList.Clear();
        Manager.instance.tournamentDataManager.userUserCountList.Clear();
        Manager.instance.tournamentDataManager.userWinnerIdList.Clear();
        Manager.instance.tournamentDataManager.userRunnerUpIdList.Clear();
        Manager.instance.tournamentDataManager.userSecondRunnerUpIdList.Clear();
    }


    public IEnumerator GetTop30Games()
    {

        yield return StartCoroutine(GetTop30GamesCoroutine());

    }

    public IEnumerator GetTop30GamesCoroutine()
    {
        // Send a GET request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Get(getTop30GamesUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Deserialize the JSON response
                TopGamesResponse responseData = JsonUtility.FromJson<TopGamesResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {

                    // Clear the existing lists
                    ClearTop30GameLists();

                    if (responseData.games.Count == 0)
                    {
                        Debug.Log("No games found.");
                        // Handle the empty list scenario here, if needed
                    }
                    else
                    {
                        foreach (var game in responseData.games)
                        {
                            Manager.instance.gameDataManager.top30GameIdList.Add(game.gameId);
                            Manager.instance.gameDataManager.top30UserIdList.Add(game.userId);
                            Manager.instance.gameDataManager.top30GameTemplateIdList.Add(game.templateId);
                            Manager.instance.gameDataManager.top30GameFaceIdList.Add(game.faceId);
                            Manager.instance.gameDataManager.top30GameBackgroundIdList.Add(game.backgroundId);
                            Manager.instance.gameDataManager.top30GameJumpAudioIdList.Add(game.jumpAudioId);
                            Manager.instance.gameDataManager.top30GameBGAudioIdList.Add(game.backgroundAudioId);
                            Manager.instance.gameDataManager.top30GameGameOverAudioIdList.Add(game.gameOverAudioId);
                            Manager.instance.gameDataManager.top30GameNameList.Add(game.gameName);
                            Manager.instance.gameDataManager.top30GamePlayCountList.Add(game.playCount);
                        }
                    }

                }
                else
                {
                    Debug.LogError("Failed to fetch games: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Failed to fetch games: " + responseData.message);

                }
            }
        }
    }

    // Clear all game-related lists
    private void ClearTop30GameLists()
    {
        Manager.instance.gameDataManager.top30GameIdList.Clear();
        Manager.instance.gameDataManager.top30UserIdList.Clear();
        Manager.instance.gameDataManager.top30GameTemplateIdList.Clear();
        Manager.instance.gameDataManager.top30GameFaceIdList.Clear();
        Manager.instance.gameDataManager.top30GameBackgroundIdList.Clear();
        Manager.instance.gameDataManager.top30GameJumpAudioIdList.Clear();
        Manager.instance.gameDataManager.top30GameBGAudioIdList.Clear();
        Manager.instance.gameDataManager.top30GameGameOverAudioIdList.Clear();
        Manager.instance.gameDataManager.top30GameNameList.Clear();
        Manager.instance.gameDataManager.top30GamePlayCountList.Clear();
    }


    public IEnumerator GetAllTournaments()
    {

        yield return StartCoroutine(GetAllTournamentsCoroutine());

    }

    public IEnumerator GetAllTournamentsCoroutine()
    {
        // Send a GET request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Get(getAllTournamentsUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Deserialize the JSON response
                AllTournamentsResponse responseData = JsonUtility.FromJson<AllTournamentsResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {

                    ClearAllTournamentLists();

                    if (responseData.tournaments.Count == 0)
                    {
                        Debug.Log("No tournaments found.");
                        // Handle the empty list scenario here, if needed
                    }
                    else
                    {
                        foreach (var tournament in responseData.tournaments)
                        {

                            Manager.instance.tournamentDataManager.allTournamentIdList.Add(tournament.tournamentId);
                            Manager.instance.tournamentDataManager.allUserIdList.Add(tournament.userId);
                            Manager.instance.tournamentDataManager.allGameIdList.Add(tournament.gameId);
                            Manager.instance.tournamentDataManager.allTournamentNameList.Add(tournament.tournamentName);
                            Manager.instance.tournamentDataManager.allTournamentHostNameList.Add(tournament.tournamentHostName);
                            Manager.instance.tournamentDataManager.allSocialLinkList.Add(tournament.socialLink);
                            Manager.instance.tournamentDataManager.allPlayerJoiningFeeList.Add(tournament.playerJoiningFee);
                            Manager.instance.tournamentDataManager.allStartDateList.Add(tournament.startDate);
                            Manager.instance.tournamentDataManager.allStartTimeList.Add(tournament.startTime);
                            Manager.instance.tournamentDataManager.allEndDateList.Add(tournament.endDate);
                            Manager.instance.tournamentDataManager.allEndTimeList.Add(tournament.endTime);
                            Manager.instance.tournamentDataManager.allPrizePoolList.Add(tournament.prizePool);
                            Manager.instance.tournamentDataManager.allStatusList.Add(tournament.status);
                            Manager.instance.tournamentDataManager.allPlayCountList.Add(tournament.playCount);
                            Manager.instance.tournamentDataManager.allUserCountList.Add(tournament.userCount);
                            Manager.instance.tournamentDataManager.allWinnerIdList.Add(tournament.winnerId);
                            Manager.instance.tournamentDataManager.allRunnerUpIdList.Add(tournament.runnerUpId);
                            Manager.instance.tournamentDataManager.allSecondRunnerUpIdList.Add(tournament.secondRunnerUpId);

                        }
                    }
                    
                }
                else
                {
                    Debug.LogError("Failed to fetch tournaments: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Failed to fetch tournaments: " + responseData.message);

                }
            }
        }
    }


    private void ClearAllTournamentLists()
    {
        Manager.instance.tournamentDataManager.allTournamentIdList.Clear();
        Manager.instance.tournamentDataManager.allUserIdList.Clear();
        Manager.instance.tournamentDataManager.allGameIdList.Clear();
        Manager.instance.tournamentDataManager.allTournamentNameList.Clear();
        Manager.instance.tournamentDataManager.allTournamentHostNameList.Clear();
        Manager.instance.tournamentDataManager.allSocialLinkList.Clear();
        Manager.instance.tournamentDataManager.allPlayerJoiningFeeList.Clear();
        Manager.instance.tournamentDataManager.allStartDateList.Clear();
        Manager.instance.tournamentDataManager.allStartTimeList.Clear();
        Manager.instance.tournamentDataManager.allEndDateList.Clear();
        Manager.instance.tournamentDataManager.allEndTimeList.Clear();
        Manager.instance.tournamentDataManager.allPrizePoolList.Clear();
        Manager.instance.tournamentDataManager.allStatusList.Clear();
        Manager.instance.tournamentDataManager.allPlayCountList.Clear();
        Manager.instance.tournamentDataManager.allUserCountList.Clear();
        Manager.instance.tournamentDataManager.allWinnerIdList.Clear();
        Manager.instance.tournamentDataManager.allRunnerUpIdList.Clear();
        Manager.instance.tournamentDataManager.allSecondRunnerUpIdList.Clear();
    }


    public IEnumerator GetUserJoinedTournamentDetails(int userId)
    {

        yield return StartCoroutine(GetUserJoinedTournamentDetailsCoroutine(userId));

    }



    public IEnumerator GetUserJoinedTournamentDetailsCoroutine(int userId)
    {
        // Create a form and add the userId field
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(getUserJoinedTournamentDetailsUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Deserialize the JSON response
                TournamentDetailsResponse responseData = JsonUtility.FromJson<TournamentDetailsResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {

                    ClearAllJoinedTournamentLists();


                    if (responseData.tournaments.Count == 0)
                    {
                        Debug.Log("No tournaments found for this user.");
                        // Handle the empty list scenario here, if needed
                    }
                    else
                    {
                        foreach (var tournament in responseData.tournaments)
                        {

                            Manager.instance.tournamentDataManager.joinedTournamentIdList.Add(tournament.tournamentId);
                            Manager.instance.tournamentDataManager.joinedGameIdList.Add(tournament.gameId);
                            Manager.instance.tournamentDataManager.joinedTournamentNameList.Add(tournament.tournamentName);
                            Manager.instance.tournamentDataManager.joinedTournamentHostNameList.Add(tournament.tournamentHostName);
                            Manager.instance.tournamentDataManager.joinedSocialLinkList.Add(tournament.socialLink);
                            Manager.instance.tournamentDataManager.joinedPlayerJoiningFeeList.Add(tournament.playerJoiningFee);
                            Manager.instance.tournamentDataManager.joinedStartDateList.Add(tournament.startDate);
                            Manager.instance.tournamentDataManager.joinedStartTimeList.Add(tournament.startTime);
                            Manager.instance.tournamentDataManager.joinedEndDateList.Add(tournament.endDate);
                            Manager.instance.tournamentDataManager.joinedEndTimeList.Add(tournament.endTime);
                            Manager.instance.tournamentDataManager.joinedPrizePoolList.Add(tournament.prizePool);
                            Manager.instance.tournamentDataManager.joinedStatusList.Add(tournament.status);
                            Manager.instance.tournamentDataManager.joinedPlayCountList.Add(tournament.playCount);
                            Manager.instance.tournamentDataManager.joinedUserCountList.Add(tournament.userCount);
                            Manager.instance.tournamentDataManager.joinedWinnerIdList.Add(tournament.winnerId);
                            Manager.instance.tournamentDataManager.joinedRunnerUpIdList.Add(tournament.runnerUpId);
                            Manager.instance.tournamentDataManager.joinedSecondRunnerUpIdList.Add(tournament.secondRunnerUpId);
                            Manager.instance.tournamentDataManager.joinedScoreList.Add(tournament.score);

                        }
                    }

                }
                else
                {
                    Debug.LogError("Failed to fetch tournament details: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Failed to fetch tournament details: " + responseData.message);

                }
            }
        }
    }


    private void ClearAllJoinedTournamentLists()
    {
        Manager.instance.tournamentDataManager.joinedTournamentIdList.Clear();
        Manager.instance.tournamentDataManager.joinedGameIdList.Clear();
        Manager.instance.tournamentDataManager.joinedTournamentNameList.Clear();
        Manager.instance.tournamentDataManager.joinedTournamentHostNameList.Clear();
        Manager.instance.tournamentDataManager.joinedSocialLinkList.Clear();
        Manager.instance.tournamentDataManager.joinedPlayerJoiningFeeList.Clear();
        Manager.instance.tournamentDataManager.joinedStartDateList.Clear();
        Manager.instance.tournamentDataManager.joinedStartTimeList.Clear();
        Manager.instance.tournamentDataManager.joinedEndDateList.Clear();
        Manager.instance.tournamentDataManager.joinedEndTimeList.Clear();
        Manager.instance.tournamentDataManager.joinedPrizePoolList.Clear();
        Manager.instance.tournamentDataManager.joinedStatusList.Clear();
        Manager.instance.tournamentDataManager.joinedPlayCountList.Clear();
        Manager.instance.tournamentDataManager.joinedUserCountList.Clear();
        Manager.instance.tournamentDataManager.joinedWinnerIdList.Clear();
        Manager.instance.tournamentDataManager.joinedRunnerUpIdList.Clear();
        Manager.instance.tournamentDataManager.joinedSecondRunnerUpIdList.Clear();
        Manager.instance.tournamentDataManager.joinedScoreList.Clear();
    }


    public IEnumerator GetTournamentScores(int tournamentId, bool isLive)
    {
        yield return StartCoroutine(GetTournamentScoresCoroutine(tournamentId, isLive));
    }

    public IEnumerator GetTournamentScoresCoroutine(int tournamentId, bool isLive)
    {
        // Create a form and add the tournamentId field
        WWWForm form = new WWWForm();
        form.AddField("tournamentId", tournamentId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(getTournamentScoresUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Deserialize the JSON response
                TournamentScoresResponse responseData = JsonUtility.FromJson<TournamentScoresResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {

                    ClearLiveTournamentLeaderboardLists();

                    if (responseData.tournamentScores.Count == 0)
                    {
                        Debug.Log("No scores found for this tournament.");
                    }
                    else
                    {
                        if (isLive)
                        {

                            foreach (var scoreData in responseData.tournamentScores)
                            {
                                Manager.instance.canvasManager.liveTournamentLeaderboardUserIdList.Add(scoreData.userId);
                                Manager.instance.canvasManager.liveTournamentLeaderboardUsernameList.Add(scoreData.username);
                                Manager.instance.canvasManager.liveTournamentLeaderboardUserScoreList.Add(scoreData.score);

                            }

                        }
                        else
                        {
                            foreach (var scoreData in responseData.tournamentScores)
                            {
                                Manager.instance.canvasManager.pastTournamentLeaderboardUserIdList.Add(scoreData.userId);
                                Manager.instance.canvasManager.pastTournamentLeaderboardUsernameList.Add(scoreData.username);
                                Manager.instance.canvasManager.pastTournamentLeaderboardUserScoreList.Add(scoreData.score);

                            }

                        }
                    }
                }
                else
                {
                    Debug.LogError("Failed to fetch tournament scores: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Failed to fetch tournament scores: " + responseData.message);

                }
            }
        }
    }

    private void ClearLiveTournamentLeaderboardLists()
    {
        Manager.instance.canvasManager.liveTournamentLeaderboardUserIdList.Clear();
        Manager.instance.canvasManager.liveTournamentLeaderboardUsernameList.Clear();
        Manager.instance.canvasManager.liveTournamentLeaderboardUserScoreList.Clear();
    }

    //public IEnumerator StoreTournamentScore(int userId, int tournamentId, int score)
    //{
    //    yield return StartCoroutine(StoreTournamentScoreCoroutine(userId, tournamentId, score));
    //}
    public IEnumerator StoreTournamentScoreCoroutine(int userId, int tournamentId, int score)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        form.AddField("tournamentId", tournamentId);
        form.AddField("score", score);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(storeTournamentScoreUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Handle the response (optional)
                ScoreResponse responseData = JsonUtility.FromJson<ScoreResponse>(responseText);

                if (responseData.status == "success")
                {
                    Debug.Log("Score successfully stored/updated.");
                }
                else
                {
                    Debug.LogError("Failed to store/update score: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Failed to store/update score: " + responseData.message);

                }
            }
        }
    }


    public IEnumerator GetGameTemplateId(int gameId)
    {
        yield return StartCoroutine(GetGameTemplateIdCoroutine(gameId));
    }

    public IEnumerator GetGameTemplateIdCoroutine(int gameId)
    {
        // Create a form and add the gameId
        WWWForm form = new WWWForm();
        form.AddField("gameId", gameId);

        Debug.Log("Game ID: " + gameId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(getGameTemplateIdUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Deserialize the JSON response
                GameTemplateIdResponse responseData = JsonUtility.FromJson<GameTemplateIdResponse>(responseText);

                if (responseData.status == "success")
                {
                    Debug.Log("Template ID: " + responseData.templateId);


                    Manager.instance.tournamentDataManager.gameTemplateId = responseData.templateId;

                    


                    

                }
                else
                {
                    Debug.LogError("Error: " + responseData.message + " for GameId: " + gameId);
                    Manager.instance.canvasManager.ShowErrorPopup("Error: " + responseData.message + " for GameId: " + gameId);

                }
            }
        }
    }




    public IEnumerator GetOrCreateGameScoreCoroutine(int userId, int gameId)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        form.AddField("gameId", gameId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(getOrCreateGameScoreUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Parse the JSON response
                UserGameScoreResponse responseData = JsonUtility.FromJson<UserGameScoreResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    Debug.Log("Score: " + responseData.score);
                    Manager.instance.userInfoManager.gameScore = responseData.score;
                    yield return new WaitForSeconds(2f);

                }
                else
                {
                    Debug.LogError("Error fetching or creating score: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Error fetching or creating score: " + responseData.message);

                    yield return new WaitForSeconds(2f);

                }
            }
        }
    }


    

    public IEnumerator UpdateGameScoreCoroutine(int userId, int gameId, int additionalScore)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        form.AddField("gameId", gameId);
        form.AddField("score", additionalScore);


        Debug.Log("======================> Additional Score: " + additionalScore);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(updateGameScoreUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

                yield return new WaitForSeconds(2f);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Parse the JSON response
                AddGameScoreResponse responseData = JsonUtility.FromJson<AddGameScoreResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    Debug.Log("New Score: " + responseData.newScore);
                    Manager.instance.userInfoManager.gameScore = responseData.newScore;

                    yield return new WaitForSeconds(2f);
                }
                else
                {
                    Debug.LogError("Error adding score: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Error adding score: " + responseData.message);

                    yield return new WaitForSeconds(2f);

                }
            }
        }
    }


    public IEnumerator IncrementGamePlayCountCoroutine(int gameId)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("gameId", gameId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(incrementGamePlayCountUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Parse the JSON response
                GamePlayCountResponse responseData = JsonUtility.FromJson<GamePlayCountResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    Debug.Log("New Play Count: " + responseData.newPlayCount);
                }
                else
                {
                    Debug.LogError("Error updating play count: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Error updating play count: " + responseData.message);

                }
            }
        }
    }

    public IEnumerator GetGameDetailsCoroutine(int gameId)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("gameId", gameId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(getGameDetailsUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Parse the JSON response
                GameDetailsResponse responseData = JsonUtility.FromJson<GameDetailsResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    Debug.Log("Game Name: " + responseData.gameName);

                    Manager.instance.gameDataManager.gameId = gameId;
                    Manager.instance.gameDataManager.gameTemplateId = responseData.templateId;
                    Manager.instance.gameDataManager.gameFaceId = responseData.faceId;
                    Manager.instance.gameDataManager.gameBackgroundId = responseData.backgroundId;
                    Manager.instance.gameDataManager.gameJumpAudioId = responseData.jumpAudioId;
                    Manager.instance.gameDataManager.gameBGAudioId = responseData.backgroundAudioId;
                    Manager.instance.gameDataManager.gameGameOverAudioId = responseData.gameOverAudioId;
                    Manager.instance.gameDataManager.gameGameName = responseData.gameName;
                    


                }
                else
                {
                    Debug.LogError("Error: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Error: " + responseData.message);

                }
            }
        }
    }

    public IEnumerator GetOrCreateTournamentScoreCoroutine(int userId, int tournamentId)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        form.AddField("tournamentId", tournamentId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(getOrCreateTournamentScoreUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Parse the JSON response
                TournamentScoreResponse responseData = JsonUtility.FromJson<TournamentScoreResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    Debug.Log("Tournament Score: " + responseData.score);
                    Manager.instance.userInfoManager.tournamentScore = responseData.score;
                    yield return new WaitForSeconds(2f);
                }
                else
                {
                    Debug.LogError("Error: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Error: " + responseData.message);

                    yield return new WaitForSeconds(2f);
                }
            }
        }
    }


    public IEnumerator IncrementTournamentPlayCountCoroutine(int tournamentId)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("tournamentId", tournamentId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(incrementTournamentPlayCountUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Parse the JSON response
                TournamentPlayCountResponse responseData = JsonUtility.FromJson<TournamentPlayCountResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    Debug.Log("New Play Count: " + responseData.newPlayCount);
                }
                else
                {
                    Debug.LogError("Error: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Error: " + responseData.message);

                }
            }
        }
    }


    public IEnumerator IncrementTournamentUserCountCoroutine(int tournamentId)
    {
        // Create a form and add the necessary fields
        WWWForm form = new WWWForm();
        form.AddField("tournamentId", tournamentId);

        // Send a POST request to the PHP script
        using (UnityWebRequest www = UnityWebRequest.Post(incrementTournamentUserCountUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error); // Log any error messages
                Manager.instance.canvasManager.ShowErrorPopup("Error: " + www.error);

            }
            else
            {
                // Print the server's response
                string responseText = www.downloadHandler.text;
                Debug.Log("Response from server: " + responseText);

                // Parse the JSON response
                TournamentUserCountJoiningFeeResponse responseData = JsonUtility.FromJson<TournamentUserCountJoiningFeeResponse>(responseText);

                // Check if the status is "success"
                if (responseData.status == "success")
                {
                    Debug.Log("New Play Count: " + responseData.newPlayCount);
                    Debug.Log("New Prize Pool: " + responseData.newPrizePool);
                }
                else
                {
                    Debug.LogError("Error: " + responseData.message);
                    Manager.instance.canvasManager.ShowErrorPopup("Error: " + responseData.message);

                }
            }
        }
    }


}




// Class to map the JSON response from the server
[System.Serializable]
public class RegisterUserResponse
{
    public string status;     // success or error
    public string message;    // Error message or success message
    public int userId;        // User ID in case of success
}


// Class to map the JSON response from the server
[System.Serializable]
public class SignInResponseData
{
    public string status;        // success or error
    public int userId;           // The user's ID
    public string username;      // The user's username
    public string email;         // The user's email
    public string walletAddress; // The user's wallet address
    public string walletMnemonics; // Decrypted wallet mnemonics (can be null)
    public string message;       // Error or success message
}

// Class to map the JSON response from the server
[System.Serializable]
public class UpdateWalletResponse
{
    public string status;     // success or error
    public string message;    // Error or success message
}

// Class to map the JSON response from the server
[System.Serializable]
public class TournamentManagerResponse
{
    public string status;    // success or error
    public string tournamentManagerAddress; // The address string
    public string contractName; // The contract name string
    public string message;   // Error message, if any
}


// Class to map the JSON response from the server
[System.Serializable]
public class StoreGameResponse
{
    public string status;     // success or error
    public string message;    // Error message or success message
    public int gameId;        // Game ID in case of success
}

// Class to map the JSON response from the server
[System.Serializable]
public class UserGamesResponse
{
    public string status;     // success or error
    public string message;    // Error message (if any)
    public List<UserGame> games; // List of games
}

[System.Serializable]
public class UserGame
{
    public int gameId;
    public int userId;
    public int templateId;
    public int faceId;
    public int backgroundId;
    public int jumpAudioId;
    public int backgroundAudioId;
    public int gameOverAudioId;
    public string gameName;
    public int playCount;
}

// Class to map the JSON response from the server
[System.Serializable]
public class TournamentResponse
{
    public string status;     // success or error
    public string message;    // Error message or success message
    public int tournamentId;  // Tournament ID in case of success
}

[System.Serializable]
public class UserTournamentsResponse
{
    public string status;    // success or error
    public string message;   // Error message, if any
    public List<Tournament> tournaments; // List of tournaments
}

[System.Serializable]
public class Tournament
{
    public int tournamentId;
    public int userId;
    public int gameId;
    public string tournamentName;
    public string tournamentHostName;
    public string socialLink;
    public float playerJoiningFee;
    public int startDate;
    public int startTime;
    public int endDate;
    public int endTime;
    public float prizePool;
    public int status;
    public int playCount;
    public int userCount;
    public int createdAt;
    public float winnerId;
    public float runnerUpId;
    public float secondRunnerUpId;
}

// Class to map the JSON response from the server
[System.Serializable]
public class TopGamesResponse
{
    public string status;    // success or error
    public string message;   // Error message, if any
    public List<UserGame> games; // List of games
}

// Class to map the JSON response from the server
[System.Serializable]
public class AllTournamentsResponse
{
    public string status;    // success or error
    public string message;   // Error message, if any
    public List<Tournament> tournaments; // List of tournaments
}

// Class to map the JSON response from the server
[System.Serializable]
public class TournamentDetailsResponse
{
    public string status;    // success or error
    public string message;   // Error message, if any
    public List<JoinedTournamentDetails> tournaments; // List of tournaments with score
}

[System.Serializable]
public class JoinedTournamentDetails
{
    public int tournamentId;
    public int gameId;
    public string tournamentName;
    public string tournamentHostName;
    public string socialLink;
    public float playerJoiningFee;
    public int startDate;
    public int startTime;
    public int endDate;
    public int endTime;
    public float prizePool;
    public int status;
    public int playCount;
    public int userCount;
    public float winnerId;
    public float runnerUpId;
    public float secondRunnerUpId;
    public int score;
}


// Class to map the JSON response from the server
[System.Serializable]
public class TournamentScoresResponse
{
    public string status;    // success or error
    public string message;   // Error message, if any
    public List<ScoreData> tournamentScores; // List of userId, username, and score data
}

[System.Serializable]
public class ScoreData
{
    public int userId;
    public string username;
    public int score;
}

// Class to map the JSON response from the server
[System.Serializable]
public class ScoreResponse
{
    public string status;    // success or error
    public string message;   // Message from the server
}

// Class to map the JSON response from the server
[System.Serializable]
public class GameTemplateIdResponse
{
    public string status;
    public int templateId;
    public string message;
}

// Class to map the JSON response from the server
[System.Serializable]
public class UserGameScoreResponse
{
    public string status;     // success or error
    public string message;    // Error message if any
    public int score;         // Score in case of success
}

// Class to map the JSON response from the server
[System.Serializable]
public class AddGameScoreResponse
{
    public string status;     // success or error
    public string message;    // Error message if any
    public int newScore;      // The new score after addition
}

// Class to map the JSON response from the server
[System.Serializable]
public class GamePlayCountResponse
{
    public string status;     // success or error
    public string message;    // Error message if any
    public int newPlayCount;  // The updated play count
}

// Class to map the JSON response from the server
[System.Serializable]
public class GameDetailsResponse
{
    public string status;        // success or error
    public string message;       // Error message, if any
    public int templateId;       // Template ID
    public int faceId;           // Face ID
    public int backgroundId;     // Background ID
    public int jumpAudioId;      // Jump Audio ID
    public int backgroundAudioId; // Background Audio ID
    public int gameOverAudioId;  // Game Over Audio ID
    public string gameName;      // Game Name
}

// Class to map the JSON response from the server
[System.Serializable]
public class TournamentScoreResponse
{
    public string status;   // success or error
    public string message;  // Error message if any
    public int score;       // The score of the user in the tournament
}


// Class to map the JSON response from the server
[System.Serializable]
public class TournamentPlayCountResponse
{
    public string status;     // success or error
    public string message;    // Error message, if any
    public int newPlayCount;  // The updated play count
}


// Class to map the JSON response from the server
[System.Serializable]
public class TournamentUserCountJoiningFeeResponse
{
    public string status;      // success or error
    public string message;     // Error message if any
    public int newPlayCount;   // The updated play count
    public float newPrizePool; // The updated prize pool
}