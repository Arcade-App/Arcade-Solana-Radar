using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class Items : MonoBehaviour
{

    Action<string> _createItemsCallback;

    // Start is called before the first frame update
    void Start()
    {
        _createItemsCallback = (jsonArrayString) =>
        {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateItems()
    {
        string userId = Main.instance.userInfo.userID;
        StartCoroutine(Main.instance.web.GetItemsIDs(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        //Parsing json array as an array
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            //Create local variables
            bool isDone = false;    //are we done downloading?
            string itemId = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();

            //Create a callbakck to get the info from Web.cs
            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.instance.web.GetItem(itemId, getItemInfoCallback));

            //Wait until the callback is called from web (info finish downloading)
            yield return new WaitUntil(() => isDone = true);

            //Instantiate GameObject (item prefab)
            GameObject item = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            //Fill information
            item.transform.Find("Name Text (TMP)").GetComponent<TMP_Text>().text = itemInfoJson["name"];
            item.transform.Find("Price Text (TMP)").GetComponent<TMP_Text>().text = itemInfoJson["price"];
            item.transform.Find("Description Text (TMP)").GetComponent<TMP_Text>().text = itemInfoJson["description"];


            //Continue to the next item
        }


    }

}
