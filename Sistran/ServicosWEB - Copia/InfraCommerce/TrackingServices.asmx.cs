using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.InfraCommerce
{
    /// <summary>
    /// Summary description for TrackingServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TrackingServices : System.Web.Services.WebService
    {

        [WebMethod]
        public captureTrackingResponse captureTracking(List<Tracking> trackingList)
        {
            try
            {



                return new captureTrackingResponse() { success = true };
            }
            catch (Exception)
            {

                return new captureTrackingResponse() { success = false };
            }
        }
    }
}
