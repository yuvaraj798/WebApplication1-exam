using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1_exam
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private SqlConnection con = null;
        private DataSet ds = null;
        private SqlDataAdapter adapterObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRCon"].ConnectionString))
            {
                using (adapterObj = new SqlDataAdapter("select*from position", con))
                {
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapterObj);
                    ds = new DataSet();
                    adapterObj.Fill(ds, "Position");
                    


                    DataRow dr = ds.Tables["Position"].NewRow();
                    dr["PositionCode"] = TxtPositionCode.Text;
                    dr["Description"] = TxtDescription.Text;
                    dr["BudgetedStrenght"] = TxtBudgetedStrength.Text;
                    dr["Year"] = DdlYear.Text;
                    dr["currentstrenght"] = TxtCurrentStrength.Text;                     
                    ds.Tables["Position"].Rows.Add(dr);
                    

                    int res = adapterObj.Update(ds, "Position");
                    ds.Clear();
                    adapterObj.Fill(ds, "Position");
                    int row = ds.Tables[0].Rows.Count - 1;
                }
            }


        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            TxtPositionCode.Text = "";
            TxtDescription.Text = "";
            TxtCurrentStrength.Text = "";
            TxtBudgetedStrength.Text = "";
            DdlYear.SelectedValue = null;
        }
    }
}