using System;
using System.Collections.Generic;
using graph.Entities;

namespace graph.Database
{
    public class GraphDB
    {
        /* 
        El singleton que hizo el profe para asi solo tener una sola referencia de la lista que contiene todos los grafos
         */
        private static GraphDB instance;
        private static readonly object padlock = new object();
        private List<Graph> graphs = new List<Graph>();
        private GraphDB()
        {
            this.graphs = new List<Graph>();
        }

        public void addGraph()
        {
            this.graphs.Add(new Graph());
        }

        public Graph GetGraph(int id)
        {
            foreach (Graph g in graphs)
            {
                if (g.Id == id)
                {
                    return g;
                }
            }
            return null;
        }
        //El singleton
        public static GraphDB Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GraphDB();
                    }
                    return instance;
                }
            }
        }
        public List<Graph> GetGraphs
        {
            get
            {
                return this.graphs;
            }
        }
    }
}