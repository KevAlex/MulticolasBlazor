using Multicolas.Logica.General;

namespace Multicolas.Logica.FCFS
{
    public class EstadoEjecucionFC
    {

        EstadoBloqueo estadoBloqueo = new EstadoBloqueo();
        int i = 0;
        int rafagaAuxiliar = 0;
        public Task Ejecutar(Proceso procesoN)
        {
            int contadorUI = 0;

            if (EstadoInicial.ListaEjecucion.Contains(procesoN) == false)
            {
                EstadoInicial.ListaEjecucion.Add(procesoN);

            }


            // Bloqueo del proceso
            if (procesoN.FueBloqueado == true || procesoN.Expulsado == true)
            {
                procesoN.TiempoComienzoAlterno = EstadoInicial.TiempoGlobal;
                rafagaAuxiliar = procesoN.RafagaTemporal;

                procesoN.FueBloqueado = false;
                i++;
                // Proceso.Bloqueado deberia ser true para el GUI mirar como manejarlo
            }



            EstadoInicial.Semaforo = true;
            //Console.WriteLine("Proceso en ejecucion " + procesoN.Name + " Llegada " + procesoN.TiempoLlegada + " Rafaga " + procesoN.Rafaga + " RafTem: " + procesoN.RafagaTemporal
            //    + " Tiempo General " + EstadoInicial.TiempoGlobal);



            if (EstadoInicial.ProcesoBloqueado == false)
            {
                procesoN.RafagaTemporal -= 1;
                //contadorUI++;
                //procesoN.PosicionUI = contadorUI + procesoN.TiempoComienzoAlterno;

                //await _stateFacade.nuevoAtributo(procesoN);

                //EstadoInicial.TiempoGlobal++;
            }

            if (EstadoInicial.ProcesoBloqueado == true && procesoN.FueBloqueado == true)
            {

                // El boton debe cambiar el valor de ProcesoBloqueado
                // que está siendo ejecutado, al hacerlo
                // el if de arriba entrara en acción

                estadoBloqueo.BloquearProceso(procesoN);


            }
            else if (!(procesoN.RafagaTemporal > 0))
            {
                // Si la i > 0 es porque el proceso fue bloqueado
                if ((procesoN.FueBloqueado == false && i > 0) || (procesoN.Expulsado == true))
                {
                    //procesoN.TiempoFinal = procesoN.Rafaga + procesoN.TiempoComienzoAlterno;
                    procesoN.TiempoFinal = rafagaAuxiliar + procesoN.TiempoComienzoAlterno;
                    procesoN.TiempoFinalH.Add(procesoN.TiempoFinal);
                    i = 0;
                }
                else
                {
                    procesoN.TiempoFinal = procesoN.Rafaga + procesoN.TiempoComienzo;
                    procesoN.TiempoFinalH.Add(procesoN.TiempoFinal);

                }
                procesoN.TiempoRetorno = procesoN.TiempoFinal - procesoN.TiempoLlegada;
                procesoN.RetornoH.Add(procesoN.TiempoRetorno);
                procesoN.TiempoEspera = ((procesoN.TiempoRetorno - procesoN.Rafaga) + procesoN.TiempoEspera);
                procesoN.EsperaH.Add(procesoN.TiempoEspera);

                EstadoInicial.FinalProceso.Add(procesoN);
                EstadoInicial.ListaEjecucion.Remove(procesoN);
                EstadoInicial.Semaforo = false;
                rafagaAuxiliar = 0;
                Console.WriteLine("Proceso finalizado " + procesoN.Name + " con tiempo general: " + EstadoInicial.TiempoGlobal);

            }
            EstadoInicial.ProcesoBloqueado = false;

            return Task.CompletedTask;
        }
    }
}
