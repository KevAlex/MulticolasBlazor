using Multicolas.Shared;

namespace Multicolas.Logica.General
{
    public class EstadoInicial
    {
        public static List<Proceso> InicialProceso { get; set; } = new List<Proceso>();

        public static List<ProcesoUI> ProcesoGrafico { get; set; } = new List<ProcesoUI>();

        public static Queue<Proceso> ProcesosListos { get; set; } = new Queue<Proceso>();

        public static Queue<Proceso> ProcesosListosFO { get; set; } = new Queue<Proceso>();

        public static Queue<Proceso> ProcesosListosSJF { get; set; } = new Queue<Proceso>();

        // A ser usada cuando se agregue un nuevo proceso
        // a un algoritmo con mayor prioridad
        public static bool NuevoProceso { set; get; } = false;

        public static List<Proceso> FinalProceso { get; set; } = new List<Proceso>();

        public static List<Proceso> ListaEjecucion { get; set; } = new List<Proceso>();

        public static bool ProcesoBloqueado { get; set; } = false;

        public static bool Semaforo { set; get; } = false;

        public static int TiempoGlobal { get; set; } = 0;

        public static Queue<Proceso> OrganizarLista(List<Proceso> listaInicial)
        {
            Queue<Proceso> interno = new Queue<Proceso>();
            List<Proceso> sortedProcesos = listaInicial.OrderBy(o => o.TiempoLlegada).ToList();

            foreach (Proceso item in sortedProcesos)
            {
                item.RafagaTemporal = item.Rafaga;
                interno.Enqueue(item);
            }

            return interno;
        }
        public static Queue<Proceso> OrganizarCola(Queue<Proceso> cola)
        {
            Queue<Proceso> interno = new Queue<Proceso>();
            List<Proceso> sortedProcesos = new List<Proceso>();
            // Organizando la lista basado en la rafaga
            sortedProcesos = cola.OrderBy(o => o.RafagaTemporal).ToList();

            foreach (var item in sortedProcesos)
            {
                interno.Enqueue(item);
            }

            

            return interno;
        }
        public static Queue<Proceso> OrganizarColaSJF(Queue<Proceso> cola, int tiempo)
        {
            Queue<Proceso> interno = new Queue<Proceso>();
            List<Proceso> sortedProcesos = new List<Proceso>();
            // Organizando la lista basado en la rafaga
            sortedProcesos = cola.OrderBy(o => o.TiempoLlegada > tiempo).ThenBy(o => o.Rafaga).ToList();

            foreach (var item in sortedProcesos)
            {
                interno.Enqueue(item);
            }



            return interno;
        }

        public static Queue<Proceso> OrganizarListaFCFS(List<Proceso> listaInicial)
        {
            Queue<Proceso> interno = new Queue<Proceso>();
            List<Proceso> sortedProcesos = listaInicial.OrderBy(o => o.TiempoLlegada).ToList();

            foreach (Proceso item in sortedProcesos)
            {
                item.RafagaTemporal = item.Rafaga;
                interno.Enqueue(item);
            }

            return interno;
        }
    }
}
