using System;
using System.Collections.Generic;

namespace TreesAndGraphs
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphActions obj = new GraphActions();

            List<string> projects = new List<string>();
            projects.Add("a");
            projects.Add("b");
            projects.Add("c");
            projects.Add("d");
            projects.Add("e");
            projects.Add("f");
            projects.Add("g");

            List<string[]> dependencies = new List<string[]>();
            dependencies.Add(new string[] { "a", "b" });
            dependencies.Add(new string[] { "b", "a" });
            /*dependencies.Add(new string[] { "f", "a" });
            dependencies.Add(new string[] { "f", "b" });
            dependencies.Add(new string[] { "f", "c" });
            dependencies.Add(new string[] { "c", "a" });
            dependencies.Add(new string[] { "b", "a" });
            dependencies.Add(new string[] { "b", "e" });
            dependencies.Add(new string[] { "a", "e" });
            dependencies.Add(new string[] { "d", "g" });
            */
            var answer = obj.BuildOrder(projects, dependencies);

            foreach (var a in answer)
            {
                Console.WriteLine(a);
            }

            /*Tree temp = new Tree();
            TreeNode<int> t = new TreeNode<int>(5);
            t.LeftNode = new TreeNode<int>(7);
            t.LeftNode.RightNode = new TreeNode<int>(6);
            t.RightNode = new TreeNode<int>(10);

            Console.WriteLine(temp.IsValidBST(t));

            TreeNode<int> t2 = new TreeNode<int>(5);
            t2.LeftNode = new TreeNode<int>(5);
            t2.RightNode = new TreeNode<int>(7);
            Console.WriteLine(temp.IsValidBST(t2));*/
        }
    }
}
