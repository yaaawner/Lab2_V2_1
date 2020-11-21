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

                //var buf_list = from data in v2Datas select (V2DataCollection)data;
                //var iters = from buf in buf_list select buf.Iterator().Average(n => n.Complex.Real);
                //return iters.Average(); 

                //var ret = from item in this.v2Datas where item is V2DataCollection select (V2DataCollection)item;

                //var ret = from buf in (from data in v2Datas where data is V2DataCollection select (V2DataCollection)data) select buf.Iterator()

                //List<DataItem> DI = (from item in v2Datas select (List<DataItem>)item);
                //double ret = 

                //IEnumerable<DataItem> buf_enum = ((IEnumerable<DataItem>)(from di in (from item in this.v2Datas select (V2DataCollection)item)
                //select di));

                //IEnumerable<DataItem> buf_enum = from d in (from item in this.v2Datas select (V2DataCollection)item) select d;

                //var buf_list = from data in v2Datas select (V2DataCollection)data;
                //IEnumerable<DataItem> DI = from buf in buf_list select (from d in buf.dataItems select d);

                //from d in buf select d;
                //IEnumerable<DataItem> di_list = from di in buf_list select di;

                //double ret = di_list.Average()

                //var ret = (from elem in (from item in this.v2Datas
                //              where item is V2DataCollection
                //             select (V2DataCollection)item)
                //           from dti in elem select dti);

                var buf_list = from data in v2Datas select (V2DataCollection)data;
                //var iters = from buf in buf_list select buf.Iterator().Sum(n => n.Complex.Magnitude);
                //return iters.Average();

                var DI = from elem in buf_list from item in elem.dataItems select item;

                return DI.Average(n => n.Complex.Magnitude);
                         
            }
        }

        public DataItem NearAverage
        {
            get
            {
                /*
                double a = this.Average;
                var buf_list = from data in v2Datas select (V2DataCollection)data;
                var iters = from buf in buf_list select buf.Iterator().Min(n => Math.Abs(n.Complex.Magnitude - a));
                double mmin = iters.Min();
                var ret = from buf in buf_list
                          where Math.Abs(buf.Iterator().Min(n => Math.Abs(n.Complex.Magnitude - a) - mmin)) < 0.01
                          select buf;

                var ret2 = from dc in ret from i in dc.dataItems select i;


                IEnumerable < DataItem > res =
                    from elem in (from item in this.v2Datas
                                  where item is V2DataCollection
                                  select (V2DataCollection)item)
                    from dti in elem
                    select dti;

                var DI = 
                */

                double a = this.Average;

                var buf_list = from data in v2Datas select (V2DataCollection)data;

                var DI = from elem in buf_list from item in elem.dataItems select item;

                var dif = from item in DI
                          select Math.Abs(item.Complex.Magnitude - a);

                double min = dif.Min();

                var ret = from item in DI
                          where Math.Abs(item.Complex.Magnitude - a) <= min + 0.01
                          select item;

                return ret.First();

            }
        }

        public IEnumerable<Vector2> Vectors
        {
            get
            {
                var buf_list = from data in v2Datas
                               where data is V2DataCollection
                               select (V2DataCollection)data;

                return from elem in buf_list
                       from item in elem.dataItems
                       select item.Vector;

            }
        }
    }
}
