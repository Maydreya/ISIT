using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace NeiroNet1
{
    // это по сути контейнер для массива нейронов neironArray
    // загружет его при создании из файла и сохраняет при выходе

    class NeiroWeb
    {
        private const int          defaultNeironCount  =           32;
        public const int neironInArrayWidth = 10; // количество по горизонтали
        public const int neironInArrayHeight = 10; // количество по вертикали
        private const string memory = "memory.txt"; // имя файла хранения сети
        private List<Neiron> neironArray = null; // массив нейронов

        // конструктор


        public NeiroWeb()
        {
            neironArray = InitWeb();            
        }

        // функция открывает текстовой файл и преобразовывает его в массив нейронов
        private static List<Neiron> InitWeb()
        {
            if (!File.Exists(memory)) return new List<Neiron>();
            string[] lines = File.ReadAllLines(memory);
            if (lines.Length == 0)    return new List<Neiron>();
            string jStr = lines[0];
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<Object> objects = json.Deserialize<List<Object>>(jStr);
            List<Neiron> res = new List<Neiron>();
            foreach (var o in objects) res.Add(NeironCreate((Dictionary<string,Object>)o));
            return res;
        }

        // функция сохраняет массив нейронов в файл
        public void SaveState()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            string jStr = json.Serialize(neironArray);
            System.IO.StreamWriter file = new System.IO.StreamWriter(memory);
            file.WriteLine(jStr);
            file.Close();
        }

        // получить список имён образов, имеющихся в памяти
        public string[] GetLiteras()
        {
            var res = new List<string>();
            for (int i = 0; i < neironArray.Count; i++) res.Add(neironArray[i].GetName());
            res.Sort();
            return res.ToArray();
        }

        // преобразовать структуру данных в клас нейрона
        private static Neiron NeironCreate(Dictionary<string, object> o)
        {
            Neiron res = new Neiron();
            res.name = (string)o["name"];
            res.countTrainig = (int)o["countTrainig"];
            Object[] veightData = (Object[])o["veight"];
            int arrSize = (int)Math.Sqrt(veightData.Length);
            res.veight = new double[arrSize, arrSize];
            int index = 0;
            for (int n = 0; n < res.veight.GetLength(0); n++)
                for (int m = 0; m < res.veight.GetLength(1); m++)
                {
                    res.veight[n, m] = Double.Parse(veightData[index].ToString());
                    index++;
                }
            return res;
        }
    }
}
