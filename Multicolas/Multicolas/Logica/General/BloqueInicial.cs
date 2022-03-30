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

        public Task AgregarProcesoInicial()
        {
            //_estadoInicial.InicialProceso.Add(new Proceso { Name = "A", Rafaga = 1, TiempoLlegada = 3 });
            //_estadoInicial.InicialProceso.Add(new Proceso { Name = "b", Rafaga = 1, TiempoLlegada = 5 });
            //_estadoInicial.InicialProceso.Add(new Proceso { Name = "c", Rafaga = 1, TiempoLlegada = 1 });
            //_estadoInicial.InicialProceso.Add(new Proceso { Name = "d", Rafaga = 1, TiempoLlegada = 0 });

            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'a', Rafaga = 8, TiempoLlegada = 0 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'b', Rafaga = 4, TiempoLlegada = 1 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'c', Rafaga = 9, TiempoLlegada = 2 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'd', Rafaga = 5, TiempoLlegada = 3 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'e', Rafaga = 2, TiempoLlegada = 4 });


            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'a', Rafaga = 5, TiempoLlegada = 0 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'b', Rafaga = 3, TiempoLlegada = 1 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'c', Rafaga = 2, TiempoLlegada = 2 });
            //EstadoInicial.InicialProceso.Add(new Proceso { Name = 'd', Rafaga = 1, TiempoLlegada = 3 });

            for (int i = 0; i < 3; i++)
            {
                randomChar = (char)rnd.Next('a', 'z');
                randomnumber = rnd.Next(0, 9);
                randomRafaga = rnd.Next(1, 4);
                EstadoInicial.InicialProceso.Add(new Proceso { Name = randomChar.ToString() + randomnumber, TiempoLlegada = i, Rafaga = randomRafaga });
                Console.WriteLine(randomChar);
                contadorNuevo++;
            }
            EstadoInicial.ProcesosListos = EstadoInicial.OrganizarLista(EstadoInicial.InicialProceso);
            return Task.CompletedTask;
        }

        public Task AgregarNuevoProceso()
        {
            randomChar = (char)rnd.Next('a', 'z');
            randomnumber = rnd.Next(0, 9);
            randomRafaga = rnd.Next(2, 7);
            EstadoInicial.ProcesosListos.Enqueue(new Proceso { Name = randomChar.ToString() + randomnumber, TiempoLlegada = contadorNuevo, Rafaga = randomRafaga, RafagaTemporal = randomRafaga });
            contadorNuevo++;
            return Task.CompletedTask;
        }

        public Task AgregarNuevoProcesoFO()
        {
            randomChar = (char)rnd.Next('a', 'z');
            randomnumber = rnd.Next(0, 9);
            randomRafaga = rnd.Next(2, 7);
            EstadoInicial.ProcesosListosFO.Enqueue(new Proceso { Name = randomChar.ToString() + randomnumber, TiempoLlegada = contadorNuevo, Rafaga = randomRafaga, RafagaTemporal = randomRafaga });
            contadorNuevo++;
            return Task.CompletedTask;
        }
    }
}
