using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection connEvent = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = "Server=mssql3.gear.host;Initial Catalog=thingdatabase;"
                    + "Integrated Security=False;User Id=thingdatabase;"
                    + "Password=" + Application["sqlPW"].ToString()
                    + ";MultipleActiveResultSets=True";
                SqlConnection connEvent = new SqlConnection(connString);
                connEvent.Open();
                SqlCommand comEvent = new SqlCommand("dbo.CreateOneThing", connEvent);
                comEvent.CommandType = CommandType.StoredProcedure;
                comEvent.Parameters.AddWithValue("@ThingDescr", txtInfo.Text);
                int iInserted = comEvent.ExecuteNonQuery();

                //new stored procedure
                SqlCommand command = new SqlCommand("dbo.GetAllTheThings", connEvent);
                command.CommandType = CommandType.StoredProcedure;
                
                SqlDataReader reader = command.ExecuteReader();
                    //Call Read before accessing data
                while (reader.Read())
                {
                    resultsDIV.InnerHtml += "<p>";
                    resultsDIV.InnerHtml += reader[1].ToString();
                    resultsDIV.InnerHtml += "</p>";
                    
                }

                
            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }
            finally
            {
                if (connEvent != null)
                {
                    connEvent.Close();
                }
            }
        }
    }
}