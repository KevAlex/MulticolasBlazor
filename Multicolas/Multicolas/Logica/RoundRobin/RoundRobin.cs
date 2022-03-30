using Multicolas.Logica.General;
using Multicolas.Shared;

namespace Multicolas.Logica.RoundRobin
{
    public class RoundRobin
    {

        private BloqueInicial bloqueControl;
        private EstadoEjecucionR estadoEjecucion;
        private EstadoBloqueo estadoBloqueo;
        private int quantumAlterno = 0;
        private int quantum = 2;

        public RoundRobin()

        {
            //ind = i;
            bloqueControl = new BloqueInicial();
            estadoEjecucion = new EstadoEjecucionR();
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
                if ((EstadoInicial.ProcesosListosFO.Peek().envejecimiento <= 0)&&(EstadoInicial.ProcesosListosFO.Count>1))
                {
                    Proceso cambio = EstadoInicial.ProcesosListosFO.Dequeue();
                    cambio.envejecimiento = 5;
                    EstadoInicial.ProcesosListos.Enqueue(cambio);

                }
                else
                {
                    EstadoInicial.ProcesosListosFO.Peek().envejecimiento--;
                }

                if ((EstadoInicial.ProcesosListosSJF.Peek().envejecimiento <= 0) && (EstadoInicial.ProcesosListosSJF.Count > 1))
                {
                    Proceso cambio = EstadoInicial.ProcesosListosSJF.Dequeue();
                    cambio.envejecimiento = 5;
                    EstadoInicial.ProcesosListosFO.Enqueue(cambio);

                }
                else
                {
                    EstadoInicial.ProcesosListosSJF.Peek().envejecimiento--;
                }
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

