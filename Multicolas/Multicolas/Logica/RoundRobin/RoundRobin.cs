using Multicolas.Logica.General;
using Multicolas.Shared;
using Multicolas.Pages;

namespace Multicolas.Logica.RoundRobin
{
    public class RoundRobin
    {

        private BloqueInicial bloqueControl;
        private EstadoEjecucionR estadoEjecucion;
        private EstadoBloqueo estadoBloqueo;
        private int quantumAlterno = 0;
        private int quantum = 2;
        private Pages.Index ind;

        public RoundRobin()

        {
            //ind = i;
            bloqueControl = new BloqueInicial();
            estadoEjecucion = new EstadoEjecucionR();
        }


        public async Task IniciarEjecucion()
        {
            EstadoInicial.InicialProceso.Add(new Proceso { Name = "A", Rafaga = 5, TiempoLlegada = 3 });
            EstadoInicial.InicialProceso.Add(new Proceso { Name = "b", Rafaga = 3, TiempoLlegada = 5 });
            EstadoInicial.InicialProceso.Add(new Proceso { Name = "c", Rafaga = 4, TiempoLlegada = 1 });
            EstadoInicial.InicialProceso.Add(new Proceso { Name = "d", Rafaga = 4, TiempoLlegada = 0 });

            EstadoInicial.ProcesosListos = EstadoInicial.OrganizarLista(EstadoInicial.InicialProceso);


            while (EstadoInicial.ProcesosListos.Count > 0)
            {
                Proceso siguiente = EstadoInicial.ProcesosListos.Dequeue();

                siguiente.TiempoComienzo = EstadoInicial.TiempoGlobal;

                //await Task.Delay(1500);

                quantumAlterno = 0;
                // Historial de tiempos
                if ((siguiente.TiempoComienzoH.Contains(siguiente.TiempoComienzo) == false) ||
                    (siguiente.TiempoComienzoH.Contains(EstadoInicial.TiempoGlobal)))
                {
                    siguiente.TiempoComienzoH.Add(EstadoInicial.TiempoGlobal);
                }

                if (siguiente.RafagaH.Contains(siguiente.Rafaga) == false)
                {
                    siguiente.RafagaH.Add(siguiente.Rafaga);

                }

                while (siguiente.RafagaTemporal > 0 && EstadoInicial.ProcesoBloqueado == false && quantumAlterno <= quantum)
                {
                    await estadoEjecucion.Ejecutar(siguiente);
                    EstadoInicial.TiempoGlobal++;

                    //await Task.Delay(1500);
                   
                   // ind.cambiarestado();

                    quantumAlterno++;
                    Console.WriteLine($"Quantum: {quantumAlterno}");
                }
                if (EstadoInicial.ListaEjecucion.Contains(siguiente))
                {
                    // Esta varable controla que el proceso calcule el t. final con la rafaga restante
                    siguiente.Expulsado = true;
                    //siguiente.FueBloqueado = false;
                    siguiente.RafagaH.Add(siguiente.RafagaTemporal);
                    siguiente.TiempoFinalH.Add(EstadoInicial.TiempoGlobal);
                    int retorno = EstadoInicial.TiempoGlobal - siguiente.TiempoLlegada;
                    int espera = retorno - (siguiente.Rafaga - siguiente.RafagaTemporal);
                    siguiente.RetornoH.Add(retorno);
                    siguiente.EsperaH.Add(espera);
                    EstadoInicial.ListaEjecucion.Remove(siguiente);
                    EstadoInicial.ProcesosListos.Enqueue(siguiente);
                }
                EstadoInicial.ProcesoBloqueado = false;
                siguiente.Bloqueado = false;


            }

        }



        public async Task<Proceso> IniciaRoundRobin(Proceso procesoEntrante)
        {
            //Proceso siguiente = EstadoInicial.ProcesosListos.Dequeue();
            Proceso siguiente = procesoEntrante;
            siguiente.TiempoComienzo = EstadoInicial.TiempoGlobal;

            //await Task.Delay(1500);

            quantumAlterno = 0;
            // Historial de tiempos
            if ((siguiente.TiempoComienzoH.Contains(siguiente.TiempoComienzo) == false) ||
                (siguiente.TiempoComienzoH.Contains(EstadoInicial.TiempoGlobal)))
            {
                siguiente.TiempoComienzoH.Add(EstadoInicial.TiempoGlobal);
            }

            if (siguiente.RafagaH.Contains(siguiente.Rafaga) == false)
            {
                siguiente.RafagaH.Add(siguiente.Rafaga);

            }

            while (siguiente.RafagaTemporal > 0 && EstadoInicial.ProcesoBloqueado == false && quantumAlterno <= quantum)
            {
                await estadoEjecucion.Ejecutar(siguiente);
                EstadoInicial.TiempoGlobal++;
                EstadoInicial.ProcesoGrafico.Add(new ProcesoUI { Id = siguiente.Name, Posicion = EstadoInicial.TiempoGlobal, Color = "green" });
                Console.WriteLine(siguiente.Name + " ProcesoGrafico");
                await Task.Delay(1500);

              
              // StateHasChanged();

                quantumAlterno++;
                Console.WriteLine($"Quantum: {quantumAlterno}");
            }
            if (EstadoInicial.ListaEjecucion.Contains(siguiente))
            {
                // Esta varable controla que el proceso calcule el t. final con la rafaga restante
                siguiente.Expulsado = true;
                //siguiente.FueBloqueado = false;
                siguiente.RafagaH.Add(siguiente.RafagaTemporal);
                siguiente.TiempoFinalH.Add(EstadoInicial.TiempoGlobal);
                int retorno = EstadoInicial.TiempoGlobal - siguiente.TiempoLlegada;
                int espera = retorno - (siguiente.Rafaga - siguiente.RafagaTemporal);
                siguiente.RetornoH.Add(retorno);
                siguiente.EsperaH.Add(espera);
                EstadoInicial.ListaEjecucion.Remove(siguiente);
                EstadoInicial.ProcesosListos.Enqueue(siguiente);
            }
            EstadoInicial.ProcesoBloqueado = false;
            siguiente.Bloqueado = false;
            Console.WriteLine("Hola" + siguiente.Name);
            return siguiente;
        }
    }
}

