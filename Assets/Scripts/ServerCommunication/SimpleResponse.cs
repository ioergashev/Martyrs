using System;

namespace PSTGU.ServerCommunication
{
    [Serializable]
    public class SimpleResponse
    {
        public string error;
        public long errorCode = 0;
        public string success;

        public bool IsError { get { return !string.IsNullOrEmpty(error) || errorCode != 0; } }

        public override string ToString()
        {
            return base.ToString() + "\nIsError="+ IsError+" error="+ error+ " errorCode="+errorCode+ " success=" +success;
        }
    }
}