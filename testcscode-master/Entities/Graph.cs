using System;
using System.Collections.Generic;

/*
#############################################################################
#
#                       Instituto Tecnológico de Costa Rica
#
#                   Área Academica de Ingeniería en Computadores
#
#   Curso: CE-1103 Algoritmos y Estructuras de  Datos 1
#
#   Programa: C#
#
#   Profesor: Jose Isaac Ramirez Herrera
#
#   Autores: Fabián Castillo Cerdas, 
#         Irene Garzona Moya, 
#         Erick Daniel Obando Venegas, 
#            José Andrés Quirós Guzmán, 
#            José Pablo Ramos Madrigal
#
#   Fecha de última modificación: 25/06/2021
#
#############################################################################
*/



namespace graph.Entities
{
    
    public class Graph
    {
        
        //La clase Grafo
         
        private static int currentId=0;
        public int id;
        private List<Node> nodes; //Lista de Nodos de cada grafo
        private List<Edge> edges; //Lista de Aristas de cada grafo



        public Graph graph{get; set;}
        public Graph(){
            this.id= currentId++; //Asigna un ID a los grafos
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


        //Se añaden los Nodos y las Aristas a sus respectivas listas en el grafo

        public List<Node> Nodes { get => nodes;}
        public List<Edge> Edges { get => edges;}
    }

}