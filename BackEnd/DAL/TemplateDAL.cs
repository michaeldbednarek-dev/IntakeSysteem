using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive;
using Google.Apis.Services;
using Google.Apis.Gmail;
using Microsoft.IdentityModel.Tokens;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using IntakeSysteemBack.DAL;
using IntakeSysteemBack.Interfaces;
using IntakeSysteemBack.Models;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System.IO;
using Google.Apis.Util.Store;
using MimeKit;
using Google.Apis.Auth.OAuth2.Flows;
using static Google.Apis.Drive.v3.DriveService;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2.Responses; 
using Google.Apis.Download;
using Microsoft.AspNetCore.Http;
using System.Web;
using GroupDocs.Editor;
using GroupDocs.Editor.Options;
using GroupDocs.Editor.HtmlCss.Resources;
using GroupDocs.Editor.Formats;


namespace IntakeSysteemBack.DAL
{
    public class TemplateDAL : BaseDAL, ITemplate
    {

        /*
        public DriveService getService()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };

            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                Scopes = new[] { DriveService.Scope.Drive,
                       DriveService.Scope.DriveAppdata,
                       DriveService.Scope.DriveFile,
                       DriveService.Scope.DriveMetadataReadonly,
                       DriveService.Scope.DriveReadonly,
                       DriveService.Scope.DriveScripts },
                DataStore = new FileDataStore(applicationName)
            });

            UserCredential credential = new UserCredential(apiCodeFlow, username, tokenResponse);


            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            return service;
        }
        */
        public DriveService getService()
        {
            UserCredential credential;
            var Scopes = new[] { DriveService.Scope.Drive,
                       DriveService.Scope.DriveAppdata,
                       DriveService.Scope.DriveFile,
                       DriveService.Scope.DriveMetadataReadonly,
                       DriveService.Scope.DriveReadonly,
                       DriveService.Scope.DriveScripts };


            using (var stream = new FileStream(@"C:\credentials.json", FileMode.Open, FileAccess.Read))
            {
                var folderPath = @"C:\";
                var filePath = Path.Combine(folderPath, "DriveServiceCredentials.json");
                
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(filePath, true)).Result;

                //create Drive API service.
                DriveService service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = applicationName,
                });
                return service;


            }
        }


        public string getEditedDocumentStringFromDrive(string TemplateFileName, string[] editArray)
        {
            var service = getService();
            var files = GetDriveFiles();
            var ar = files.ToArray();

            var FolderId = ar[0].Id.ToString();
            var fileName = ar[0].Name.ToString();
            var FolderPath = "C:/GoogleDriveFiles/";

            DownloadGoogleFile(FolderId, fileName);
            var str = editDocument(FolderPath, fileName, editArray);

            File.Delete(FolderPath + fileName);
            File.Delete(FolderPath + "Edited" + fileName);

            return str.Split("$")[1];
        }
        public string createFolder(string parent, string folderName)
        {
            var service = getService();
            var files = GetDriveFiles();
            var ar = files.ToArray();

            var FolderId = ar[0].Id.ToString();
            var fileName = ar[0].Name.ToString();
            var FolderPath = "C:/GoogleDriveFiles/";



            DownloadGoogleFile(FolderId, fileName);

            string[] testArray = { "Susanne", "12 uur", "Michael" };
            var str = editDocument(FolderPath, fileName,testArray);
            emailTest("michaeldbednarek@gmail.com", "intake", str);

            File.Delete(FolderPath + fileName);
            

            var driveFolder = new Google.Apis.Drive.v3.Data.File();
            driveFolder.Name = folderName;
            driveFolder.MimeType = "application/vnd.google-apps.folder";
            driveFolder.Parents = new string[] { parent };
            var command = service.Files.Create(driveFolder);
            var file = command.Execute();
            return file.Id;
        }
        public List<GoogleDriveFile> GetDriveFiles()
        {
            DriveService service = getService();

            // define parameters of request.
            FilesResource.ListRequest FileListRequest = service.Files.List();

            //listRequest.PageSize = 10;
            //listRequest.PageToken = 10;
            FileListRequest.Fields = "nextPageToken, files(id, name, size, version, createdTime)";

            //get file list.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFile> FileList = new List<GoogleDriveFile>();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    GoogleDriveFile File = new GoogleDriveFile
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Size = file.Size,
                        Version = file.Version,
                        CreatedTime = file.CreatedTime
                    };
                    FileList.Add(File);
                }
            }
            return FileList;
        }

        //Download file from Google Drive by fileId.
        public string DownloadGoogleFile(string fileId, string fileName)
        {
            string FolderPath = "C:/GoogleDriveFiles/";

            DriveService service = getService();
            string FilePath = System.IO.Path.Combine(FolderPath, fileName);

            MemoryStream stream1 = new MemoryStream();
                // Add a handler which will be notified on progress changes.
                // It will notify on each chunk download and when the
                // download is completed or failed.

                FilesResource.GetRequest request = service.Files.Get(fileId);

                string FileName = request.Execute().Name;

                request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                {
                    //Return status of the download
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                SaveStream(stream1, FilePath);
                               // request.Download(stream1);
                                break;
                            }
                        case DownloadStatus.Failed:
                            {

                                break;
                            }


                    }
                };
                request.Download(stream1);

            return FilePath;
        }

        public string editDocument(string FolderPath, string fileName, string[] inputs)
        {
            var returnString = "";
            using (FileStream fs = File.OpenRead(FolderPath + fileName))
            {
                GroupDocs.Editor.Options.WordProcessingLoadOptions loadOptions = new WordProcessingLoadOptions();
                loadOptions.Password = "5050@";


                using (Editor editor = new Editor(delegate { return fs; }, delegate { return loadOptions; }))
                {
                    //Set values for eventual files
                    GroupDocs.Editor.Options.WordProcessingEditOptions editOptions = new WordProcessingEditOptions();
                    editOptions.FontExtraction = FontExtractionOptions.ExtractEmbeddedWithoutSystem;
                    editOptions.EnableLanguageInformation = true;
                    editOptions.EnablePagination = true;

                    //Open editor
                    using (EditableDocument beforeEdit = editor.Edit(editOptions))
                    {
                        //get the original text
                        string originalContent = beforeEdit.GetContent();
                        List<IHtmlResource> allResources = beforeEdit.AllResources;

                        //Change all input fields to the users choices
                        var inputFields = originalContent.Split('{', '}');

                        
                        var j = 0;
                        for (int i = 1; i < inputFields.Length;)
                        {
                            inputFields[i] = inputs[j];
                            i += 2;
                            j++;
                        }

                        //put whole document back together
                        originalContent = "";
                        foreach (var part in inputFields)
                        {
                            originalContent = originalContent + part;
                        }

                        returnString = originalContent;

                        string editedContent = originalContent.Replace("Naam:", "Michael Bednarek");

                        

                        // Save Document
                        using (EditableDocument afterEdit = EditableDocument.FromMarkup(editedContent, allResources))
                        {
                            WordProcessingFormats docxFormat = WordProcessingFormats.Docx;
                            GroupDocs.Editor.Options.WordProcessingSaveOptions saveOptions = new WordProcessingSaveOptions(docxFormat);

                            saveOptions.EnablePagination = true;
                            saveOptions.Locale = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                            saveOptions.OptimizeMemoryUsage = true;
                            //saveOptions.Password = "5050@";
                            saveOptions.Protection = new WordProcessingProtection(WordProcessingProtectionType.ReadOnly, "write_password");

                            using (FileStream outputStream = File.Create(FolderPath + "Edited" + fileName))
                            {
                                editor.Save(afterEdit, outputStream, saveOptions);
                            }
                        }
                    }
                }
            }
            return returnString;
        }

        // file save to server path
        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (System.IO.FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        //Delete file from the Google drive
        public void DeleteFile(GoogleDriveFile files)
        {
            DriveService service = getService();
            try
            {
                // Initial validation.
                if (service == null)
                    throw new ArgumentNullException("service");

                if (files == null)
                    throw new ArgumentNullException(files.Id);

                // Make the request.
                service.Files.Delete(files.Id).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
        }

        public void emailTest(string From, string Subject, string Body)
        {
            try
            {
                var final = Body.Split('$')[1];
                var bodyBuilder = new BodyBuilder();
               // bodyBuilder.TextBody = final;
                bodyBuilder.HtmlBody = final;
                

                
                var mimeMessage = new MimeMessage();
               // bodyBuilder.TextBody = mimeMessage.GetTextBody(MimeKit.Text.TextFormat.Html);
                mimeMessage.From.Add(MailboxAddress.Parse(From));
                mimeMessage.ReplyTo.Add(MailboxAddress.Parse(From));
                mimeMessage.To.Add(MailboxAddress.Parse("michaeldbednarek@gmail.com"));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = bodyBuilder.ToMessageBody();//new TextPart("plain");


                string[] Scopes = { GmailService.Scope.GmailSend };
                UserCredential credential;
                //read your credentials file
                using (FileStream stream = new FileStream(Path.Combine(Environment.CurrentDirectory, @"", @"credentials.json"), FileMode.Open, FileAccess.Read))
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    path = Path.Combine(path, ".credentials/gmail-dotnet-quickstart.json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(path, true)).Result;
                }

                string message = mimeMessage.ToString();


                //call your gmail service
                var service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = applicationName });
                var msg = new Google.Apis.Gmail.v1.Data.Message();
                msg.Raw = Base64UrlEncode(message.ToString());
                service.Users.Messages.Send(msg, "me").Execute();



            }
            catch (Exception e)
            {
                var donefuckeditup = e;
            }

        }

        private string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
    }
}
