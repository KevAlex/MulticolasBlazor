using Multicolas.Logica.General;
using Multicolas.Shared;

namespace Multicolas.Logica.SJF
{
    public class SJF
    {

        private BloqueInicial bloqueControl;
        private EstadoEjecucionSJF estadoEjecucion;
        private EstadoBloqueo estadoBloqueo;
        private Pages.Index ind;

        public SJF()

        {
            //ind = i;
            bloqueControl = new BloqueInicial();
            estadoEjecucion = new EstadoEjecucionSJF();
        }

        public async Task<Proceso> IniciarSJF(Proceso procesoEntrante)
        {
            EstadoInicial.ProcesosListosSJF = EstadoInicial.OrganizarColaSJF(EstadoInicial.ProcesosListosSJF, EstadoInicial.TiempoGlobal);
            //Proceso siguiente = EstadoInicial.ProcesosListos.Dequeue();
            Proceso siguiente = procesoEntrante;
            siguiente.TiempoComienzo = EstadoInicial.TiempoGlobal;


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

            while ((siguiente.RafagaTemporal > 0) && (EstadoInicial.ProcesoBloqueado == false) && (EstadoInicial.NuevoProceso == false))
            {

                await estadoEjecucion.Ejecutar(siguiente);
                EstadoInicial.TiempoGlobal++;
                EstadoInicial.ProcesoGrafico.Add(new ProcesoUI { Id = siguiente.Name, Posicion = EstadoInicial.TiempoGlobal, Color = "green" });

                await Task.Delay(1500);
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
                EstadoInicial.ProcesosListosSJF.Enqueue(siguiente);
            }
            EstadoInicial.ProcesoBloqueado = false;
            siguiente.Bloqueado = false;
            Console.WriteLine("Hola" + siguiente.Name);
            return siguiente;
        }
    }
}

