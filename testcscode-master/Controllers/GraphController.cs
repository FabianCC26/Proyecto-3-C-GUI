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
    [Route("api/[controller]")]
    public class GraphController : ControllerBase
    {
        private readonly ILogger<GraphController> _logger;
        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger;
        }


        //Retorna todos los grafos existentes
        [HttpGet]
        public List<Graph> GetAllGraphs()
        {
            return GraphDB.Instance.GetGraphs;
        }
        [HttpGet("{id}")]
        public IActionResult GetGraph(int id)
        {
             var getGraphId = GraphDB.Instance.GetGraph(id);
             if (getGraphId == null){
                return NotFound();
            }
            return Ok(getGraphId);
        }


        //Crea un nuevo grafo
        [HttpPost]
        public IActionResult Post()
        {
            GraphDB.Instance.addGraph();
            return Ok();
        }




        //Elimina todos los grafos
        [HttpDelete]
        public IActionResult DeleteAllGraphs()
        {
            var ListOfGraphs= GraphDB.Instance.GetGraphs;
            if(ListOfGraphs==null)
            {
                return NotFound();
            }else
            {
                ListOfGraphs.Clear();
                return Ok();
            }  
        }





        //Elimina el grafo con respecto al Id 
        [HttpDelete("{id}")]
        public IActionResult DeleteIdGraph(int id)
        {
            var getGraphId = GraphDB.Instance.GetGraph(id);
            var ListOfGraphs= GraphDB.Instance.GetGraphs;
            
             if (ListOfGraphs == null){
                return NotFound();
            }else
            {
                ListOfGraphs.Remove(getGraphId);
                return Ok();
            }
            
        }
    }
}