using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAI.Ejercicio52Guia
{
    class Program
    {
        static Dictionary<int, Persona> personas = new Dictionary<int, Persona>();

        static void Main(string[] args)
        {
            Console.WriteLine("a. Alta");
            Console.WriteLine("b. Baja");
            Console.WriteLine("g. Grabar archivo");
            Console.WriteLine("l. Leer de archivo");
            Console.WriteLine("s. Terminar");

            while (true)
            {
                var tecla = Console.ReadKey();
                if (tecla.Key == ConsoleKey.A)
                {
                    Alta();
                }
                else if (tecla.Key == ConsoleKey.B)
                {
                    Baja();
                }
                else if (tecla.Key == ConsoleKey.G)
                {
                    Grabar();
                }
                else if (tecla.Key == ConsoleKey.L)
                {
                    Leer();
                }
                else if (tecla.Key == ConsoleKey.S)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("No es una opción de la lista.");
                }
            }


            Console.ReadKey();
        }

        private static void Leer()
        {
            string ruta;
            do
            {
                Console.WriteLine("Ingrese el nombre del archivo a leer.");
                ruta = Console.ReadLine();
                if (!File.Exists(ruta))
                {
                    Console.WriteLine("El archivo no existe");
                }
            }
            while (!File.Exists(ruta));

            using (var archivo = File.OpenRead(ruta))
            {
                using (var reader = new StreamReader(archivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var persona = Persona.Parse(linea);
                        personas.Add(persona.Legajo, persona);
                    }
                }
            }
        }

        private static void Grabar()
        {
            Console.WriteLine("Ingrese el nombre del archivo a grabar.");
            string ruta = Console.ReadLine();
            if (File.Exists(ruta))
            {
                Console.WriteLine("El archivo ya existe. Desea sobreescribirlo?");
                if (Console.ReadKey().Key == ConsoleKey.S)
                {
                    File.Delete(ruta);
                }
            }

            using (var archivoStream = File.OpenWrite(ruta))
            {
                using (var writer = new StreamWriter(archivoStream))
                {
                    foreach (var persona in personas)
                    {
                        writer.WriteLine($"{persona.Value.Legajo}|{persona.Value.Nombre}");
                    }
                }
            }
        }

        private static void Baja()
        {
            throw new NotImplementedException();
        }

        private static void Alta()
        {
            bool seguir = true;
            while (seguir)
            {
                var persona = Persona.Ingresar();
                if (personas.ContainsKey(persona.Legajo))
                {
                    Console.WriteLine("El legajo indicado ya existe.");
                }
                else
                {
                    personas.Add(persona.Legajo, persona);
                    Console.WriteLine("Desea ingresar otro [S/N]?");
                    var tecla = Console.ReadKey();
                    seguir = tecla.Key == ConsoleKey.S;
                }
            }
        }
    }
}
