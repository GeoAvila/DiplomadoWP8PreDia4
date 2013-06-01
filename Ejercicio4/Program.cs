using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4
{
    class Program
    {
        static void Main(string[] args)
        {
            var violinista = new Violinista();
            var clarinetista = new Clarinetista();
            var triangulista = new Triangulista();

            var publico = new List<Publico>();



            DirectorDeOrquesta<Musico> director = new DirectorDeOrquesta<Musico>(violinista, clarinetista, triangulista);
            for (int i = 0; i < 5; i++)
            {
                var p = new Publico("Publico " + i.ToString());
                director.EmpiezaSinfonia += delegate(object sender, EventArgs e)
                                            { p.GuardarSilencio(); };
                director.FinalizaSinfonia += delegate(object sender, EventArgs e)
                                            { p.Aplaudir(); };

                publico.Add(p);
            }

            director.EmpezarSinfonia();
            director.TerminarSinfonia();

            Console.ReadKey();
        }
    }

    public abstract class Musico
    {
        public string Instrumento { get; private set; }

        public Musico(string instrumento)
        {
            Instrumento = instrumento;
        }


        public void Tocar()
        {
            Console.WriteLine(this.ToString() + " empieza a tocar");
        }

        public void Terminar()
        {
            Console.WriteLine(this.ToString() + " termina de tocar");
        }
    }

    public class DirectorDeOrquesta<T> where T:Musico
    {
        private List<T> musicos;
        public DirectorDeOrquesta(params T[] musicos)
        {
            this.musicos = new List<T>(musicos);
            foreach (var musico in musicos)
            {
                if (musico is IAfinable)
                {
                    ((IAfinable)musico).Afinar();
                    //(musico as IAfinable).Afinar(); // devuele null mejor manejo de errores
                }
                
            }
        }
        public void EmpezarSinfonia()
        {
            if (EmpiezaSinfonia != null)
            {
                EmpiezaSinfonia(this, EventArgs.Empty);
            }
            foreach (var musico in musicos)
            {
                musico.Tocar();
            }
        }

        public void TerminarSinfonia()
        {
            
            foreach (var musico in musicos)
            {
                musico.Terminar();
            }
            if (FinalizaSinfonia != null)
            {
                FinalizaSinfonia(this, EventArgs.Empty);
            }
        }
        public event EventHandler EmpiezaSinfonia;
        public event EventHandler FinalizaSinfonia;

    }

    public class Violinista : Musico,IAfinable
    {
        public Violinista()
            : base("Violin")
        {

        }

        public void Afinar()
        {
            Console.WriteLine("El violinista está afinando");
        }
    }

    public class Clarinetista : Musico,IAfinable
    {
        public Clarinetista()
            : base("Clarinete")
        {

        }

        public void Afinar()
        {
            Console.WriteLine("El clarinetista está afinando");
        }
    }
    public class Triangulista : Musico
    {
        public Triangulista():base("Triangulo")
        {

        }
    }
    public interface IAfinable
    {
        void Afinar();
    }
    public class Publico
    { 
        public string Nombre{ get; private set;}
        public Publico (string nombre)
	    {
            this.Nombre = nombre;
	    }
        public void GuardarSilencio()
        {
            Console.WriteLine(Nombre+" esta guardando silencio");
        }
        public void Aplaudir()
        {
            Console.WriteLine(Nombre+" esta aplaudiendo");
        }

    }

}
