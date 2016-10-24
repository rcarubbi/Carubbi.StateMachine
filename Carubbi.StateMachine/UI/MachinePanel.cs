using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Carubbi.StateMachine
{
    public class MachinePanel<T> : CompositeControl
    {
        public delegate Dictionary<string, object> StateActionHandler();


        public Machine<T> Machine
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            Panel pnlButtons = new Panel();
            this.Controls.Add(pnlButtons);
            pnlButtons.Controls.Add(new LiteralControl("<table><tr>"));
            if (Machine != null && Machine.BusinessObject != null && Machine.CurrentState != null)
            {

                foreach (IStateAction<T> action in Machine.RetrieveActions((State<T>)Machine.CurrentState))
                {
                    Button btnAction = new Button();
                    btnAction.ID = "btn" + action.SignatureName;
                    btnAction.Text = action.LiteralName;
                  
 
                    
                    btnAction.Click += new EventHandler(btnAction_Click);
                    pnlButtons.Controls.Add(new LiteralControl("<td>"));
                    pnlButtons.Controls.Add(btnAction);
                    pnlButtons.Controls.Add(new LiteralControl("</td>"));
                }

            }
            pnlButtons.Controls.Add(new LiteralControl("</tr></Table>"));
        }

        void btnAction_Click(object sender, EventArgs e)
        {
            if (RefreshContext != null)
                Machine.BusinessContext = RefreshContext();
            if (BeforeActionPermorm != null)
                BeforeActionPermorm(sender, e);
            foreach (IStateAction<T> action in Machine.RetrieveActions((State<T>)Machine.CurrentState))
            {
                if (action.ActionInfo.Name == ((Button)sender).ID.Replace("btn", ""))
                {
                    action.Executar();
                    this.Controls.Clear();
                    this.CreateChildControls();

                }
            }
            if (AfterActionPermormed != null)
                AfterActionPermormed(sender, e);
        }

        public event EventHandler BeforeActionPermorm;
        public event EventHandler AfterActionPermormed;
        public event StateActionHandler RefreshContext;
    }
}
