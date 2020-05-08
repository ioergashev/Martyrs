using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace PSTGU
{
    public class WelcomeSettingsRuntime : MonoBehaviour
    {
        public string SiteUrl = "http://pstgu.ru/baza-dannykh-za-khrista-postradavshie/";

        public bool NeedShowWelcome
        {
            get
            {
                var value = PlayerPrefs.GetInt("NeedShowWelcome", 1) == 1? true : false;

                return value;
            }
            set
            {
                PlayerPrefs.SetInt("NeedShowWelcome", value == true ? 1 : 0);
            }
        }
    }
}