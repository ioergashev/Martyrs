using UnityEngine;

namespace PSTGU
{
    /// <summary> Собирает список найденных записей при успешном поиске </summary>
    public class BuildSearchListSystem : MonoBehaviour
    {
        private SearchWindow searchWindow;
        private SearchSettingsRuntime searchSettingsRuntime;
        private DataRuntime dataRuntime;

        private void Awake()
        {
            searchWindow = FindObjectOfType<SearchWindow>();
            searchSettingsRuntime = FindObjectOfType<SearchSettingsRuntime>();
            dataRuntime = FindObjectOfType<DataRuntime>();
        }

        private void Start()
        {
            searchSettingsRuntime.OnSearchComplite.AddListener(SearchCompliteAction);
        }

        private void SearchCompliteAction()
        {
            // Очистить старый список
            searchSettingsRuntime.PersonList.ForEach(item => Destroy(item.gameObject));
            searchSettingsRuntime.PersonList.Clear();

            for (int i = 0; i < dataRuntime.SearchResponse.Count; i++)
            {
                // Добавить элемент в список
                var item = Instantiate(SearchSettings.Instance.SearchListItemPrefab, 
                    searchWindow.View.SearchListContainer).GetComponent<PersonListItem>();

                // Выгрузить информацию о личности
                item.Content = dataRuntime.SearchResponse[i];

                // Отобразить имя
                item.View.NameTxt.text = FormatName(item.Content.data);

                // Отобразить время жизни
                item.View.LifetimeTxt.text = FormatLifetime(item.Content.data);

                // Отобразить сан
                item.View.SanTxt.text = FormatSan(item.Content.data);

                // Добавить элемент в список
                searchSettingsRuntime.PersonList.Add(item);
            }

            // Сообщить о обновлении списка
            searchSettingsRuntime.OnSearchListUpdated?.Invoke();
        }

        private string FormatLifetime(PersonContent.PersonData personData)
        {
            string result = "?";

            string birth = FormatBirthDate(personData.Рождение);

            string death = FormatDeathDate(personData.Кончина);

            // Если исвестна хотя бы одна из дат
            if (birth != "?" || death != "?")
            {
                result = string.Format("{0} — {1}", birth, death);
            }

            return result;
        }

        private string FormatBirthDate(PersonContent.Birth birth)
        {
            string result = "?";

            // Если год указан
            if (birth.Год >= 1)
            {
                // Записать год
                result = birth.Год.ToString("D4");

                // Если месяц указан
                if (birth.Месяц >= 1)
                {
                    // Записать месяц
                    result = birth.Месяц.ToString("D2") + "-" + result;

                    // Если день указан
                    if (birth.День >= 1)
                    {
                        // Записать день
                        result = birth.День.ToString("D2") + "-" + result;
                    }
                }
            }

            return result;
        }

        private string FormatDeathDate(PersonContent.Death death)
        {
            string result = "?";

            // Если год указан
            if (death.Год >= 1)
            {
                // Записать год
                result = death.Год.ToString("D4");

                // Если месяц указан
                if (death.Месяц >= 1)
                {
                    // Записать месяц
                    result = death.Месяц.ToString("D2") + "-" + result;

                    // Если день указан
                    if (death.День >= 1)
                    {
                        // Записать день
                        result = death.День.ToString("D2") + "-" + result;
                    }
                }
            }

            return result;
        }

        private string FormatName(PersonContent.PersonData personData)
        {
            // По умолчанию только ФИО
            string result = personData.ФИО;

            // Форматировать чин
            string chin = MakeFirstCharCase(personData.Канонизация.Чин_святости, true);

            // Если имеется чин
            if (!string.IsNullOrEmpty(chin))
            {
                // Прибавить чин к имени
                result = string.Format("{0} {1}", chin, result);
            }

            return result;
        }

        private string FormatSan(PersonContent.PersonData personData)
        {
            string result = MakeFirstCharCase(personData.Сан_ЦеркСлужение, true);

            return result;
        }

        /// <param name="upper"> true - верхний регистр; false - нижний регистр </param>
        private string MakeFirstCharCase(string str, bool upper)
        {
            // Если строка не пустая
            if (!string.IsNullOrEmpty(str) && str.Length >=2)
            {
                char firstChar = upper ? char.ToUpperInvariant(str[0])
                   : char.ToLowerInvariant(str[0]);

                str = firstChar + str.Substring(1);
            }

            return str;
        }
    }
}
