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
                item.View.NameTxt.text = item.Content.data.ФИО;
               
                // Отобразить дату смерти
                item.View.DeathDateTxt.text = FormatDeathDate(item.Content.data.Кончина);

                // Отобразить чин святости
                item.View.ChinTxt.text = FormatChin(item.Content.data.Канонизация);

                // Добавить элемент в список
                SearchSettings.PersonList.Add(item);
            }

            // Сообщить о обновлении списка
            SearchSettings.OnSearchListUpdated?.Invoke();
        }

        private string FormatDeathDate(PersonContent.Death death)
        {
            var deathDay = death.День <= 0 ? "?" : death.День.ToString();

            var deathMonth = death.Месяц <= 0 ? "?" : death.Месяц.ToString();

            var deathYear = death.Год <= 0 ? "????" : death.Год.ToString();

            var result = string.Format("{0}.{1}.{2}", deathDay, deathMonth, deathYear);

            return result;
        }

        private string FormatChin(PersonContent.Kanonization kanonization)
        {
            var result = string.IsNullOrEmpty(kanonization.Чин_святости) ? "Не канонизирован" : kanonization.Чин_святости;

            return result;
        }
    }
}
