//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using PSTGU.ServerCommunication;

//public class TestSearchResponse : MonoBehaviour
//{
//    public string Query = "Москва";

//    IEnumerator Start()
//    {
//        Debug.Log("Send Request");

//        SearchResponse response = null;

//        yield return ManagerServerCommunication.Search(Query, (res) => response = res, 0, 10);

//        if (response == null)
//        {
//            Debug.Log("Response is null");
//        }

//        if (response.IsError)
//        {
//            Debug.Log("Response Is Error");
//        }
//    }
//}