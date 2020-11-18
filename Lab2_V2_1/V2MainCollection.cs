using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using System.Numerics;

namespace Lab2_V2_1
{
    class V2MainCollection : IEnumerable<V2Data>
    {
        private List<V2Data> v2Datas;

        public int Count
        {
            get { return v2Datas.Count; }
        }

        public void Add(V2Data item)
        {
            v2Datas.Add(item);
        }

        public bool Remove(string id, double w)
        {
            bool flag = false;

            for (int i = 0; i < v2Datas.Count;)
            {
                if (v2Datas[i].Freq == w && v2Datas[i].Info == id)
                {
                    v2Datas.Remove(v2Datas[i]);
                    flag = true;
                }
                else
                {
                    i++;
                }
            }

            return flag;
        }

        public void AddDefaults()
        {
            Grid1D Ox = new Grid1D(10, 3);
            Grid1D Oy = new Grid1D(10, 3);
            v2Datas = new List<V2Data>();
            V2DataOnGrid[] grid = new V2DataOnGrid[3];
            V2DataCollection[] collections = new V2DataCollection[3];

            for (int i = 0; i < 3; i++)
            {
                grid[i] = new V2DataOnGrid("data info2"/*+ i.ToString()*/, 2, Ox, Oy);     // test i = 2
                collections[i] = new V2DataCollection("collection info" + i.ToString(), i);
            }

            for (int i = 0; i < 3; i++)
            {
                grid[i].initRandom(0, 100);
                collections[i].initRandom(4, 100, 100, 0, 100);
                v2Datas.Add(grid[i]);
                v2Datas.Add(collections[i]);
            }
        }

        public override string ToString()
        {
            string ret = "";
            foreach (V2Data data in v2Datas)
            {
                ret += (data.ToString() + '\n');
            }
            return ret;
        }

        public IEnumerator<V2Data> GetEnumerator()
        {
            return ((IEnumerable<V2Data>)v2Datas).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)v2Datas).GetEnumerator();
        }

        public string ToLongString(string format)
        {
            string ret = "";
            foreach (V2Data data in v2Datas)
            {
                ret += (data.ToLongString(format) + '\n');
            }
            return ret;
        }

        public double Average
        {
            get {
                /*
                var la = 0;
                foreach (V2Data data in v2Datas)
                {
                    V2DataOnGrid buf = (V2DataOnGrid)data;
                    buf.Iterator();
                }
                */
                //List<V2DataOnGrid> buf_list = (List<V2DataOnGrid>)v2Datas;

                var buf_list = from data in v2Datas select (V2DataCollection)data;
                var iters = from buf in buf_list select buf.Iterator().Average(n => n.Complex.Real);
                return iters.Average(); }
        }
        
    }
}
