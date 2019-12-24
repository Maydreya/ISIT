using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiroNet1
{
    class Neiron
    {
        // это класс нейрона, каждый нейрон хранит в себе массив определённого образа
        // он может обучаться и сравнивать значение с имеющимся в памяти

        public string name; // имя - текстовое значение образа который хранит нейрон
        public  double[,] veight; // массив весов - именно это и есть память нейрона
        public  int countTrainig; // количество вариантов образа в памяти
                                  // нужно для правильного пересчёта весов при обучении

        // конструктор
        public Neiron() {}

        // получить имя
        public string GetName() { return name; }

        // очистить память нейрона и присвоить ему новое имя

        public void Clear(string name, int x, int y)
         {
             this.name = name;
             veight = new double[x,y];
             for (int n = 0; n < veight.GetLength(0); n++)
                 for (int m = 0; m < veight.GetLength(1); m++) veight[n, m] = 0;
             countTrainig = 0;
         }

        

    }


}
