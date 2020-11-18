using System;
using System.Numerics;

namespace Lab2_V2_1
{
    struct DataItem
    {
        public Vector2 Vector { get; set; }
        public Complex Complex { get; set; }

        public DataItem(Vector2 vector, Complex complex)
        {
            Vector = vector;
            Complex = complex;
        }

        public override string ToString()
        {
            return "Vector: " + Vector.X.ToString() + " " + Vector.Y.ToString() + "\n" +
                   "Complex: " + Complex.ToString() + "\n";
        }

        public string ToString(string format)
        {
            return "Vector: " + Vector.X.ToString(format) + " " + Vector.Y.ToString(format) + "\n" +
                   "Complex: " + Complex.ToString(format);
        }

    }

    struct Grid1D
    {
        public float Step { get; set; }
        public int Num { get; set; }

        public Grid1D(float step, int num)
        {
            Step = step;
            Num = num;
        }

        public override string ToString()
        {
            return "Step: " + Step.ToString() + "; Num: " + Num.ToString();
        }

        public string ToString(string format)
        {
            return "Step: " + Step.ToString(format) + "; Num: " + Num.ToString(format);
        }
    }

    abstract class V2Data
    {
        public string Info { get; set; }
        public double Freq { get; set; }

        public V2Data(string info, double freq)
        {
            Info = info;
            Freq = freq;
        }

        public abstract Complex[] NearAverage(float eps);
        public abstract string ToLongString();
        public abstract string ToLongString(string format);

        public override string ToString()
        {
            return "Info: " + Info + " Frequency: " + Freq.ToString();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            /* 1 */
            Grid1D x = new Grid1D(10, 3);
            Grid1D y = new Grid1D(10, 3);
            V2DataOnGrid test = new V2DataOnGrid("test", 100, x, y);
            test.initRandom(0, 100);
            Console.WriteLine(test.ToLongString());

            //Console.WriteLine("\n\n ========== OPERATOR ========== \n\n");

            V2DataCollection collection = (V2DataCollection)test;
            Console.WriteLine(collection.ToLongString());

            /* 2 */
            V2MainCollection mainCollection = new V2MainCollection();
            mainCollection.AddDefaults();
            Console.WriteLine(mainCollection.ToString());

            /* 3 */
            Complex[] c;
            int count = 1;
            foreach (V2Data item in mainCollection)
            {
                Console.WriteLine("item " + count.ToString());
                item.ToLongString();

                c = item.NearAverage(10);
                Console.WriteLine("average eps = 10");
                for (int i = 0; i < c.Length; i++)
                {
                    Console.WriteLine(c[i].ToString());
                }
                count++;
            }

            /* test remove */
            mainCollection.Remove("data info2", 2);
            Console.WriteLine(mainCollection.ToString());
        }
    }
}