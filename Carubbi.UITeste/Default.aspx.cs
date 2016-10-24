using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Carubbi.StateMachine;
using System.Configuration;
using Carubbi.Negocio;


namespace Carubbi.UITeste
{
    public partial class _Default : System.Web.UI.Page
    {
        public Machine<IOficio> MaqOficio
        {
            get
            {
                if (Session["of"] == null)
                {

                    Session["of"] = MachineFactory<IOficio>.GetInstance().GetMachine(new Oficio(), "stateMachine");

                   
                }
                return (Machine<IOficio>)Session["of"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MachinePanel<IOficio> machinePanel = new MachinePanel<IOficio>();
            machinePanel.Machine = MaqOficio;
            form1.Controls.Add(machinePanel);
            machinePanel.BeforeActionPermorm += new EventHandler(machinePanel_BeforeActionPermorm);
            machinePanel.AfterActionPermormed += new EventHandler(machinePanel_AfterActionPermormed);
            machinePanel.RefreshContext += new MachinePanel<IOficio>.StateActionHandler(machinePanel_RefreshContext);
            lblState.Text = MaqOficio.CurrentState.Name;

          
          
        }

        void machinePanel_BeforeActionPermorm(object sender, EventArgs e)
        {
            
        }

        Dictionary<string, object> machinePanel_RefreshContext()
        {
           Dictionary<string, object> context = new Dictionary<string, object>();
           context["teste"] = MaqOficio.BusinessObject.Id;
           return context;
        }

        void machinePanel_AfterActionPermormed(object sender, EventArgs e)
        {
            lblState.Text = MaqOficio.CurrentState.Name + " - " + MaqOficio.BusinessObject.Id.ToString();
            
        }
    }
}
