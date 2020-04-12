using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

namespace PSTGU.ServerCommunication
{
    [Serializable]
    [CreateAssetMenu(fileName = "ServerSettings")]
    public class ServerSettings : ScriptableObject
    {
        private static ServerSettings instance;

        public static ServerSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<ServerSettings>("PSTGU/ServerSettings");
                }

                return instance;
            }
        }

        [Header("General")]

        public float RequestTimeout = 30;

        [Header("Server")]

        [SerializeField]
        private string testServerUrl = "http://95.165.144.255:8344/NMBook/api/nm/";

        [SerializeField]
        private string productionServerUrl = "http://95.165.144.255:8344/NMBook/api/";

        [SerializeField]
        private bool useTestServer = true;

        public string ServerURL { get => useTestServer ? testServerUrl : productionServerUrl; }

        public string AuthToken = "&token=nm19nav";

        [Header("File Server")]

        [SerializeField]
        private string testFileServerUrl = "http://95.165.144.255:8344/NMBook";

        [SerializeField]
        private string productionFileServerUrl = "http://95.165.144.255:8344/NMBook";

        [SerializeField]
        private bool useTestFileServer = true;

        public string FileServerURL { get => useTestFileServer ? testFileServerUrl : productionFileServerUrl; }

        public string AuthTokenFile = "?token=nm19nav";

        [Header("Debug")]

        public bool LogResponseHeaders = false;

        public bool LogResponseBody = false;
    }
}