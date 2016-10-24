using Carubbi.Negocio;
using Carubbi.StateMachine;
using System;

namespace Carubbi.NegocioTeste
{
    public class MaquinaEstadosOficios : Machine<IOficio>
    {
        public MaquinaEstadosOficios(IOficio oficio)
            : base(oficio)
        { 
            
        
        }

         
        public override void Config()
        {
            this.Add(MachineFactory<IOficio>.GetInstance().GetState("EstadosOficio.EmElaboracao"), true);
            this.Add(MachineFactory<IOficio>.GetInstance().GetState("EstadosOficio.Pendente"));
            this.Add(MachineFactory<IOficio>.GetInstance().GetState("EstadosOficio.Concluido"));

            
  
        }

        public override void UpdateState()
        {
            try
            {
                // ExecuteNonQuery(CommandType.StoredProcedure, "SPOJ9_ESTADO_OFICIO_ATUALIZAR");
            }
            catch (Exception ex)
            {
                throw new StateMachineException("Erro oa atualizar o estado do ofício", ex);
            }
        }

       
        [StateAction(StateName="EstadosOficio.EmElaboracao", LiteralName="Enviar")]
        public void Enviar() 
        {

            this.CurrentState = this["EstadosOficio.Pendente"];
        }


      
        [StateAction(StateName = "EstadosOficio.Pendente", LiteralName = "Aprovar")]
        public void Aprovar()
        {
            ((Oficio)BusinessObject).Id = (int)this.BusinessContext["teste"];
            ((Oficio)BusinessObject).Id++;
            this.CurrentState = this["EstadosOficio.Concluido"];
        }

        [StateAction(StateName = "EstadosOficio.Concluido", LiteralName = "Cancelar")]
        [StateAction(StateName = "EstadosOficio.Pendente", LiteralName = "Cancelar")]
        public void Cancelar()
        {

            this.CurrentState = this["EstadosOficio.EmElaboracao"];
        }

        [StateAction(StateName = "EstadosOficio.Cancelado", LiteralName = "Reiniciar")]
        public void Ler()
        {
            this.CurrentState = this["EstadosOficio.EmElaboracao"];
            
        
        }


         

    }
}
