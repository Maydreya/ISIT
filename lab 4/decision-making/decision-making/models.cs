using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decision_making
{
    class models
    {
        public int[] votes { get; private set; }
        public double[] percents { get; private set; }
        public List<int> wins = new List<int>();
        public string win;

        //сброс голосов
        public void ClearVotes()
        {
            votes = null;
        }

        //создание массива для подсчета голосов
        public void CreateVotesMass(int count)
        {
            votes = new int[count];
            percents = new double[count];
        }

        //добавление голоса определеннному кандидату
        public void Bolshinstvo(int index)
        {
            votes[index]++;
            int summ = 0;
            foreach (int a in votes)
                summ += a;
            for (int i = 0; i < percents.Length; i++)
                percents[i] = (double)votes[i] / (double)summ;
            maximum(votes);

        }
        //явный победитель
        public List<string> yavn(int[,] arr)
        {
            string[] alt = new string[Data.alts];
            int n = Data.alts;
            int m = Data.experts;
            int plus = 0;
            List<string> par = new List<string>();
            string alter;
            for (int i = 0; i < n; i++)
            {
                alt[i] = "alternative " + (i + 1);
            }
            for (int i = 1; i < n; i++)
            {
                int alt1 = 0, alt2 = 0;
                for (int j = i + 1; j <= n; j++)
                {
                    int q = 0, g = 0;
                    for (int k = 0; k < m; k++)
                    {
                        for (int h = 0; h < n; h++)
                        {
                            if (arr[k, h] == i) alt1 = h;
                            if (arr[k, h] == j) alt2 = h;
                        }
                        if (alt1 < alt2) q++;
                        else g++;
                    }
                    if (q > g)
                    {
                        par.Add("alternative " + (i) + " > altrenative " + (j));
                    }
                    else
                    {
                        par.Add("alternative " + (i) + " < altrenative " + (j));
                        alter = alt[plus];
                        alt[plus] = alt[j - 1];
                        plus++;
                        alt[j - 1] = alter;
                    }
                }
            }
            win = alt[0];
            return par;
        }
        //Метод Симпсона
        public int[] simpson(int[,] arr)
        {
            int n = Data.alts;
            int m = Data.experts;
            int[,] dop = new int[n, n - 1];
            for (int i = 1; i <= n; i++)
            {
                int alt1 = 0, alt2 = 0, count = 0;
                for (int j = 1; j <= n; j++)
                {
                    if (i != j)
                    {
                        int q = 0, g = 0;
                        for (int k = 0; k < m; k++)
                        {
                            for (int h = 0; h < n; h++)
                            {
                                if (arr[k, h] == i) alt1 = h;
                                if (arr[k, h] == j) alt2 = h;
                            }
                            if (alt1 < alt2) q++;
                            else g++;
                        }
                        dop[i - 1, count] = q;
                        count++;
                    }
                }
            }
            int[] min = new int[n];
            int minv = 0, max, p = 1;
            //выбрать минимальное по строкам, побеждает с максимальным из мах
            for (int i = 0; i < dop.GetLength(0); i++)
            {
                minv = dop[i, 0];
                for (int j = 0; j < dop.GetLength(1); j++)
                {
                    if (dop[i, j] < minv && dop[i, j] != 0)
                        minv = dop[i, j];
                }
                min[i] = minv;
            }
            max = min[0];
            for (int i = 1; i < n; i++)
            {
                if (max < min[i])
                {
                    max = min[i];
                    p = i + 1;
                }

            }
            win = "alternative " + p;
            return min;
        }
        //Модель Борда
        public int[] board(int[,] arr)
        {
            int n = Data.alts;
            int m = Data.experts;
            int[] sum = new int[n];
            for (int i = 1; i <= n; i++)
            {
                for (int k = 0; k < m; k++)
                {
                    for (int h = 0; h < n; h++)
                    {
                        if (arr[k, h] == i) sum[i - 1] += n - h;
                    }
                }
            }
            return sum;
        }
        public int[] kopland(int[,] arr) //копланд
        {
            int n = Data.alts;
            int m = Data.experts;
            int[] solv = new int[n];
            for (int i = 1; i <= n; i++)
            {
                int alt1 = 0, alt2 = 0;
                for (int j = 1; j <= n; j++)
                {
                    if (i != j)
                    {
                        for (int k = 0; k < m; k++)
                        {
                            for (int h = 0; h < n; h++)
                            {
                                if (arr[k, h] == i) alt1 = h;
                                if (arr[k, h] == j) alt2 = h;
                            }
                            if (alt1 < alt2) solv[i - 1]++;
                            else solv[i - 1]--;
                        }
                    }
                }
            }
            return solv;
        }

        public void maximum(int[] array)
        {
            wins.Clear();
           int value = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > value)
                {
                    value = array[i];
                }
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value) wins.Add(i+1);
            }
        }
    }
}
