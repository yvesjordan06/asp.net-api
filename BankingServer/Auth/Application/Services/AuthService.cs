using System.Text;
using Azure;
using Azure.AI.Language.QuestionAnswering;
using BankingServer.Auth.Domain.Models;
using BankingServer.Auth.Domain.Repositories;
using BankingServer.Auth.Domain.Services;
using BankingServer.Core.Exceptions;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using Newtonsoft.Json;
using CustomVisionErrorException = Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models.CustomVisionErrorException;
using ImageUrl = Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models.ImageUrl;

namespace BankingServer.Auth.Application.Services
{
    public record RegisterRecord
    {
        public string email;
        public string password;
    }
    public class AuthService : IAuthService
    {
        // You can obtain these values from the Keys and Endpoint page for your Custom Vision resource in the Azure Portal.
        private static string trainingEndpoint = "https://congotelecomdemo.cognitiveservices.azure.com/";
        private static string trainingKey = "KeyHere";
        // You can obtain these values from the Keys and Endpoint page for your Custom Vision Prediction resource in the Azure Portal.
        //https://congotelecomdemo-prediction.cognitiveservices.azure.com/
        private static string predictionEndpoint = "https://congotelecomdemo-prediction.cognitiveservices.azure.com/";
        private static string predictionKey = "KeyHere";
        
        private static string languageEndpoint = "https://congotelecom.cognitiveservices.azure.com/";
        private static string languageKey = "keyHere";
        private static string languageRegion = "eastus";
        private static string qNaDeploymentName = "production";
        private static string qnaProjectName = "congodemo";
        
        private static string translatorEndpoint = "https://api.cognitive.microsofttranslator.com";
        private static string translatorKey = "KeyHere";

        private CustomVisionTrainingClient trainingApi { get; }
        private CustomVisionPredictionClient predictionApi { get; }
        private Project project { get; }

        
    
        private readonly IAuthRepository _authRepository;
    
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            trainingApi = AuthenticateTraining(trainingEndpoint, trainingKey);
            predictionApi = AuthenticatePrediction(predictionEndpoint, predictionKey);
            project = trainingApi.GetProject(Guid.Parse("e227488c-c015-4099-95c2-5535ed9dd266"));
        }
        
        private static CustomVisionTrainingClient AuthenticateTraining(string endpoint, string trainingKey)
        {
            // Create the Api, passing in the training key
            CustomVisionTrainingClient trainingApi = new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey))
            {
                Endpoint = endpoint
            };
            return trainingApi;
        }
        private static CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
        {
            // Create a prediction endpoint, passing in the obtained prediction key
            CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(predictionKey))
            {
                Endpoint = endpoint
            };
            return predictionApi;
        }
    
        public Task<UserModel> RegisterAsync(string email, string password)
        {
            throw BankException.NotImplemented();
        }

        public Task<AuthTokenModel> LoginAsync(string email, string password)
        {
            throw BankException.NotFound();
        }

        public Task<AuthTokenModel> RefreshTokenAsync(string token, string refreshToken)
        {
            throw BankException.NotImplemented();
        }

        public async Task<string> Predict(Stream image)
        {
            Console.WriteLine("Making the prediction");
            //Print out the projeect 
            Console.WriteLine($"Project: {project.Name}");
            Console.WriteLine($"Project Id: {project.Id}");
            
            // Print the published iteration name
            var iteration = await trainingApi.GetIterationsAsync(project.Id);
            //Get the first iteration
            var firstIteration = iteration.First();
            
            //Print the iteration name
            Console.WriteLine($"Iteration: {firstIteration.Name}");

            //Classify the image stream using Azure custom vision prediction 
            try
            {
                //Convert url https://cdn.pixabay.com/photo/2018/07/08/15/31/apple-3524113__480.jpg to PredictionImageUrl
                var result = await predictionApi.ClassifyImageWithNoStoreAsync(project.Id, firstIteration.PublishName, image);
             //  var url = new ImageUrl("https://cdn.pixabay.com/photo/2018/07/08/15/31/apple-3524113__480.jpg");
                //var result = await predictionApi.ClassifyImageUrlWithNoStoreAsync(project.Id, firstIteration.PublishName, url);
                // Print out the results
                foreach (var c in result.Predictions)
                {
                    Console.WriteLine($"\t{c.TagName}: {c.Probability:P1}");
                }

                var cm= result.Predictions.First();
                if (cm.Probability < 0.6) return $"Resultat_Inconnu___Maladie_Inconnu: 00.0%";
                return $"{cm.TagName}: {cm.Probability:P1}";
            }
            catch (CustomVisionErrorException e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public Task LogoutAsync()
        {
            throw BankException.NotImplemented();
        }

        public async Task<QnaModel> AskQuestion(string question)
        {
            
            QuestionAnsweringClient client = new QuestionAnsweringClient(new Uri(languageEndpoint), new AzureKeyCredential(languageKey));
            QuestionAnsweringProject project = new QuestionAnsweringProject(qnaProjectName, qNaDeploymentName);
            var translatedQuestion = await Translate(question);
            //Log the question
            Console.WriteLine($"Question: {translatedQuestion}");
            Response<AnswersResult> response = await client.GetAnswersAsync(translatedQuestion, project);
            AnswersResult result = response.Value;
            var answerR = "";
            var confidenceR = "00%";
            var sourceR = "";
            var promptR = new List<string>();
            
            //If there is no answer
           
            {
                answerR = "Aucune réponse trouvée";
                confidenceR = "00%";
                sourceR = "Aucune source";
                promptR.Add("No good match found in the KB");
            }
            foreach (KnowledgeBaseAnswer answer in result.Answers)
            {
                if(answer.Confidence < 0.4)continue;
                Console.WriteLine($"Short Answer: {answer.ShortAnswer}");
                answerR = await Translate(answer.Answer, "fr");
                Console.WriteLine($"Answer:{answerR}");
                Console.WriteLine($"Confidence Score: {answer.Confidence}");
                confidenceR = $"{answer.Confidence:P1}";
                Console.WriteLine($"Source: {answer.Source}");
                sourceR = answer.Source;
                //If there is no prompt continue
                if (answer.Dialog == null) continue;
                
                //Follow up prompts
                foreach (KnowledgeBaseAnswerPrompt prompt in answer.Dialog.Prompts)
                {
                    Console.WriteLine($"Prompt: {prompt.DisplayText}");
                    promptR.Add(await Translate(prompt.DisplayText, "fr"));
                    Console.WriteLine($"QnA ID: {prompt.QnaId}");
                }
                Console.WriteLine($"Questions:");
                foreach (string questionText in answer.Questions)
                {
                    Console.WriteLine($"\t{questionText}");
                }
                Console.WriteLine($"Metadata:");
                foreach (KeyValuePair<string, string> metadata in answer.Metadata)
                {
                    Console.WriteLine($"\t{metadata.Key}: {metadata.Value}");
                }
            }
         

            return new QnaModel(question, answerR, confidenceR, sourceR,promptR);
        }

        public async Task<string> Translate(string text, string to="en")
        {
            // Input and output languages are defined as parameters.
            string route = $"/translate?api-version=3.0&to={to}";
            object[] body = new object[] { new { Text = text } };
            string requestBody = JsonConvert.SerializeObject(body);
            
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(translatorEndpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", translatorKey);
                // Location of the resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", languageRegion);
                
                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync();
                
                //Log the result
                Console.WriteLine(result);
                
                // Read response as a JSON object.
                dynamic jsonResponse = JsonConvert.DeserializeObject(result);
                
                // Extract the translated text.
                string translation = jsonResponse[0].translations[0].text;
                // Return the translation.
                return translation;
            }
        }
    }
}

