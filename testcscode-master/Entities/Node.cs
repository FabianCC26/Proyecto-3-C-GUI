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
#         José Andrés Quirós Guzmán, 
#         José Pablo Ramos Madrigal
#
#   Fecha de última modificación: 25/06/2021
#
#
#
#############################################################################
*/


namespace graph.Entities
{
    public class Node
    {


        //Metodo contructor de Nodos
        private static int currentId=0;
        private int id;

      

        private String entity = "hola";

        private int outDegree = 0;

        private int inDegree = 0;


        //Asigna IP a los Nodos

        public Node(){
            this.id= Node.currentId++;
    
        }

        public Node(int id)
        {
            this.id=id;
        }


        //Se le asigna los siguientes atributos  al los objetos Nodo

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