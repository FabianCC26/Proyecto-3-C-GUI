using System;
using System.Collections.Generic;
using graph.Database;
using graph.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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


        //Post
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

        //Elimina todos los nodos 
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



        // Elimina nodo respecto al id
        [HttpDelete("id")]
        public IActionResult Delete(int id,int idNode)
        {
            var getGraphId=GraphDB.Instance.GetGraph(id);
    

            if(getGraphId==null){
                return NotFound();


            }else
            {   
                
                for(int i=0; i < getGraphId.Nodes.Count ;i++)
                {
                    if(getGraphId.Nodes[i].Id==idNode)
                    {
                        

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

                        

                            getGraphId.Edges.RemoveAt(startEdgeList[v]);



                        }



                        for(int v=0; v < endEdgeList.Count ;v++){


                            for(int h=0; h < getGraphId.Nodes.Count ;h++){

                                if (getGraphId.Edges[endEdgeList[v]].StartNode == getGraphId.Nodes[h].Id){

                                    getGraphId.Nodes[h].OutDegree -= 1;        


                                }

                            }

                            getGraphId.Edges.RemoveAt(endEdgeList[v]);


                        }

                        getGraphId.Nodes.RemoveAt(i);
                        return Ok();
                    }
                
                }
                return NotFound();
                
            }

        }



        
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
