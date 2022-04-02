namespace Multicolas.Logica.General
{
    public class BloqueInicial
    {
        Random rnd = new Random();
        char randomChar;
        int randomnumber;
        int randomRafaga;
        int randomLlegada;
        int contadorNuevo = 0;
        public List<Proceso> inicialProcesosFCFS = new List<Proceso>();
        public List<Proceso> inicialProcesosSJF = new List<Proceso>();

        public Task AgregarProcesoInicial(string algoritmo)
        {

            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'a', Rafaga = 5, TiempoLlegada = 0 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'b', Rafaga = 3, TiempoLlegada = 1 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'c', Rafaga = 2, TiempoLlegada = 2 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'd', Rafaga = 1, TiempoLlegada = 3 });

            switch (algoritmo)
            {
                case "RR":
                    for (int i = 0; i < 3; i++)
                    {
                        randomChar = (char)rnd.Next('a', 'z');
                        randomnumber = rnd.Next(0, 9);
                        randomRafaga = rnd.Next(1, 5);
                        EstadoInicial.InicialProceso.Add(new Proceso { Name = randomChar.ToString() + randomnumber, TiempoLlegada = i, Rafaga = randomRafaga, Algoritmo = "RR" });
                        Console.WriteLine(randomChar);
                        contadorNuevo++;
                    }
                    EstadoInicial.ProcesosListos = EstadoInicial.OrganizarLista(EstadoInicial.InicialProceso);
                    break;

                case "FCFS":
                    for (int i = 0; i < 3; i++)
                    {
                        randomChar = (char)rnd.Next('a', 'z');
                        randomnumber = rnd.Next(0, 9);
                        randomRafaga = rnd.Next(1, 2);
                        inicialProcesosFCFS.Add(new Proceso { Name = randomChar.ToString() + randomnumber, TiempoLlegada = i, Rafaga = randomRafaga, Algoritmo = "FCFS" });
                        Console.WriteLine(randomChar);
                    }
                    EstadoInicial.ProcesosListosFO = EstadoInicial.OrganizarListaFCFS(inicialProcesosFCFS);
                    break;

                case "SJF":
                    for (int i = 0; i < 3; i++)
                    {
                        randomChar = (char)rnd.Next('a', 'z');
                        randomnumber = rnd.Next(0, 9);
                        randomRafaga = rnd.Next(3, 7);
                        inicialProcesosSJF.Add(new Proceso { Name = randomChar.ToString() + randomnumber, TiempoLlegada = i, Rafaga = randomRafaga, Algoritmo = "SJF" });
                        Console.WriteLine(randomChar);
                    }
                    EstadoInicial.ProcesosListosSJF = EstadoInicial.OrganizarLista(inicialProcesosSJF);

                    break;

            }

            return Task.CompletedTask;
        }

        public Task AgregarNuevoProceso()
        {
            randomChar = (char)rnd.Next('a', 'z');
            randomnumber = rnd.Next(0, 9);
            randomRafaga = rnd.Next(2, 7);
            EstadoInicial.ProcesosListos.Enqueue(new Proceso { Name = randomChar.ToString() + randomnumber, TiempoLlegada = contadorNuevo, Rafaga = randomRafaga, RafagaTemporal = randomRafaga, Algoritmo = "RR" });
            contadorNuevo++;


            return Task.CompletedTask;
        }

        public Task AgregarNuevoProcesoFO()
        {
            randomChar = (char)rnd.Next('a', 'z');
            randomnumber = rnd.Next(0, 9);
            randomRafaga = rnd.Next(1, 5);
            EstadoInicial.ProcesosListosFO.Enqueue(new Proceso { Name = randomChar.ToString() + randomnumber, TiempoLlegada = contadorNuevo, Rafaga = randomRafaga, RafagaTemporal = randomRafaga, Algoritmo = "FCFS" });
            contadorNuevo++;
            return Task.CompletedTask;
        }

        public Task AgregarNuevoProcesoSJF()
        {
            randomChar = (char)rnd.Next('a', 'z');
            randomnumber = rnd.Next(0, 9);
            randomRafaga = rnd.Next(4, 7);
            EstadoInicial.ProcesosListosSJF.Enqueue(new Proceso { Name = randomChar.ToString() + randomnumber, TiempoLlegada = contadorNuevo, Rafaga = randomRafaga, RafagaTemporal = randomRafaga, Algoritmo = "SJF" });
            contadorNuevo++;
            return Task.CompletedTask;
        }
    }
}
