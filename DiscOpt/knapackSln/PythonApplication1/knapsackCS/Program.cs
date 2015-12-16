using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace knapsackCS
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Solver(args[0]);
            s.SolveBottomUp();
            //Console.WriteLine(s.SolveBottomUp() + " 0");
        }
    }

    class Solver
    {
        int W, n;
        List<Item> items = new List<Item>();

        public Solver(string filename)
        {
            // load the data.
            using (StreamReader reader = new StreamReader(filename))
            {
                string line = reader.ReadLine();
                string[] info = line.Split(new[] { ' ' });
                
                this.n = int.Parse(info[0]);
                this.W = int.Parse(info[1]);

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    info = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (!string.IsNullOrEmpty(line))
                    {
                        Item item = new Item();
                        item.v = int.Parse(info[0]);
                        item.w = int.Parse(info[1]);
                        items.Add(item);
                    }
                }
            }
        }

        public int SolveBottomUp()
        {
            int[] m = new int[W + 1];
            List<int> itemsUsed = new List<int>();
            Dictionary<int, List<int>> paths = new Dictionary<int, List<int>>();

            for (int i = 1; i < n; i++)
            {
                for (int j = W; j > 0; j--)
                {
                    if (j >= items[i].w)
                    {
                        if (m[j] < m[j - items[i].w] + items[i].v)
                        {
                            m[j] = m[j - items[i].w] + items[i].v;

                            List<int> path = new List<int>();
                            List<int> prevPath = new List<int>();

                            if (paths.ContainsKey(m[j - items[i].w]))
                            {
                                prevPath = paths[m[j - items[i].w]];
                                path.AddRange(prevPath);
                            }
                            
                            path.Add(i);
                            paths[m[j]] = path;                            
                        }
                    }
                }
            }

            Console.WriteLine(m[W] + " 0");
            var p = paths[m[W]];
            int output = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (p.Contains(i)) { output = 1; } else { output = 0; }
                Console.Write(output + " ");
            }

            Console.WriteLine();

            return m[W];
        }

        struct Item
        {
            public int w;
            public int v;
        }

    }
}
