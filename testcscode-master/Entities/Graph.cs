using System;
using System.Collections.Generic;
namespace graph.Entities
{
    
    public class Graph
    {
        /* 
        La clase Grafo
         */
        private static int currentId=0;
        public int id;
        private List<Node> nodes; //Lista de Nodos de cada grafo
        private List<Edge> edges; //Lista de Aristas de cada grafo



        public Graph graph{get; set;}
        public Graph(){
            this.id= currentId++; //El Id
            this.nodes = new List<Node>();
            this.edges = new List<Edge>();
        }

        public Graph(int id)
        {
            this.id=id;
        }

        public int Id
        {
            get{return this.id;}
            set {this.id=value;}
        }

        public List<Node> Nodes { get => nodes;}
        public List<Edge> Edges { get => edges;}
    }

}