using System;
using System.Collections.Generic;
using System.Text;

namespace G_NET_106_Advanced_01
{
    internal class SafeList <T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public void Add (T add)
        {
            Items.Add (add);
        }
        public  T Get(int index)
        {
            if (index >= 0 & Items.Count > index)
            {

                return Items[index];
            }
            else {

                return default;
            }

        }
    }
}
