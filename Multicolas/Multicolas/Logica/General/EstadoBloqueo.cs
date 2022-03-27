namespace Multicolas.Logica.General
{
    public class EstadoBloqueo
    {
        //Evaluar si esta clase sobra
        public Task BloquearProceso(Proceso procesoB)
        {
            // parece que sobra
            procesoB.Bloqueado = true;
            // para aumentarle el tiempo 
            // parece que sobra
            procesoB.FueBloqueado = true;
            // Lo moví al index para renderizarlo mejor
            //EstadoInicial.ListaEjecucion.Remove(procesoB);
            EstadoInicial.ProcesosListos.Enqueue(procesoB);
            Console.WriteLine("Proceso bloqueado " + procesoB.Name + " Rafaga Temporal " + procesoB.RafagaTemporal + " Espera " + procesoB.TiempoEspera);
            return Task.CompletedTask;
        }
    }
}
