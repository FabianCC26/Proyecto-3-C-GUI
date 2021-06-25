using System;
using System.Collections.Generic;
using graph.Database;
using graph.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace graph.Controllers{



    [ApiController]
    [Route("api/Graph/{id}/[controller]")]

     public class EdgeController: Controller{

         private readonly ILogger<EdgeController> _logger;

        public EdgeController(ILogger<EdgeController> logger)
        {
            _logger = logger;
        }


        [HttpPost]

        public IActionResult AddNewEdge(int id, int startNode, int endNode, int weight){

            var getGraphId=GraphDB.Instance.GetGraph(id);



            for(int i=0; i < getGraphId.Nodes.Count ;i++){

                
                if(getGraphId.Nodes[i].Id == startNode){


                    for (int k=0; k < getGraphId.Nodes.Count ;k++){



                        if(getGraphId.Nodes[k].Id == endNode){



                            for (int j=0; j < getGraphId.Nodes.Count ;j++){


                                if(getGraphId.Nodes[j].Id == startNode){

                                    getGraphId.Nodes[j].OutDegree +=1;


                                        for (int l=0; l < getGraphId.Nodes.Count ;l++){


                                            if(getGraphId.Nodes[l].Id == endNode){

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





        [HttpDelete("id")]
        public IActionResult Delete(int id,int idEdge)
        {
            var getGraphId=GraphDB.Instance.GetGraph(id);
    

            if(getGraphId==null){
                return NotFound();


            }else
            {   
                
                for(int i=0; i < getGraphId.Edges.Count ;i++)
                {
                    if(getGraphId.Edges[i].Id==idEdge)

                    {

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


                        getGraphId.Edges.RemoveAt(i);
                        return Ok();

                    }

                        
                }

                return NotFound();
                
            }     
                 
        }

    }
}












