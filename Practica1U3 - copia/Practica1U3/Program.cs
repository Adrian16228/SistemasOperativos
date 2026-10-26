using System;
using System.Collections.Generic;
namespace PlanificacionProcesos
{
    public class Bloque(int id, int tamaño)
    {
        public int Id { get; set; }
        public int Tamaño { get; set; }
        public bool EstaLibre { get; set; }
        public Proceso ProcesoAsignado { get; set; }

    }
    public class Proceso
    {
        public int Id { get; set; }
        public int MemoriaSolicitada  { get; set; }
        public string Estado { get; set; }
        public int ParticionAsignada { get; set; }
        public int MemoriaOcupada { get; set; }
        public int FragmentacionInterna { get; set; }

        
    }




    class Program
    {
        static void Main(string[] args)
        {
            /*pedir datos a usuario para crear la memoria
             * ejecutar "crearmemoria"
             * mostrar la memoria creada
         
            */
            
        }
        List<Proceso> ProcesosTerminados = new List<Proceso>();

        //memoria creada al principio con la cantidad de bloques y el tamaño de cada bloque por el usuario
        static List<Bloque> CrearMemoria(int cantidadBloques, int[] tamaño)
        {
            List<Bloque> memoria = new List<Bloque>();
            for (int i = 0; i < cantidadBloques; i++)
            {
                for (int j = 0; j < tamaño[i]; j++)
                {
                    memoria.Add(new Bloque(i, tamaño[i]));
                }
            }
            return memoria;
        }

        static void FirstFit(List<Bloque> memoria, int idProceso, int memoriaSolicitada)
        {//crear un nuevo proceso con los datos del usuario
            Proceso proceso = new Proceso
            {
                Id = idProceso,
                MemoriaSolicitada = memoriaSolicitada,
                Estado = "En espera",
                ParticionAsignada = -1,
                MemoriaOcupada = 0,
                FragmentacionInterna = 0
            };

            //buscar el primer bloque libre que tenga el tamaño suficiente para el proceso
            for (int i = 0; i < memoria.Count; i++)
            {
                if (memoria[i].EstaLibre && memoria[i].Tamaño >= memoriaSolicitada)
                {
                    //asignar el proceso al bloque
                    memoria[i].EstaLibre = false;
                    memoria[i].ProcesoAsignado = proceso;
                    //actualizar el estado del proceso
                    proceso.Estado = "Activo";
                    proceso.ParticionAsignada = memoria[i].Id;
                    proceso.MemoriaOcupada = memoriaSolicitada;
                    proceso.FragmentacionInterna = memoria[i].Tamaño - memoriaSolicitada;

                    Console.WriteLine($"Proceso asignado al bloque {memoria[i].Id}");
                    return;
                }
            }
           
            Console.WriteLine("No hay bloques libres suficientes para el proceso");
        }
        static void TerminarProceso(List<Bloque> memoria, int idProceso)
        {
            for (int i = 0; i < memoria.Count; i++)
            {//buscar el proceso en la memoria por su id, que este asignado a un bloque de memoria y que este activo
                if (memoria[i].ProcesoAsignado != null && memoria[i].ProcesoAsignado.Id == idProceso && memoria[i].ProcesoAsignado.Estado == "Activo")
                {
                    //terminar el proceso actualizando la memoria como el proceso
                    memoria[i].EstaLibre = true;
                    memoria[i].ProcesoAsignado.Estado = "Terminado";
                    //se guarda en otra lista los procesos terminados que ya no estan en la memoria
                    Proceso procesoTerminado = memoria[i].ProcesoAsignado;
                    //se elimina de la memoria el proceso terminado
                    memoria[i].ProcesoAsignado = null;
                    Console.WriteLine($"Proceso {idProceso} terminado");
                    return;
                }
            }
            Console.WriteLine($"No se encontró el proceso {idProceso} en la memoria");
        }
        static void MostrarTablaProcesos(List<Bloque> memoria)
        {
            Console.WriteLine("Tabla de procesos:\n \tID\t Memoria solicitada(MB) \tEstado");
            for(int i=0;i<memoria.Count)
            Console.WriteLine("")

        }
    }
}
/*
 * Mostrar: 
numero de particiones de memoria(num de nodos lista)
tamaño en mb por partición
estrategia de asignación

tabla inicial de particiones todas libre

1. Registrar llegada de un proceso.
	id proceso
	memoria solicitada
2. Terminar un proceso.
	mandar que proceso se termina(P1)
	solo si esta activo y asignado a un bloque de memoria si no informar
3. Mostrar tabla de procesos.
4. Mostrar tabla de bloques de memoria.
5. Mostrar resumen de memoria.
	Memoria total
	Memoria ocupada por procesos
	Fragmentación interna total
	Memoria libre
	Procesos registrados(los que si se asignaron)
	Procesos activos asignados(que no terminan)
	Procesos terminados
	procesos sin asignar
	Porcentaje de memoria ocupada por procesos
	porcentaje de fragmentación interna
	porcentaje de memoria libre
6. Salir



memoria asignada a procesos
+	
fragmentación interna
+
memoria libre
---------------------------------
=estos 3 es igual a la memoria total



 */