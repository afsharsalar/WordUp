using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordUp.Model
{
    public class Group : IHierarchy
    {
        public IHierarchy Parent { get; set; }
        public string Name { get; set; }
    }
}
