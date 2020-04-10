using UnityEngine;

namespace PSTGU
{
    public class BuildSearchListSystem : MonoBehaviour
    {
        private void Start()
        {
            SearchSettings.OnSearchComplite.AddListener(SearchCompliteAction);
        }

        private void SearchCompliteAction()
        {
            // Очистить старый список
            SearchSettings.PersonList.ForEach(item => Destroy(item.gameObject));
            SearchSettings.PersonList.Clear();

            for (int i = 0; i < Data.SearchResponse.Count; i++)
            {
                // Добавить элемент в список
                var item = Instantiate(SearchSettings.SearchListItemPrefab, 
                    SearchWindow.SearchList.SearchListContainer).GetComponent<PersonListItem>();

                // Выгрузить информацию о личности
                item.Content = Data.SearchResponse[i];

                // Отобразить имя
                item.View.NameTxt.text = FormatName(item.Content.data);

                // Отобразить дату рождения
                item.View.BirthDateTxt.text = FormatBirthDate(item.Content.data.Рождение);

                // Отобразить дату смерти
                item.View.DeathDateTxt.text = FormatDeathDate(item.Content.data.Кончина);

                // Отобразить сан
                item.View.SanTxt.text = FormatSan(item.Content.data);

                // Добавить элемент в список
                SearchSettings.PersonList.Add(item);
            }

            // Сообщить о обновлении списка
            SearchSettings.OnSearchListUpdated?.Invoke();
        }

        private string FormatDeathDate(PersonContent.Death death)
        {
            string result = "?";

            // Если год указан
            if (death.Год >= 1)
            {
                // Записать год
                result = death.Год.ToString();

                // Если месяц указан
                if (death.Месяц >= 1)
                {
                    // Записать месяц
                    result = death.Месяц.ToString() + "." + result;

                    // Если день указан
                    if (death.День >= 1)
                    {
                        // Записать день
                        result = death.День.ToString() + "." + result;
                    }
                }
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
                result = birth.Год.ToString();

                // Если месяц указан
                if (birth.Месяц >= 1)
                {
                    // Записать месяц
                    result = birth.Месяц.ToString() + "." + result;

                    // Если день указан
                    if (birth.День >= 1)
                    {
                        // Записать день
                        result = birth.День.ToString() + "." + result;
                    }
                }
            }

            return result;
        }

        private string FormatName(PersonContent.PersonData personData)
        {
            // Форматировать чин
            var chin = MakeFirstCharUpper(personData.Канонизация.Чин_святости);

            // Настроить текст в зависимости от наличия чина
            var result = string.IsNullOrEmpty(chin) ? personData.ФИО : chin + " " + personData.ФИО;

            return result;
        }

        private string FormatSan(PersonContent.PersonData personData)
        {
            var result = MakeFirstCharUpper(personData.Сан_ЦеркСлужение);

            return result;
        }

        private string MakeFirstCharUpper(string str)
        {
            // Если строка не пустая
            if (!string.IsNullOrEmpty(str) && str.Length >=2)
            {
                 // Сделать первый знак заглавным
                 str = char.ToUpperInvariant(str[0]) + str.Substring(1);
            }

            return str;
        }
    }
}
