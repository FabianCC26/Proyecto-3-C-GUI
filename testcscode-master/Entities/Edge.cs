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
#
#############################################################################
*/


namespace graph.Entities
{
    public class Edge
    {


        //declaracion de variables para la creacion de grafos
        private static int currentId = 0;
        private int id;

        
        private int startNode = 0;
        private int endNode = 0;
        private int weight = 0;


        //Metodo creador de Aristas
        public Edge(int startNode, int endNode, int weight){
            this.id= Edge.currentId++;
            this.startNode = startNode;
            this.endNode = endNode;
            this.weight = weight;
        }

        

        //Se añaden los atributos a los objetos Arista
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