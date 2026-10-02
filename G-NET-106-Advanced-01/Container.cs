using System;
using System.Collections.Generic;
using System.Text;

namespace G_NET_106_Advanced_01
{
    internal class Container<t>
    {
        public t Template { get; set; }
        public void Add(t t) { 
            Template = t;
        }
        public t Get()
        {
            return Template;
        }
    }
}
