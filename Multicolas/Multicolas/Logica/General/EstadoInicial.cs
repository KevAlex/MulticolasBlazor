namespace Multicolas.Logica.General
{
    public class EstadoInicial
    {
        public static List<Proceso> InicialProceso { get; set; } = new List<Proceso>();

        public static Queue<Proceso> ProcesosListos { get; set; } = new Queue<Proceso>();

        public static List<Proceso> FinalProceso { get; set; } = new List<Proceso>();

        public static List<Proceso> ListaEjecucion { get; set; } = new List<Proceso>();

        public static bool ProcesoBloqueado { get; set; } = false;

        public static bool Semaforo { set; get; } = false;

        public static int TiempoGlobal { get; set; } = 0;

        public static Queue<Proceso> OrganizarLista(List<Proceso> listaInicial)
        {
            Queue<Proceso> interno = new Queue<Proceso>();
            List<Proceso> sortedProcesos = InicialProceso.OrderBy(o => o.TiempoLlegada).ToList();

            foreach (Proceso item in sortedProcesos)
            {
                item.RafagaTemporal = item.Rafaga;
                interno.Enqueue(item);
            }

            return interno;
        }
    }
}
