using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicallyStringClassfication
{
    internal class Node
    {
        internal string Str;
        internal int NofDescendants = 0;
        internal Dictionary<string, Node> Children = new Dictionary<string, Node>();

        internal Node(string word)
        {
            Str = word;
        }

        internal virtual void AddNode(IEnumerable<string> strs)
        {
            NofDescendants++;
            if (strs.Count() == 0) return;
            if (!Children.ContainsKey(strs.First()))
                Children[strs.First()] = new Node(strs.First());
            Children[strs.First()].AddNode(strs.Skip(1));
        }

        //リストアップする
        internal virtual void ListUp(int depth = 0, int limit = int.MaxValue)
        {
            if (depth > limit)
                return;
            for (int i = 0; i < depth; i++) Console.Write("-");
            Print();
            Children.Values.ToList().ForEach(n => n.ListUp(depth+1, limit));
        }

        internal virtual void Print()
        {
            Console.WriteLine(Str + " : " + NofDescendants);
        }
    }
}
