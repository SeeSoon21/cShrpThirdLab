using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;




namespace just_try
{
    class Program
    {

        static void Main(string[] args)
        {

            Magazine objectMagazine = new Magazine("Космос", Frequency.Monthly, new DateTime(2023, 5, 27), 50);
            Magazine objectMagazine2 = new Magazine("Циклон", Frequency.Monthly, new DateTime(2021, 3, 19), 100);
            Magazine objectMagazine3 = new Magazine("Бегемот", Frequency.Yearly, new DateTime(2021, 3, 19), 275);

            Article[] ArrayArticle = new Article[4];
            ArrayArticle[0] = new Article(new Person("Анатолий", "Карапченко", new DateTime(2000, 6, 30)), "Картошка", 2.8);
            ArrayArticle[1] = new Article(new Person("Вызымат", "Дурмалеев", new DateTime(2020, 1, 23)), "Барабир", 8.1);
            ArrayArticle[2] = new Article(new Person("Сергей", "Довлатов", new DateTime(1983, 4, 21)), "Чемоданище", 8.9);
            ArrayArticle[3] = new Article(new Person("Изольд", "Харетьянов", new DateTime(2021, 7, 1)), "Апочему", 7.8);
            Article[] ArrayArticle2 = new Article[2];
            ArrayArticle2[0] = new Article(new Person("Алексей", "Трынд", new DateTime(2000, 6, 30)), "Зефир", 9.9);
            ArrayArticle2[1] = new Article(new Person("Дмитрий", "Шляпников", new DateTime(2020, 1, 23)), "Бегемот", 4.5);
            Article[] ArrayArticle3 = new Article[1];
            ArrayArticle3[0] = new Article(new Person("Трофим", "Богоугодский", new DateTime(2000, 6, 30)), "Сказка о грешниках", 9.8);

            Person[] listEditors = new Person[4];
            listEditors[0] = new Person("Степан", "Крымалов", new DateTime(2018, 12, 16));
            listEditors[1] = new Person("Вызымат", "Дурмалеев", new DateTime(2020, 1, 23));
            listEditors[2] = new Person("Игорь", "Леопольдов", new DateTime(2013, 11, 4));
            listEditors[3] = new Person("Изольд", "Харетьянов", new DateTime(2021, 7, 1));

            objectMagazine.AddArticles(ArrayArticle);
            objectMagazine.AddEditors(listEditors);
            objectMagazine2.AddArticles(ArrayArticle2);
            objectMagazine2.AddEditors(listEditors);
            objectMagazine3.AddArticles(ArrayArticle3);
            objectMagazine3.AddEditors(listEditors);


            //1)вызов методов объекта Magazine по сортировке ListArray
            //сортировка по названию статьи
            Console.WriteLine("\n------------------------------------1-e задание------------------------------------");
            Console.WriteLine("\nВывод массива Article после сортировки по рейтингу");
            objectMagazine.SortByArticleRating();
            foreach (Article art in objectMagazine.ListArticle)
            {
                Console.WriteLine(art);
               
            }

            foreach(var temp in objectMagazine.GetHighRate(3))
            {
                Console.WriteLine($"вЫсший рейтинг: {temp}");
            }

            //сортировка по фамилии автора
            Console.WriteLine("\nВывод после сортировки по названию статьи:");
            objectMagazine.SortByArticleTitle();
            foreach (Article art in objectMagazine.ListArticle)
            {
                Console.WriteLine(art);
            }

            //сортировка по рейтингу статьи
            Console.WriteLine("\nВывод после сортировки по фамилии автора: ");
            objectMagazine.SortByArticleAuthorSurname();
            foreach (Article art in objectMagazine.ListArticle)
            {
                Console.WriteLine(art);
            }



            //2
            //создание переменной делегата
            Console.WriteLine("------------------------------------2-e задание------------------------------------");
            KeySelector<string> myKeySelector = delegate (Magazine magObj)
            {
                return magObj.Mag_title;
            };
            MagazineCollection<string> magCollection = new MagazineCollection<string>(myKeySelector);
            //magCollection.AddDefaults();
            magCollection.AddMagazines(objectMagazine, objectMagazine2, objectMagazine3);
            Console.WriteLine("\n\nВывод magCollection:");
            Console.WriteLine(magCollection.ToShortString());


            //3)Вызов методов класса MagazineCollection
            //наибольший средний рейтинг:
            Console.WriteLine("------------------------------------3-e задание------------------------------------");
            Console.WriteLine($"1.\tНаибольший средний рейтинг: {magCollection.getMaxAverageRating}\n");

            //FrequencyGroup с заданной периодичностью выхода
            IEnumerable pairFreq = magCollection.FrequencyGroup(Frequency.Yearly);
            Console.WriteLine($"2.\tЖурналы, выходящие с периодичность '{Frequency.Yearly}'");
            foreach (KeyValuePair<string, Magazine> keyValue in pairFreq)
            {
                Console.WriteLine(keyValue.Value.ToShortString());
            }

            //свойство класса, выполняющее группировку по периодичности выхода
            IEnumerable groupingByFrequency = magCollection.groupingDependsFrequency;
            Console.WriteLine("\n3.\tГруппировка по периодичности выхода: ");
            foreach (IGrouping<Frequency, KeyValuePair<string, Magazine>> tempKey in groupingByFrequency)
            {
                Console.WriteLine($"Ключ: {tempKey.Key}");
                foreach (KeyValuePair<string, Magazine> tempValue in tempKey)
                {
                    Console.WriteLine($"Значение {tempValue.Value.ToShortString()}");
                }
                
            }


            //4) TestCollection<Edition, Magazine> 
            int number;
            Random rnd = new Random();
            Console.WriteLine("Введите число элементов в коллекции: ");
            number = CheckInt();
            Console.WriteLine(10 / 2);
            
            // генерация KeyValP ч\з локальную функцию
            GenerateElement<Edition, Magazine> Meth = delegate (int i)
            {
                Edition key = new Edition("Edition", new DateTime(1970 + i % 100, i % 12 + 1, i % 30 + 1), i + 10000);
                Magazine value = new Magazine("Magazine", (Frequency)(i % 3), new DateTime(1970 + i % 100, i % 12 + 1, i % 30 + 1), i + 10000);
                return new KeyValuePair<Edition, Magazine>(key, value);
            };

            TestCollection<Edition, Magazine> testCollection = new TestCollection<Edition, Magazine>(number, Meth);

            testCollection.ToMeasureTimeSearchInTKeyList();
            testCollection.toMeasureTimeSearchInStringList();
            testCollection.toMeasureTimeSearchInDictionaryTKey();
            testCollection.toMeasureTimeSearchInDictionaryString();
        }

        public static int CheckInt()
        {
            int number;
            
            while(!int.TryParse(Console.ReadLine(), out number) || number < 0)
            {
                Console.WriteLine("Неверный ввод! Введите ещё раз!");
            }

            return number;
           
        }
    }


}






