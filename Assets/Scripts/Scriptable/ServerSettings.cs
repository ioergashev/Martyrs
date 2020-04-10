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
        private static ServerSettings _instance;

        private static ServerSettings instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ServerSettings>("PSTGU/ServerSettings");
                }

                return _instance;
            }
        }

        [SerializeField]
        private string testServerUrl = "http://95.165.144.255:8344/NMBook/api/nm/";
        public static string TestServerUrl { get => instance.testServerUrl; }

        [SerializeField]
        private string productionServerUrl = "http://95.165.144.255:8344/NMBook/api/";
        public static string ProductionServerUrl { get => instance.productionServerUrl; }

        [SerializeField]
        private bool useTestServer;
        public static bool UseTestServer { get => instance.useTestServer; }

        public static string ServerURL { get => UseTestServer ? TestServerUrl : ProductionServerUrl; }

        [SerializeField]
        private float requestTimeout = 30;
        public static float RequestTimeout { get => instance.requestTimeout; }

        [SerializeField]
        private string authToken = "&token=nm19nav";
        public static string AuthToken { get => instance.authToken; }

        [SerializeField]
        private bool logResponseHeaders = false;
        public static bool LogResponseHeaders { get => instance.logResponseHeaders; }

        [SerializeField]
        private bool logResponseBody = true;

        public static bool LogResponseBody { get => instance.logResponseBody; }

        [SerializeField]
        private string testFileServerUrl = "http://95.165.144.255:8344/NMBook";
        public static string TestFileServerUrl { get => instance.testFileServerUrl; }

        [SerializeField]
        private string productionFileServerUrl = "http://95.165.144.255:8344/NMBook";
        public static string ProductionFileServerUrl { get => instance.productionFileServerUrl; }

        [SerializeField]
        private bool useTestFileServer;
        public static bool UseTestFileServer { get => instance.useTestFileServer; }

        public static string FileServerURL { get => UseTestFileServer ? TestFileServerUrl : ProductionFileServerUrl; }

        [SerializeField]
        private string authTokenFile = "?token=nm19nav";
        public static string AuthTokenFile { get => instance.authTokenFile; }
    }
}