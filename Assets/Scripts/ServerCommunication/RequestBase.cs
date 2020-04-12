using UnityEngine;
using UnityEngine.Networking;

namespace PSTGU.ServerCommunication
{
    public abstract class RequestBase
    {
        public string URL;
        protected UnityWebRequest unityWebRequest;

        public UnityWebRequest UnityWebRequest
        {
            get
            {
                if (unityWebRequest == null) 
                {
                    InitRequest();
                }
                return unityWebRequest;
            }
        }

        protected RequestBase(string url)
        {
            URL = url;
        }

        public AsyncOperation Send()
        {
			return UnityWebRequest.SendWebRequest();
        }

        public void Abort()
        {
            if (unityWebRequest != null)
            {
                unityWebRequest.Abort();
            }
        }

        public abstract void InitRequest();
    }
}
