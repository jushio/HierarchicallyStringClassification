using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicallyStringClassfication
{
    internal class IndexedNode : Node
    {
        internal IndexedNode(IndexedNode parent, string word) : base(word) { Parent = parent; }
        internal SortedSet<int> indices = new SortedSet<int>();
        private static readonly string NumberNodeString = "[Number]";
        public IndexedNode Parent { get; private set; }

        internal override void AddNode(IEnumerable<string> strs)
        {
            NofDescendants++;
            if (strs.Count() == 0) return;
            int j;
            string childNodeStr = strs.First();
            if (Int32.TryParse(childNodeStr, out j))
            {
                indices.Add(j);
                childNodeStr = NumberNodeString;
            }

            if (!Children.ContainsKey(childNodeStr))
                Children[childNodeStr] = new IndexedNode(this, childNodeStr);
            Children[childNodeStr].AddNode(strs.Skip(1));
        }

        internal override void Print()
        {
            if (Str != NumberNodeString)
            {
                base.Print();
                return;
            }
            Console.WriteLine("[" + GetRangedNumSequence(Parent.indices) + "]"
                + " : " + NofDescendants);
        }

        /// <summary>
        /// 1,2,3,5,7,8,9 => [1-3,5,7-9]
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private string GetRangedNumSequence(SortedSet<int> nums)
        {
            StringBuilder sb = new StringBuilder();
            if (nums.Count == 0)
                return "";
            int prev = int.MaxValue;
            int lastPrintedValue = int.MaxValue;
            int cnt = 0;
            foreach (int now in nums)
            {
                bool isFirst = (cnt++ == 0);
                bool discontinuous = isFirst || (now != (prev + 1));
                if (discontinuous)
                {
                    if (!isFirst)
                    {
                        if (lastPrintedValue != prev)
                        {
                            sb.Append("-");
                            sb.Append(prev);
                        }
                        sb.Append(",");
                    }
                    sb.Append(now);
                    lastPrintedValue = now;
                }
                prev = now;
            }
            if (lastPrintedValue != nums.Last())
            {
                sb.Append("-");
                sb.Append(nums.Last());
            }
            return sb.ToString();
        }
    }
}
