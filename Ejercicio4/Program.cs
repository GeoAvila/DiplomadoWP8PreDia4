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
            DirectorDeOrquesta director = new DirectorDeOrquesta(violinista, clarinetista,triangulista);

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

    public class DirectorDeOrquesta
    {
        private List<Musico> musicos;
        public DirectorDeOrquesta(params Musico[] musicos)
        {
            this.musicos = new List<Musico>(musicos);
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
        }


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

}
