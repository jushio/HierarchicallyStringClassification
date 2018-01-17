using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicallyStringClassfication
{
    internal class IndexedNode : Node
    {
        internal IndexedNode(string word) : base(word) {; }
        internal List<int> indices = new List<int>();

        internal override void AddNode(IEnumerable<string> strs)
        {
            NofDescendants++;
            if (strs.Count() == 0) return;
            if (Int32.TryParse(strs.First(), out int j))
            {
                indices.Add(j);
                return;
            }

            if (!Children.ContainsKey(strs.First()))
                Children[strs.First()] = new IndexedNode(strs.First());
            Children[strs.First()].AddNode(strs.Skip(1));
        }

    }
}
