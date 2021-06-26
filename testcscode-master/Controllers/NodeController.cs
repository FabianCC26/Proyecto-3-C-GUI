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
#
#############################################################################
*/







namespace graph.Controllers{
    
    [ApiController]
    [Route("api/Graph/{id}/[controller]")]
    public class NodeController: Controller
    {
        
        private readonly ILogger<NodeController> _logger;

        public NodeController(ILogger<NodeController> logger)
        {
            _logger = logger;
        }


        //Añade un un nuevo nodo en un grafo especifico
        [HttpPost]
        public IActionResult AddNewNode(int id)

        {
            var listofGraphs=GraphDB.Instance.GetGraphs;
            var getGraphId=GraphDB.Instance.GetGraph(id);


            for(int i=0; i < listofGraphs.Count ;i++){

                if(listofGraphs[i].Id == id){

                    getGraphId.Nodes.Add(new Node());
                    return Ok();

                }

            }

            return NotFound();

        }

        //Retorna todos los nodos de un grafo en especifico
        [HttpGet]
        public IActionResult GetActionResult(int id)
        {   
            
            var getGraphId=GraphDB.Instance.GetGraph(id);
            	
            if(getGraphId==null)
            {
                return NotFound();
            }
            return Ok(getGraphId.Nodes);
        }

        //Elimina todos los nodos de un grafo especifico
        [HttpDelete]
        public IActionResult DeleteAllNodes(int id)
        {
            var  getGraphId=GraphDB.Instance.GetGraph(id);
            if(getGraphId==null)
            {
                return NotFound(); 
            }else
            {             
                getGraphId.Nodes.Clear();
                getGraphId.Edges.Clear();
                return Ok();
            }
        }



        // Elimina un nodo de un grafo según la id del Nodo
        [HttpDelete("id")]
        public IActionResult Delete(int id,int idNode)
        {
            var getGraphId=GraphDB.Instance.GetGraph(id);
    
            //verifica que el grafo exista
            if(getGraphId==null){
                return NotFound();


            }else
            {   
                //recorre la lista de nodos en busca del nodo a eliminar

                for(int i=0; i < getGraphId.Nodes.Count ;i++)
                {

                    //Verifica que el nodo exista
                    if(getGraphId.Nodes[i].Id==idNode)
                    {
                        
                        /*Esta sección se encarga de eliminar las aristas relacionadas con el nodo que se borrará, 
                        ademas modifica el inDegree y el outDregree de los nodos relacionados con dicha arista*/
                        


                        int thisNode = getGraphId.Nodes[i].Id;

                        int counterofEdges = getGraphId.Edges.Count;

                        List<int> startEdgeList = new List<int>();
                        List<int> endEdgeList = new List<int>();

                        


                        for(int j=0; j < counterofEdges   ;j++){


                            if (getGraphId.Edges[j].StartNode == thisNode){


                                startEdgeList.Add(j);
        
                            }


            
                        }


                        
                        for(int j=0; j < counterofEdges   ;j++){


                            if (getGraphId.Edges[j].EndNode == thisNode){


                                endEdgeList.Add(j);
        
                            }


            
                        }




                        int aux;

                        int selc = startEdgeList.Count;


                        if(startEdgeList.Count != 0)
                        {
                            for (int b= 0; b < selc; ++b)
                            {
                                for (int j = b + 1; j < selc; ++j)
                                {
                                    if (startEdgeList[b] < startEdgeList[j])
                                    {
                                    aux = startEdgeList[b];
                                    startEdgeList[b] = startEdgeList[j];
                                    startEdgeList[j] = aux;
                                    }
                                }
                            }
                        }


                        int eelc = endEdgeList.Count;


                        if(endEdgeList.Count != 0)
                        {
                            for (int b= 0; b < eelc; ++b)
                            {
                                for (int j = b + 1; j < eelc; ++j)
                                {
                                    if (endEdgeList[b] < endEdgeList[j])
                                    {
                                    aux = endEdgeList[b];
                                    endEdgeList[b] = endEdgeList[j];
                                    endEdgeList[j] = aux;
                                    }
                                }
                            }
                        }




                        for(int v=0; v < startEdgeList.Count ;v++){


                            for(int h=0; h < getGraphId.Nodes.Count ;h++){

                                if (getGraphId.Edges[startEdgeList[v]].EndNode == getGraphId.Nodes[h].Id){

                                    getGraphId.Nodes[h].InDegree -= 1;        


                                }

                            }

                        
                            //remueve la arita que sale del Nodo a eliminar
                            getGraphId.Edges.RemoveAt(startEdgeList[v]);



                        }



                        for(int v=0; v < endEdgeList.Count ;v++){


                            for(int h=0; h < getGraphId.Nodes.Count ;h++){

                                if (getGraphId.Edges[endEdgeList[v]].StartNode == getGraphId.Nodes[h].Id){

                                    getGraphId.Nodes[h].OutDegree -= 1;        


                                }

                            }


                            //remueve la arita que entra al Nodo a eliminar
                            getGraphId.Edges.RemoveAt(endEdgeList[v]);


                        }

                        //Elimina el Nodo en cuastion
                        getGraphId.Nodes.RemoveAt(i);
                        return Ok();
                    }
                
                }
                return NotFound();
                
            }

        }



        //Modifica la entidad almacenada de un nodo en un grafo especifico según la id del nodo
        [HttpPut("id")]

        public IActionResult UpdateNodeValue(int id,int idNode,string newValue){

            var getGraphId=GraphDB.Instance.GetGraph(id);


            if(getGraphId==null){
                return NotFound();

            }else
            {   
                
                for(int i=0; i < getGraphId.Nodes.Count ;i++)
                {
                    if(getGraphId.Nodes[i].Id==idNode)
                    {
                        getGraphId.Nodes[i].Entity = newValue;
     

                        return Ok();
                    }
                }
                
                return NotFound();
                
            }

        }
        
    }
}
