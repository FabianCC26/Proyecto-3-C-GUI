using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using graph.Entities;
using Microsoft.EntityFrameworkCore;
using graph.Database;


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
#            Irene Garzona Moya, 
#            Erick Daniel Obando Venegas, 
#            José Andrés Quirós Guzmán, 
#            José Pablo Ramos Madrigal
#
#   Fecha de última modificación: 25/06/2021
#
#
#############################################################################
*/



namespace graph.Controllers
{

    [ApiController]
    [Route("api/Graph/{id}/[controller]")]
    public class DijkstraController : ControllerBase
    {


        private readonly ILogger<DijkstraController> _logger;
        public DijkstraController(ILogger<DijkstraController> logger)
        {
            _logger = logger;
        }



        //Obtiene el camino mas corto entre 2 nodos utilizando el algoritmo de Dijkstra
        [HttpGet]
        public IActionResult DijkstraAlgoritm(int id,int Start, int End)
        {


            //Variables necesarias para realiazr el argoritmo
            var getGraphId=GraphDB.Instance.GetGraph(id);


            int inicio = Start;
            int final = End;
            int distancia = 0;
            int cantNodos = getGraphId.Nodes.Count;
            int actual = 0;


            //0 - visidado
            //1 - Distancia
            //2 - Padre


            //Se crea una matríz la cual va a guardar los datos necesarios de cada nodo

            int [,] tabla = new int[cantNodos,3];


            for (int n = 0; n < cantNodos; n++)
            {
                tabla[n,0] = 0;
                tabla[n,1] = int.MaxValue;
                tabla[n,2] = 0;
            }
            tabla[inicio,1] = 0;


            actual = inicio;





            do{

                //Se establece el nodo de inicio como visitado

                tabla[actual,0] =1;

                //Se realiza la operacion por cada nodo existente
                for (int i =0; i < cantNodos; i++)
                {
                    //Verifica si algun nodo está conectado al nodo de inicio
                    if(getGraphId.Nodes[i].InDegree != 0){
                        

                        //Se recorre la lista de Aristas
                        for (int j = 0; j < getGraphId.Edges.Count; j++)
                        {

                            //Verifica si una arista conecta el nodo en cuastion con el nodo de inicio
                            if(getGraphId.Edges[j].EndNode == i && getGraphId.Edges[j].StartNode == actual){

                                
                                distancia = getGraphId.Edges[j].Weight + tabla[actual,1];

                                //Se sustituye la distancia en la tabla
                                if(distancia < tabla[i,1])
                                {
                                    tabla[i,1] = distancia;

                                    tabla[i,2] = actual;

                                }


                            }
                            
                        }
                        
                    }

                }

                //Verififica cual es el nodo con la menor distancia y que no haya sido visitado
                int indiceMenor = -1;
                int distanciMenor = int.MaxValue; 

                for (int i = 0; i < cantNodos; i++)
                {
                    if(tabla[i,1] < distanciMenor && tabla[i,0] == 0)
                    {
                        indiceMenor = i;
                        distanciMenor = tabla[i,1];

                    }

                }


                actual = indiceMenor;



            }while (actual != -1);



            //Se añaden los nodos del camino mas corto a una lista
            List<int> ruta  = new List<int>();
            int nodo = final;

            while(nodo != inicio)
            {
                ruta.Add(nodo);
                nodo = tabla[nodo,2];

            }

            ruta.Add(inicio);
            
            ruta.Reverse();

            //Con respecto a la lista de Nodos del camino mas corto, se crea un array con los objetos Nodo respectivos

            object[] arrayNodes = new object[ruta.Count];


            



            for(int i=0; i < ruta.Count; i++){

                arrayNodes[i] = getGraphId.Nodes[ruta[i]];

                
            }

            String DistanciaTotal = "Distancia total:" + distancia.ToString();

            List<object> arrayNodesDistance = arrayNodes.ToList();

            arrayNodesDistance.Add(DistanciaTotal);


            

            
            //retorna un array con los objeto Nodo que conforman el camino mas corto



            return Ok(arrayNodesDistance);

            
        }

    }
}