using UnityEngine;
using UnityEngine.Networking;

namespace PSTGU.ServerCommunication
{
    public class SimpleSearchRequest : RequestBase
    {
        private const string searchURL = "Search/Simple?query=";

        public SimpleSearchRequest(string url, string query, int skip = 0, int take = 0) : base(url + searchURL + query)
        {
            if (skip > 0)
            {
                URL += "&Skip=" + skip;
            }

            if (take > 0)
            {
                URL += "&Take=" + take;
            }
        }

        public override void InitRequest()
        {
            unityWebRequest = UnityWebRequest.Get(URL);
        }
    }
}
