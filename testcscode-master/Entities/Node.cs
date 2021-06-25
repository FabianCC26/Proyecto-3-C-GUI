using System;
using System.Collections.Generic;
namespace graph.Entities
{
    public class Node
    {
        private static int currentId=0;
        private int id;

      

        private String entity = "hola";

        private int outDegree = 0;

        private int inDegree = 0;

        public Node(){
            this.id= Node.currentId++;
    
        }

        public Node(int id)
        {
            this.id=id;
        }

        public string Entity
        {
            get=>entity; 
            set => this.entity=value;
        }
        public int Id 
        { 
            get => id; 
            set => id = value; 
        }



        public int InDegree
        {
            get=>inDegree; 
            set => this.inDegree=value;
        }

        public int OutDegree
        {
            get=>outDegree; 
            set => this.outDegree=value;
        }


    }
}