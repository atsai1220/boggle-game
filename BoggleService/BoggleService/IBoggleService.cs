using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Boggle
{
    [ServiceContract]
    public interface IBoggleService
    {
        /// <summary>
        /// Sends back index.html as the response body.
        /// </summary>
        [WebGet(UriTemplate = "/api")]
        Stream API();

        [WebInvoke(Method = "PUT", UriTemplate = "/games")]
        void CancelJoinRequest(CancelJoinRequestBody body);

        [WebInvoke(Method = "GET", UriTemplate = "/games/{gameId}?Brief={brief}")]
        BoggleGameContract GameStatus(string gameId, bool brief);
    }
}
