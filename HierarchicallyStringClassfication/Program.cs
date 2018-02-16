using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicallyStringClassfication
{
    class Program
    {
        static void Main(string[] args)
        {
            if (false)
                Test();

            if (!(args.Length >= 1 && args.Length <= 3))
            {
                Console.WriteLine("HierarchicallyStringClassfication.exe @fileName [@rowNum] [@depth]");
                return;
            }
            string fileName = args[0];
            int rowNum = -1;
            int depth = -1;
            if (args.Length > 1)
            {
                string rowNum_ = args[1];
                rowNum_ = args[1];
                if (!int.TryParse(rowNum_, out rowNum))
                {
                    Console.WriteLine("@rowNum is not a number.");
                }
            }
            if (args.Length > 2)
            {
                string depth_ = args[2];
                depth_ = args[2];
                if (!int.TryParse(depth_, out depth))
                {
                    Console.WriteLine("@depth is not a number.");
                }
            }
            depth = (depth < 0) ? int.MaxValue : depth; 
            try
            {
                ClassifyFile2(fileName, rowNum: rowNum, limit: depth);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }


        static void ClassifyFile(string fileName, int limit = int.MaxValue)
        {
            var root = new Node("");
            // Read the file and display it line by line.  
            using (System.IO.StreamReader file =
                new System.IO.StreamReader(fileName))
            {
                string line;
                var delims = new char[] { '/', '_' };
                while ((line = file.ReadLine()) != null)
                {
                    root.AddNode(line.Split(delims));
                }
            }
            root.ListUp(0, limit);
        }

        static void ClassifyFile2(string fileName, int rowNum = -1, int limit = int.MaxValue)
        {
            var root = new IndexedNode(null, "");
            // Read the file and display it line by line.  
            using (System.IO.StreamReader file =
               new System.IO.StreamReader(fileName))
            {
                string line;
                var delims = new char[] { '/', '_', '[', ']' };
                while ((line = file.ReadLine()) != null)
                {
                    string str = line;
                    if (rowNum >= 0)
                    {
                        var defs = line.Split(' ');
                        if (!(rowNum < defs.Length))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("Exception: row number " + rowNum + " is over the number of columns in the entry.");
                            sb.AppendLine("---");
                            sb.AppendLine(line);
                            sb.AppendLine("---");
                            throw new Exception(sb.ToString());
                        }
                        str = defs[rowNum];
                    }
                    root.AddNode(str.Split(delims));
                }
            }
            root.ListUp(0, limit);
        }

        static void Test()
        {
            List<string> sample = new List<string>() { "a", "b", "a" };
            List<List<string>> samples = new List<List<string>>()
            {
                new List<string>() {"a", "b" },
                new List<string>() {"a", "c" },
                new List<string>() {"b", "c" }
            };
            var root = new Node("");
            root.AddNode(sample);
            root.ListUp(0);
            Console.ReadLine();

            var root2 = new Node("");
            foreach (var sa in samples)
                root2.AddNode(sa);
            root2.ListUp(0);
            Console.ReadLine();

            List<string> sample3 = new List<string>()
            {
                "a/b/c",
                "a/b/a",
                "a/b/d"
            };
            var root3 = new Node("");
            foreach (var sa in sample3)
                root3.AddNode(sa.Split('/'));
            root3.ListUp();
            Console.ReadLine();

            ClassifyFile(@"0.txt", 5);
        }
    }
}
