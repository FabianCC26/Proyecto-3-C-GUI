using System;
using System.Collections.Generic;
using graph.Database;
using graph.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
#############################################################################
*/



namespace graph.Controllers{



    [ApiController]
    [Route("api/Graph/{id}/[controller]")]

     public class EdgeController: Controller{

         private readonly ILogger<EdgeController> _logger;

        public EdgeController(ILogger<EdgeController> logger)
        {
            _logger = logger;
        }




        //Crea una nueva Arista
        [HttpPost]

        public IActionResult AddNewEdge(int id, int startNode, int endNode, int weight){

            var getGraphId=GraphDB.Instance.GetGraph(id);


            //recorre la lista de nodos
            for(int i=0; i < getGraphId.Nodes.Count ;i++){

                //Verifica cuales son los nodos relacionados con la arista que se crea
                if(getGraphId.Nodes[i].Id == startNode){

                    
                    for (int k=0; k < getGraphId.Nodes.Count ;k++){


                        //Verifica si la arista termina en un nodo, si es el caso al nodo se le suma 1 al atributo de InDegree
                        if(getGraphId.Nodes[k].Id == endNode){



                            for (int j=0; j < getGraphId.Nodes.Count ;j++){


                                if(getGraphId.Nodes[j].Id == startNode){
                                    
                                    //suma 1 al InDegree de dicho nodo
                                    getGraphId.Nodes[j].OutDegree +=1;


                                        for (int l=0; l < getGraphId.Nodes.Count ;l++){

                                            //Verifica si la arista empieza en un nodo, si es el caso al nodo se le suma 1 al atributo de OutDegree
                                            if(getGraphId.Nodes[l].Id == endNode){
                                            

                                                //suma 1 al OutDegree de dicho nodo
                                                getGraphId.Nodes[l].InDegree +=1;
                                                getGraphId.Edges.Add(new Edge(startNode,endNode,weight));
                                                return Ok();

                                            }

                                        }
                                        return NotFound();
                                }

                            } 
                            return NotFound();

                        }

                    }
                    return NotFound();

                }

            }
            return NotFound();
        }



        //Obtiene las Aristas dde un Grafo en especifico
        [HttpGet]
        public IActionResult GetActionResult(int id)
        {   
            
            var getGraphId=GraphDB.Instance.GetGraph(id);
            	
            if(getGraphId==null)
            {
                return NotFound();
            }
            return Ok(getGraphId.Edges);
        }




        //Borra todas las Aristas de un grafo
        [HttpDelete]
        public IActionResult DeleteAllEdges(int id)
        {
            var  getGraphId=GraphDB.Instance.GetGraph(id);
            if(getGraphId==null)
            {
                return NotFound(); 
            }else
            {

                for (int i=0; i < getGraphId.Nodes.Count ;i++){


                    getGraphId.Nodes[i].InDegree = 0;
                    getGraphId.Nodes[i].OutDegree = 0;
                    

                }    


                getGraphId.Edges.Clear();
                return Ok();
            }
        }




        //Actualiza los atributos de una Arista
        [HttpPut("id")]

        public IActionResult UpdateEdgeValues(int id,int idEdge,int newStart,int newEnd,int newWeight){

            var getGraphId=GraphDB.Instance.GetGraph(id);


            if(getGraphId==null){
                return NotFound();

            }else
            {   
                
                for(int i=0; i < getGraphId.Edges.Count ;i++)
                {
                    if(getGraphId.Edges[i].Id==idEdge)
                    {
                        getGraphId.Edges[i].Weight = newWeight;
                        getGraphId.Edges[i].StartNode = newStart;
                        getGraphId.Edges[i].EndNode = newEnd;
     

                        return Ok();
                    }
                }
                
                return NotFound();
                
            }

        }




        //Borra una Arista en especifico segun su Id
        [HttpDelete("id")]
        public IActionResult Delete(int id,int idEdge)
        {
            var getGraphId=GraphDB.Instance.GetGraph(id);
    
            //Verifica que el grafo exista
            if(getGraphId==null){
                return NotFound();


            }else
            {   
                //Recorre la lista de Aristas
                for(int i=0; i < getGraphId.Edges.Count ;i++)
                {

                    //Verifica cual es la Arista que se va a eliminar
                    if(getGraphId.Edges[i].Id==idEdge)

                    {


                        /*Esta parte se encarga de actualizar los InDegree y Outdegree de los nodos 
                        los cuales estaban relacionados a la Arista que se eliminará*/

                        int var1 = getGraphId.Edges[i].StartNode;
                        



                        for (int j=0; j < getGraphId.Nodes.Count ;j++){

                            if (getGraphId.Nodes[j].Id == var1){

                                getGraphId.Nodes[j].OutDegree -= 1;

                            }

                            


                        }


                        int var2 = getGraphId.Edges[i].EndNode;

                        for (int k=0; k < getGraphId.Nodes.Count ;k++){



                            if (getGraphId.Nodes[k].Id == var2){

                                getGraphId.Nodes[k].InDegree -= 1;         

                            }
                        }

                        //Elimia la Arista en cuestion
                        getGraphId.Edges.RemoveAt(i);
                        return Ok();

                    }

                        
                }

                return NotFound();
                
            }     
                 
        }

    }
}












