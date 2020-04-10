using UnityEngine;
using System;

namespace PSTGU
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        private static GameSettings _instance;

        private static GameSettings instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<GameSettings>("PSTGU/GameSettings");
                }

                return _instance;
            }
        }
    }
}