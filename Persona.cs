using System;
using System.Globalization;
using System.Linq;

namespace CAI.Ejercicio52Guia
{
    internal class Persona
    {
        private Persona(int legajo, string nombre)
        {
            Legajo = legajo;
            Nombre = nombre;
        }

        public int Legajo { get; }
        public string Nombre { get; }

        internal static Persona Ingresar()
        {
            int legajo = 0;
            bool ok = false;
            Console.WriteLine("Nueva persona");
            while (!ok)
            {
                Console.WriteLine("Ingrese el número de legajo.");
                var ingreso = Console.ReadLine();
                if (!int.TryParse(ingreso, out legajo))
                {
                    Console.WriteLine("Debe ingresar un numero.");
                    continue;
                }

                if (legajo < 1)
                {
                    Console.WriteLine("Ingrese un numero positivo.");
                    continue;
                }

                ok = true;
            }

            string nombre = null;
            ok = false;
            while (!ok)
            {
                Console.WriteLine("Ingrese el nombre");
                nombre = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.WriteLine("Ingrese un nombre.");
                    continue;
                }

                if (!nombre.All(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)))
                {
                    Console.WriteLine("Ingrese sólo letras.");
                    continue;
                }

                nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nombre.ToLower());
                ok = true;
            }

            return new Persona(legajo, nombre);
        }

        internal static Persona Parse(string linea)
        {
            var datos = linea.Split('|');
            return new Persona(legajo: int.Parse(datos[0]), nombre: datos[1]);
        }
    }
}