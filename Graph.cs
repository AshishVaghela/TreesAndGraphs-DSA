using System;
using System.Collections.Generic;

namespace TreesAndGraphs
{
    public class Graph<T>
    {
        public T Value { get; set; }
        public List<Graph<T>> Chidren { get; set; }
        public enum State { NotVisited, Visited }
    }

    public class GraphActions
    {
        /*public List<string> BuildOrder(List<string> projects, List<string[]> dependencies)
        {
            Dictionary<string, List<string>> projectDependencies = new Dictionary<string, List<string>>();
            HashSet<string> visitedProjects = new HashSet<string>();
            List<string> order = new List<string>();

            foreach (var project in projects)
            {
                projectDependencies.Add(project, new List<string>());
            }

            foreach (var dependency in dependencies)
            {
                projectDependencies[dependency[0]].Add(dependency[1]);
            }

            foreach (var key in projectDependencies.Keys)
            {
                getBuildOrder(key, projectDependencies, visitedProjects, order);
            }
            order.Reverse();
            return order;
        }

        private void getBuildOrder(string project, Dictionary<string, List<string>> projectDependencies, HashSet<string> visitedProjects, List<string> order)
        {
            if (visitedProjects.Contains(project))
            {
                return;
            }

            foreach (var pd in projectDependencies[project])
            {
                getBuildOrder(pd, projectDependencies, visitedProjects, order);
            }
            order.Add(project);
            visitedProjects.Add(project);
        }

        public bool HasRouteBetweenNodes(Graph start, Graph destination)
        {
            var q = new Queue<Graph>();
            var setData = new HashSet<Graph>();
            q.Enqueue(start);
            setData.Add(start);
            while(q.Count != 0)
            {
                var current = q.Dequeue();
                if (current == destination)
                {
                    return true;
                }
                foreach (var child in current.Chidren)
                {
                    if (!setData.Contains(child))
                    {
                        q.Enqueue(child);
                    }
                }
            }
            return false;
        }

        public bool HasRouteBWNodes(Graph start, Graph destination)
        {
            Queue<Graph> q = new Queue<Graph>();
            var visitedNodes = new HashSet<Graph>();
            q.Enqueue(start);
            visitedNodes.Add(start);

            while(q.Count > 0)
            {
                var temp = q.Dequeue();
                if (temp == destination)
                {
                    return true;
                }
                foreach (var t in temp.Chidren)
                {
                    if(!visitedNodes.Contains(t))
                        q.Enqueue(t);
                }
            }

            return false;
        }
        */
        enum status { NotStarted, Visiting, Visited };
        /*public List<string> BuildOrder(List<string> projects, List<string[]> dependecies)
        {
            List<string> rightOrder = new List<string>();

            List<Graph<string>> graphs = new List<Graph<string>>();

            Dictionary<string, List<string>> dependenciesInDictionary = new Dictionary<string, List<string>>();

            Dictionary<string, status> visitList = new Dictionary<string, status>();
            
            foreach (var p in projects)
            {
                dependenciesInDictionary.Add(p, new List<string>());
                visitList.Add(p, status.NotStarted);
            }

            foreach (var d in dependecies)
            {
                dependenciesInDictionary[d[0]].Add(d[1]);
            }

            foreach (string project in projects)
            {
                BuildDependencies(project, dependenciesInDictionary, visitList);
            }
        }

        private void BuildDependencies(string project, Dictionary<string, List<string>> dependenciesInDictionary, Dictionary<string, status> visitList)
        {
            if (visitList[project] == status.NotStarted)
            {
                visitList[project] = status.Visiting;
                var t = dependenciesInDictionary[project];
                foreach (string d in t)
                {
                    BuildDependencies(d, dependenciesInDictionary, visitList);
                }
                visitList[project] = status.Visited;
            }
            else if (visitList[project] == status.Visiting)
            {
                throw new Exception();
            }
        }*/

        public List<string> BuildOrder(List<string> projects, List<string[]> dependencies)
        {
            Dictionary<string, status> projs = new Dictionary<string, status>();

            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            foreach (var p in projects)
            {
                projs.Add(p, status.NotStarted);
                dict.Add(p, new List<string>());
            }

            foreach (var d in dependencies)
            {
                dict[d[1]].Add(d[0]);
            }

            List<string> orderList = new List<string>();

            foreach (var p in projects)
            {
                BuildOrderThroughDepen(projs, dict, p, orderList);
            }

            return orderList;
        }

        private void BuildOrderThroughDepen(Dictionary<string, status> projs, Dictionary<string, List<string>> dict, string project, List<string> list)
        {
            if (projs[project] == status.Visited)
            {
                return;
            }

            foreach (var p in dict[project])
            {
                if (projs[p] == status.NotStarted)
                {
                    projs[project] = status.Visiting;
                    BuildOrderThroughDepen(projs, dict, p, list);
                }
                else if (projs[p] == status.Visiting)
                {
                    throw new Exception();
                }
            }

            projs[project] = status.Visited;
            list.Add(project);
        }
    }
}
