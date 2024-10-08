using Aptos.Unity.Rest.Model;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Aptos.Unity.Rest
{
    /// <summary>
    /// Faucet Client for claiming APT from Devnet
    /// NOTE: Does not work on Testnet. Testnet only supports airdrops through an authenticated URL
    /// </summary>
    public class FaucetClient: MonoBehaviour
    {
        public static FaucetClient Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Funds a Testnet Account
        /// </summary>
        /// <param name="callback">Callback function used when response is received.</param>
        /// <param name="address">Address that will get funded.</param>
        /// <param name="amount">Amount of APT requested.</param>
        /// <param name="endpoint">Base URL for faucet.</param>
        /// <returns>Calls <c>callback</c> function with <c>(bool, ResponsiveInfo)</c>: \n
        /// A boolean stating that the request for funding was successful, and an object containg the response details</returns>
        public IEnumerator FundAccount(Action<bool, ResponseInfo> callback, string address, int amount, string endpoint)
        {

            Debug.Log("Debug 1: Inside Fund Account");

            Debug.Log("Debug 1.5: Endpoint : " + endpoint);
            

            string faucetURL = endpoint + "/mint?amount=" + amount + "&address=" + address;

            Debug.Log("Debug 2: Faucet URL: " + faucetURL);


            Uri transactionsURI = new Uri(faucetURL);

            Debug.Log("Debug 3: Transaction URL: " + transactionsURI);


            var request = new UnityWebRequest(transactionsURI, "POST");
            request.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("Debug 4: Request: " + request);

            ResponseInfo responseInfo = new ResponseInfo();

            request.SendWebRequest();
            while (!request.isDone)
            {
                yield return null;
            }

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                responseInfo.status = ResponseInfo.Status.Failed;
                responseInfo.message = request.error;
                callback(false, responseInfo);
            }
            else if (request.responseCode == 404)
            {
                responseInfo.status = ResponseInfo.Status.NotFound;
                responseInfo.message = request.error;
                callback(false, responseInfo);
            }
            else if (request.responseCode >= 400)
            {
                responseInfo.status = ResponseInfo.Status.Failed;
                responseInfo.message = request.error;
                callback(false, responseInfo);
            }
            else
            {
                responseInfo.status = ResponseInfo.Status.Success;
                responseInfo.message = "Funding succeeded!";
                callback(true, responseInfo);
                Debug.Log("Debug 5: Success Request Result " + responseInfo);

            }

            Debug.Log("Debug 5.1: Request Result " + responseInfo);

            request.Dispose();
            yield return new WaitForSeconds(1f);
        }
    }
}