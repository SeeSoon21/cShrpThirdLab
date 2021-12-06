using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace just_try
{
    //TKey - тип делегата, KeySelector – класс делегата
    delegate TKey KeySelector<TKey>(Magazine mg);

    class MagazineCollection<TKey> //TKey–определяет тип ключа в коллекции
    {
        private Dictionary<TKey, Magazine> dictionaryMagazine = new Dictionary<TKey, Magazine>(); //коллекция ключ-magazine
        private KeySelector<TKey> myKeySelector; //экземпляр делегата(то есть, это уже указатель(метод))

        //конструктор(параметр–указатель на функцию, высчитывающую ключ)
        public MagazineCollection(KeySelector<TKey> magazineKey)
        {
            this.myKeySelector = magazineKey;
            dictionaryMagazine = new Dictionary<TKey, Magazine>();
        }

        #region свойства
        public double getMaxAverageRating{
            get{
                //if collection don't contain elements
                if (dictionaryMagazine.Values.Count > 0)
                {
                    return dictionaryMagazine.Values.Max(m => m.Average_rating);
                }
                return -1;
            }
        }

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> groupingDependsFrequency
        {
            get
            {
                return dictionaryMagazine.GroupBy(m => m.Value.Frequency_of_release);
            }
        }

  
        #endregion свойства

        #region методы
        //метод для добавления некоторого числа элементов Magazine для инициализации коллекции по умолчанию
        public void AddDefaults()
        {
            
            Magazine tempMagazine = new Magazine();
            TKey key = myKeySelector(tempMagazine);
            dictionaryMagazine.Add(key, tempMagazine);
        }

        //добавление элементов в коллекцию
        public void AddMagazines(params Magazine[] arrayMagazine)
        {
            TKey key;
            for (int i = 0; i < arrayMagazine.Length; i++)
            {
                key = myKeySelector(arrayMagazine[i]);
                if (key != null)
                    dictionaryMagazine.Add(myKeySelector(arrayMagazine[i]), arrayMagazine[i]);
                else Console.WriteLine("error!");
            }
        }

        public override string ToString()
        {
            string small_string = "";
            foreach(KeyValuePair<TKey, Magazine> keyValue in dictionaryMagazine)
            {
                small_string += dictionaryMagazine[keyValue.Key].ToString();
                small_string += "\n\n";
            }

            return small_string;
        }

        public virtual string ToShortString()
        {
            string small_string = "";
            foreach(KeyValuePair<TKey, Magazine> keyValue in dictionaryMagazine)
            {
                small_string += dictionaryMagazine[keyValue.Key].ToShortString();
                small_string += "\n\n";
            }

            return small_string;
        }

        //возвращает подмножество коллекции с заданной периодичность выхода журнала
        public IEnumerable<KeyValuePair<TKey, Magazine>>FrequencyGroup(Frequency value)
        {
            return dictionaryMagazine.Where( m => m.Value.Frequency_of_release == value);
        }
        #endregion
    }
}
