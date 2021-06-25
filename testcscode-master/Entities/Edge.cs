using System;
using System.Collections.Generic;
namespace graph.Entities
{
    public class Edge
    {
        private static int currentId = 0;
        private int id;


        private int startNode = 0;
        private int endNode = 0;
        private int weight = 0;



        public Edge(int startNode, int endNode, int weight){
            this.id= Edge.currentId++;
            this.startNode = startNode;
            this.endNode = endNode;
            this.weight = weight;
        }

        


        public int Id 
        { 
            get => id; 
            set => Id = value; 
        }


    
         public int StartNode{
            get => startNode;
            set => this.startNode=value;
        }

           public int EndNode{
            get => endNode;
            set => this.endNode=value;
        }


        public int Weight{
            get => weight;
            set => this.weight=value;
        }


    

    }
}