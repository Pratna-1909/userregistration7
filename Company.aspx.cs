using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserRegistration
{
    public partial class Company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayVideo();
        }
        void DisplayVideo()
        {
            var vFrame = new Literal();
            vFrame.Text = string.Format(@"<iframe width=""600"" height= ""350"" src=""{0}"" frameborder =""0"" allowfullscren> </iframe>", "https://youtu.be/embed/4IgC2Q5-yDE");
            Panel1.Controls.Add(vFrame);
        }
    }
}