using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace just_try
{
    class Edition 
    {
        
        protected string editionTitle;
        private DateTime dateEdition;
        private int editionCirculation;

        public string EditionTitle {
            get { return this.editionTitle; }
            set { this.editionTitle = value; }
        }

        public DateTime DateEdition {
            get { return dateEdition; }
            set { dateEdition = value; }
        }

        public int EditionCirculation {
            get { return this.editionCirculation; }
            set {
                try
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    this.editionCirculation = value;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Вызвано исключение при присваивании {value}: " + ex);
                    Console.WriteLine("Допустимое значение >= 0");
                }
            }
        }

       
        public Edition()
        {
            editionTitle = "empty edition";
            dateEdition = new DateTime(2020, 3, 3);
            editionCirculation = 1;
        }

        public Edition(string title, DateTime date, int circulation)
        {
            editionTitle = title;
            dateEdition = date;
            editionCirculation = circulation;

        }

        public virtual object DeepCopy()
        {
            return new Edition(this.EditionTitle, this.DateEdition,
                this.EditionCirculation);
        }

        public override string ToString()
        {
            return "Название издания: " + EditionTitle +
                "\tДата выхода издания: " + DateEdition.ToShortDateString() +
                "\tТираж издания: " + EditionCirculation + '\n';
        }

        public override bool Equals(object obj)
        {
            if (obj is Edition edit)
            {
                if (this.EditionTitle == edit.EditionTitle &&
                    this.DateEdition == edit.DateEdition &&
                    this.EditionCirculation == edit.EditionCirculation)
                {
                    return true;
                }

                return false;
            }
            return false;
        }

        public static bool operator==(Edition ed1, object ob2)
        {
            if (ob2 is Edition editTwo)
            {
                if (ed1.Equals(editTwo))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool operator !=(Edition ed1, object ob2)
        {
            if (ob2 is Edition editTwo)
            {
                if (!ed1.Equals(editTwo))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 31 * this.EditionTitle.GetHashCode() +
                31 * this.DateEdition.GetHashCode() +
                this.EditionCirculation.GetHashCode();
        }


    }
}

/*//сравнение 2-x Edition по названию(если возвращается >0, то вызывающий объект стоит после объекта-параметра)
        public int CompareTo(object o)
        {
            if (o == null) return 1;

            //Console.WriteLine("Вывод из CompareTo");
            //Console.WriteLine(o.ToString());
            Edition tempEdition = o as Edition;
            //Console.WriteLine("Вывод tempEdition" + tempEdition.ToString());
            //приводим объект к типу Edition, если нельзя – temp'у присв. null
            //if (tempEdition != null)
                return this.EditionTitle.CompareTo(tempEdition.EditionTitle);
            //else
               // throw new Exception("Невозможно сравнить два объекта по полю EditionTitle");
             
        }

        //переопределение сортировки массива форычем по полю
        public int Compare(Edition edition1, Edition edition2)
        {
            if (edition1.DateEdition > edition2.DateEdition)
                return 1;
            else if (edition1.DateEdition < edition2.DateEdition)
                return -1;
            else
                return 0;

        }

        //вспомогательный класс, реализующий интерфейс IComparer<Edition>
        private class HelpClassEdition : Edition, IComparer<Edition>
        {
            public int Compare(Edition edition1, Edition edition2)
            {
                if (edition1.EditionCirculation > edition2.EditionCirculation)
                    return 1;
                else if (edition1.EditionCirculation < edition2.EditionCirculation)
                    return -1;
                else
                    return 0;
            }
        }
        */
