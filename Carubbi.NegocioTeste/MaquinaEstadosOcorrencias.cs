using Carubbi.Negocio;
using Carubbi.StateMachine;

namespace Carubbi.NegocioTeste
{
    public class MaquinaEstadosOcorrencias : Machine<IOficio>
    {
        public MaquinaEstadosOcorrencias(IOficio oficio)
            : base(oficio) 
        { 
            
        
        }

         
        public override void Config()
        {
            this.Add(MachineFactory<IOficio>.GetInstance().GetState("EstadosOcorrencia.Novo"), true);
            this.Add(MachineFactory<IOficio>.GetInstance().GetState("EstadosOcorrencia.Atribuido"));
            this.Add(MachineFactory<IOficio>.GetInstance().GetState("EstadosOcorrencia.OK"));

  
        }

        public override void RetrieveState()
        {
            
        }

        public override void UpdateState()
        {

        }

        [StateAction(StateName = "EstadosOcorrencia.Novo", LiteralName = "Atribuir")]
        public void Enviar()
        {
            this.CurrentState = this["EstadosOcorrencia.Atribuido"];
        }


        [StateAction(StateName = "EstadosOcorrencia.Atribuido", LiteralName = "Aprovar")]
        public void Aprovar()
        {
            ((Oficio)BusinessObject).Id = (int)this.BusinessContext["teste"];
            ((Oficio)BusinessObject).Id+=2;
            this.CurrentState = this["EstadosOcorrencia.OK"];
        }

        [StateAction(StateName = "EstadosOcorrencia.OK", LiteralName = "Criar")]
        [StateAction(StateName = "EstadosOcorrencia.Atribuido", LiteralName = "Criar")]
        public void Criar()
        {

            this.CurrentState = this["EstadosOcorrencia.Novo"];
        }
    }
}
