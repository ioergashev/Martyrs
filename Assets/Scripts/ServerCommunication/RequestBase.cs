using UnityEngine;
using UnityEngine.Networking;

namespace PSTGU.ServerCommunication
{
    public abstract class RequestBase
    {
        public string URL;
        protected UnityWebRequest _unityWebRequest;

        public UnityWebRequest UnityWebRequest
        {
            get
            {
                if (_unityWebRequest == null) 
                {
                    InitRequest();
                }
                return _unityWebRequest;
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
            if (_unityWebRequest != null)
            {
                _unityWebRequest.Abort();
            }
        }

        public abstract void InitRequest();
    }
}
