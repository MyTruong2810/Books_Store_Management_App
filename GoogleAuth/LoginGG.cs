//using Google.Apis.Auth.OAuth2;
//using Google.Apis.PeopleService.v1;
//using Google.Apis.Services;
//using Google.Apis.PeopleService.v1.Data;
//using Google.Apis.Util.Store;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using System.IO;

//namespace Books_Store_Management_App.GoogleAuth
//{
//    public class GoogleLoginHandler
//    {
//        public static async Task<Tuple<string, string>> LoginWithGoogle()
//        {
//            try
//            {
//                string clientSecretFilePath = Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "Assets\\client_secret.json");
//                string[] scopes = new string[]
//                {
//                    "https://www.googleapis.com/auth/userinfo.email",
//                    "https://www.googleapis.com/auth/userinfo.profile"
//                };
//                string credPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GoogleAuthTokens");
//                if (Directory.Exists(credPath))
//                {
//                    Directory.Delete(credPath, true); 
//                }
//                UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
//                    GoogleClientSecrets.FromFile(clientSecretFilePath).Secrets,
//                    scopes,
//                    "user",
//                    CancellationToken.None,
//                    new FileDataStore(credPath, true) 
//                );
//                var service = new PeopleServiceService(new BaseClientService.Initializer
//                {
//                    HttpClientInitializer = credential,
//                    ApplicationName = "Your WinUI App"
//                });
//                var request = service.People.Get("people/me");
//                request.PersonFields = "names,emailAddresses";

//                Person profile = await request.ExecuteAsync();
//                string userName = profile.Names?.FirstOrDefault()?.DisplayName;
//                string userEmail = profile.EmailAddresses?.FirstOrDefault()?.Value;
//                return new Tuple<string, string>(userName, userEmail);
//            }
//            catch (Exception ex)
//            {
//                return new Tuple<string, string>(null, $"Error during Google Login: {ex.Message}");
//            }
//        }
//    }
//}
