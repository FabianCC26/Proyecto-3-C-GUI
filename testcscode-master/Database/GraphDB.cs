using System;
using System.Collections.Generic;
using graph.Entities;

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



namespace graph.Database
{
    public class GraphDB
    {
      

        //Singleton para solo tener una instancia unica de la lista de grafos


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