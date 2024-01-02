using System;
using System.Collections;
using System.Collections.Generic;

namespace TreesAndGraphs
{
    public class Tree
    {
        public TreeNode<int> Root;

        public void AddNode(int value)
        {
            if (Root == null)
            {
                Root = new TreeNode<int>(value);
            }
            else
            {
                var current = Root;
                TreeNode<int> previous = null;
                while (current != null)
                {
                    previous = current;
                    if (value <= current.Value)
                    {
                        current = current.LeftNode;
                    }
                    else
                    {
                        current = current.RightNode;
                    }
                }

                if (value < previous.Value)
                {
                    previous.LeftNode = new TreeNode<int>(value);
                }
                else
                {
                    previous.RightNode = new TreeNode<int>(value);
                }
            }
        }

        public IEnumerable<int> DepthFirstTraversal()
        {
            Stack stack = new Stack();

            stack.Push(Root);

            while (stack.Count != 0)
            {
                yield return ((TreeNode<int>)stack.Peek()).Value;
                TreeNode<int> current = (TreeNode<int>)stack.Pop();
                if (current.RightNode != null)
                    stack.Push(current.RightNode);
                if (current.LeftNode != null)
                    stack.Push(obj: current.LeftNode);
            }
        }

        public IEnumerable<int> BredthFirstTraversal()
        {
            Queue queue = new Queue();

            queue.Enqueue(Root);

            while (queue.Count != 0)
            {
                var current = (TreeNode<int>)queue.Dequeue();
                yield return current.Value;

                if (current.LeftNode != null)
                    queue.Enqueue(current.LeftNode);

                if (current.RightNode != null)
                    queue.Enqueue(current.RightNode);
            }
        }

        public void InOrderTraversal(TreeNode<int> node)
        {
            if (node == null)
            {
                return;
            }
            InOrderTraversal(node.LeftNode);
            Console.WriteLine(node.Value);
            InOrderTraversal(node.RightNode);
        }

        public void PreOrderTraversal(TreeNode<int> node)
        {
            if (node == null)
            {
                return;
            }
            Console.WriteLine(node.Value);
            PreOrderTraversal(node.LeftNode);
            PreOrderTraversal(node.RightNode);
        }

        public void PostOrderTraversal(TreeNode<int> node)
        {
            if (node == null)
            {
                return;
            }
            PostOrderTraversal(node.LeftNode);
            PostOrderTraversal(node.RightNode);
            Console.WriteLine(node.Value);
        }

        public TreeNode<int> MinimalTree(int[] sortedArray)
        {
            if (sortedArray.Length == 0)
            {
                return null;
            }
            if (sortedArray.Length > 1)
            {
                int midLength = sortedArray.Length / 2;
                var node = new TreeNode<int>(sortedArray[midLength]);

                var leftTree = new int[midLength];
                Array.Copy(sortedArray, leftTree, midLength);
                node.LeftNode = MinimalTree(leftTree);

                var rightTreeLength = sortedArray.Length % 2 == 1 ? midLength : midLength - 1;
                var rightTree = new int[rightTreeLength];
                Array.Copy(sortedArray, rightTreeLength + 1, rightTree, 0, rightTreeLength);
                node.RightNode = MinimalTree(rightTree);
                return node;
            }
            else
            {
                return new TreeNode<int>(sortedArray[0]);
            }
        }

        public List<List<TreeNode<int>>> ListOfNodesPerDepth(Tree tree)
        {
            var queue = new Queue<TreeNode<int>>();
            List<List<TreeNode<int>>> result = new List<List<TreeNode<int>>>();

            queue.Enqueue(tree.Root);
            while (queue.Count != 0)
            {
                List<TreeNode<int>> nodes = new List<TreeNode<int>>();
                while (queue.Count != 0)
                {
                    nodes.Add(queue.Dequeue());
                }
                foreach (var child in nodes)
                {
                    if (child.LeftNode != null)
                    {
                        queue.Enqueue(child.LeftNode);
                    }
                    if (child.RightNode != null)
                    {
                        queue.Enqueue(child.RightNode);
                    }
                }
                result.Add(nodes);
            }
            return result;
        }

        public List<List<TreeNode<int>>> ListOfNodesPerDepthDepthFirst(Tree tree)
        {
            List<List<TreeNode<int>>> results = new List<List<TreeNode<int>>>();
            int count = 0;
            DepthFirstListOfNodesCreation(tree.Root, ref results, count);

            return results;
        }

        private void DepthFirstListOfNodesCreation(TreeNode<int> node, ref List<List<TreeNode<int>>> results, int count)
        {
            if (node == null)
            {
                return;
            }
            if (count == results.Count)
            {
                results.Add(new List<TreeNode<int>>());
            }
            results[count].Add(node);
            DepthFirstListOfNodesCreation(node.LeftNode, ref results, count + 1);
            DepthFirstListOfNodesCreation(node.RightNode, ref results, count + 1);
        }

        public bool CheckBalancedTree(Tree tree)
        {
            bool isBalancedTree = true;
            CheckBalancedTree(tree.Root, ref isBalancedTree);
            return isBalancedTree;
        }

        private int CheckBalancedTree(TreeNode<int> node, ref bool isBalancedTree)
        {
            if (!isBalancedTree || node == null)
            {
                return 0;
            }

            int leftHeight = 0, rightHeight = 0;
            if (node.LeftNode != null)
            {
                leftHeight++;
                leftHeight += CheckBalancedTree(node.LeftNode, ref isBalancedTree);
            }
            if (node.RightNode != null)
            {
                rightHeight++;
                rightHeight += CheckBalancedTree(node.RightNode, ref isBalancedTree);
            }

            var diff = Math.Abs(leftHeight - rightHeight);
            if (diff > 1)
                isBalancedTree = false;

            return Math.Max(leftHeight, rightHeight);
        }

        public bool CheckIfBinarySearchTree(Tree tree)
        {
            int? parent = null;
            bool isBST = true;
            CheckIfBinarySearchTree(tree.Root, parent, null, null, ref isBST);
            return isBST;
        }

        private void CheckIfBinarySearchTree(TreeNode<int> node, int? parent, int? leftGrandPrent, int? rightGrandParent, ref bool isBST)
        {
            if (node == null || !isBST)
            {
                return;
            }

            if (parent.HasValue)
            {
                if (parent.Value < node.Value)
                {
                    if (node.LeftNode != null)
                    {
                        if (node.LeftNode.Value <= node.Value && (leftGrandPrent == null || leftGrandPrent != null && node.LeftNode.Value > leftGrandPrent))
                        {
                            CheckIfBinarySearchTree(node.LeftNode, node.Value, parent, rightGrandParent, ref isBST);
                        }
                        else
                        {
                            isBST = false;
                        }
                    }
                    if (node.RightNode != null)
                    {
                        if (node.RightNode.Value > node.Value)
                        {
                            CheckIfBinarySearchTree(node.LeftNode, node.Value, leftGrandPrent, rightGrandParent, ref isBST);
                        }
                        else
                        {
                            isBST = false;
                        }
                    }
                }
                else
                {
                    if (node.LeftNode != null)
                    {
                        if (node.LeftNode.Value <= node.Value)
                        {
                            CheckIfBinarySearchTree(node.LeftNode, node.Value, leftGrandPrent, rightGrandParent, ref isBST);
                        }
                        else
                        {
                            isBST = false;
                        }
                    }
                    if (node.RightNode != null)
                    {
                        if (node.RightNode.Value > node.Value && (rightGrandParent == null || rightGrandParent != null && node.RightNode.Value <= rightGrandParent))
                        {
                            CheckIfBinarySearchTree(node.LeftNode, node.Value, leftGrandPrent, parent, ref isBST);
                        }
                        else
                        {
                            isBST = false;
                        }
                    }
                }
            }
            else
            {
                if(node.LeftNode != null)
                {
                    if (node.LeftNode.Value <= node.Value)
                    {
                        CheckIfBinarySearchTree(node.LeftNode, node.Value, leftGrandPrent, rightGrandParent, ref isBST);
                    }
                    else
                    {
                        isBST = false;
                    }
                }

                if (node.RightNode != null)
                {
                    if (node.RightNode.Value > node.Value)
                    {
                        CheckIfBinarySearchTree(node.RightNode, node.Value, leftGrandPrent, rightGrandParent, ref isBST);
                    }
                    else
                    {
                        isBST = false;
                    }
                }
            }
        }

        public Tree GetBinaryTree(int[] num)
        {
            var answer = new Tree();
            answer.Root = new TreeNode<int>(num[num.Length / 2]);

            PrepareTree(num, answer.Root);
            return answer;
        }

        private TreeNode<int> PrepareTree(int[] smallNum, TreeNode<int> answer)
        {
            int leftLength = (smallNum.Length / 2);
            if (leftLength > 0)
            {
                answer.LeftNode = new TreeNode<int>(smallNum[leftLength/2]);
                var newArray = new int[leftLength];
                Array.Copy(smallNum, newArray, leftLength);
                PrepareTree(newArray, answer.LeftNode);
            }

            var rightLength = smallNum.Length - (smallNum.Length / 2) - 1;
            if (rightLength > 0)
            {
                answer.RightNode = new TreeNode<int>(smallNum[smallNum.Length / 2 + rightLength / 2 + 1]);
                var newArray = new int[rightLength];
                Array.Copy(smallNum, (smallNum.Length / 2) + 1, newArray, 0, rightLength);
                PrepareTree(newArray, answer.RightNode);
            }

            return answer;
        }

        public List<LinkedList<TreeNode<int>>> GetListOfNodesAtEveryDepth(TreeNode<int> tree)
        {
            var answer = new List<LinkedList<TreeNode<int>>>();
            GetList(tree, answer, 1);
            return answer;
        }

        private void GetList(TreeNode<int> tree, List<LinkedList<TreeNode<int>>> answer, int count)
        {
            if (tree == null)
            {
                return;
            }
            if (count > answer.Count)
            {
                answer.Add(new LinkedList<TreeNode<int>>());
            }
            answer[count - 1].AddLast(tree);
            count = count + 1;
            GetList(tree.LeftNode, answer, count);
            GetList(tree.RightNode, answer, count);
        }

        public bool IsBalancedTree(TreeNode<int> treeNode)
        {
            int answer = BalancedOrNot(treeNode, 0);
            if (answer == -1)
                return false;
            return true;
        }

        private int BalancedOrNot(TreeNode<int> treeNode, int level)
        {
            if (treeNode == null)
            {
                return 0;
            }

            int leftDepth = 0;
            int rightDepth = 0;
            
            if(treeNode.LeftNode != null || treeNode.RightNode != null)
                level += 1;

            if (treeNode.LeftNode != null)
            {
                leftDepth = BalancedOrNot(treeNode.LeftNode, level);
            }
            if (treeNode.RightNode != null)
            {
                rightDepth = BalancedOrNot(treeNode.LeftNode, level);
            }
            if (leftDepth - rightDepth > 1 || rightDepth - leftDepth > 1)
            {
                return -1;
            }

            return level;
        }

        public bool IsValidBST(TreeNode<int> root)
        {
            return IsValidBST(root, null, null);
        }

        private bool IsValidBST(TreeNode<int> node, int? min, int? max)
        {
            if (node == null)
                return true;

            if((max.HasValue && node.Value > max) || (min.HasValue && node.Value <= min))
                return false;

            if (!IsValidBST(node.LeftNode, min, node.Value) || !IsValidBST(node.RightNode, node.Value, max))
                return false;
            
            return true;
        }
    }

    public class TreeNode<T>
    {
        public TreeNode(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
        public TreeNode<T> LeftNode { get; set; }
        public TreeNode<T> RightNode { get; set; }
    }
}
