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

            string dataDir = @"./dat";
            string[] files = System.IO.Directory.GetFiles(
                dataDir, "*", System.IO.SearchOption.AllDirectories);
            bool ver0 = false;
            foreach (var file in files)
            {
                if (ver0)
                {
                    Console.WriteLine(file);
                    StreamWriter sw = new StreamWriter(
                    file + "out.txt", // 出力先ファイル名
                      true, // 追加書き込み
                    System.Text.Encoding.GetEncoding("Shift_JIS")); // 文字コード
                    TextWriter tmp = Console.Out; // 標準出力の保持
                    Console.SetOut(sw); // 出力先（Outプロパティ）を設定

                    ClassifyFile(file, 4);
                    Console.SetOut(tmp); //戻す
                    sw.Close();
                }
                ClassifyFileToFiles(file, 3);
            }
        }

        static void ClassifyFile(string fileName, int limit = int.MaxValue)
        {
            var root = new Node("");
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(fileName);
            string line;
            var delims = new char[] { '/', '_' };
            while ((line = file.ReadLine()) != null)
            {
                root.AddNode(line.Split(delims));
            }
            root.ListUp(0, limit);
        }

        static void ClassifyFileToFiles(string fileName, int limit = int.MaxValue)
        {
            var root = new IndexedNode("");
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(fileName);
            string line;
            var delims = new char[] { '/', '_', '[' , ']'};
            while ((line = file.ReadLine()) != null)
            {
                var defs = line.Split(' ');
                root.AddNode(defs[1].Split(delims));
            }

            int cnt = 0;
            foreach (var child in root.Children)
            {
                StreamWriter sw = new StreamWriter(
    @"" + fileName + "_" + cnt++ + "_out.txt", // 出力先ファイル名
      true, // 追加書き込み
    System.Text.Encoding.GetEncoding("Shift_JIS")); // 文字コード

                TextWriter tmp = Console.Out; // 標準出力の保持
                Console.SetOut(sw); // 出力先（Outプロパティ）を設定

                Console.WriteLine(child.Key);

                child.Value.ListUp(0, limit);

                Console.SetOut(tmp); //戻す
                sw.Close();
            }
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
