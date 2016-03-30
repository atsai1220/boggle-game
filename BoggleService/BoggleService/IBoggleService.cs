using System.Collections.Generic;
using System.Dynamic;
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

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/users")]
        UserTokenContract CreateUser(CreateUserBody body);

        [WebInvoke(Method ="POST", UriTemplate = "/games")]
        string JoinGame(JoinGameBody body);

        [WebInvoke(Method = "PUT", UriTemplate = "/games")]
        void CancelJoinRequest(CancelJoinRequestBody body);

        [WebInvoke(Method = "PUT", UriTemplate = "/games/{gameId}")]
        string PlayWord(PlayWordBody body, string gameId);

        [WebInvoke(Method = "GET", UriTemplate = "/games/{gameId}?Brief={brief}")]
        BoggleGameContract GameStatus(string gameId, bool brief);
    }
}
