using Carubbi.StateMachine;

namespace Carubbi.Negocio
{
    public class Usuario : IUsuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public interface IUsuario
    {
        int Id { get; set; }
        string Name { get; set; }
    }

    public class Oficio : IOficio
    {
        public int Id { get; set; }
        public Machine<IOficio> Maquina { get; set; }

    }

    public interface IOficio
    {
        int Id { get; set; }
        Machine<IOficio> Maquina { get; set; }

    }

}
