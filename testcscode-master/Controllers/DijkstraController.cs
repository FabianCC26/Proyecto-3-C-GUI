using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using graph.Entities;
using Microsoft.EntityFrameworkCore;
using graph.Database;


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


        [HttpGet]
        public IActionResult DijkstraAlgoritm(int id,int Start, int End)
        {



            var getGraphId=GraphDB.Instance.GetGraph(id);


            int inicio = Start;
            int final = End;
            int distancia = 0;
            int cantNodos = getGraphId.Nodes.Count;
            int actual = 0;


            //0 - visidado
            //1 - Distancia
            //2 - Padre



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


                tabla[actual,0] =1;

                for (int i =0; i < cantNodos; i++)
                {
                    if(getGraphId.Nodes[i].InDegree != 0){

                        for (int j = 0; j < getGraphId.Edges.Count; j++)
                        {
                            if(getGraphId.Edges[j].EndNode == i && getGraphId.Edges[j].StartNode == actual){

                                distancia = getGraphId.Edges[j].Weight + tabla[actual,1];


                                if(distancia < tabla[i,1])
                                {
                                    tabla[i,1] = distancia;

                                    tabla[i,2] = actual;

                                }


                            }
                            
                        }
                        
                    }

                }


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


            List<int> ruta  = new List<int>();
            int nodo = final;

            while(nodo != inicio)
            {
                ruta.Add(nodo);
                nodo = tabla[nodo,2];

            }

            ruta.Add(inicio);
            
            ruta.Reverse();

            Node[] arrayNodes = new Node[ruta.Count];



            for(int i=0; i < ruta.Count; i++){

                arrayNodes[i] = getGraphId.Nodes[ruta[i]];

                Console.WriteLine(ruta[i]);
                
            }

            



            Console.WriteLine(distancia);

            return Ok(distancia);

            
            


            
        }

    }
}