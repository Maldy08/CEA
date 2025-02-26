using CEA.Application.DTOs.Oficios;
using CEA.Application.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace CEA.Infrastructure.Services
{
    public class GoogleCloudService : IGoogleCloudService
    {


   
        private string fileId = "1NVUavkhHQTYSslMudpeg5pJDOWskPBxnc8Sjkm-O7lc";
        private string mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        public GoogleCloudService()
        {
       
        }

        public void DocumentUpdate( OficioDto _oficioDto)
        {
            string DEPENDENCIA = _oficioDto.RemDepen;
            string SECCION = _oficioDto.Depto.ToString();
            string OFICIO = _oficioDto.NoOficio;
            string FECHA = DateTime.Now.ToString("dd/MM/yyyy");
            string ASUNTO = _oficioDto.Tema;
            string RESPONSABLE = _oficioDto.RemNombre;
            string PUESTO = _oficioDto.RemCargo;

            try
            {
                var credential = GetGoogleCredential().CreateScoped(DocsService.Scope.Documents);
                var service = new DocsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Drive API Snippets"
                });
                IList<Request> requests = new List<Request>
                {
                    new Request
                    {
                        ReplaceAllText = (new ReplaceAllTextRequest
                        {
                            ContainsText = (new SubstringMatchCriteria
                            {
                                Text = ("{{DEPENDENCIA}}"),
                                MatchCase = (true)
                            }),
                            ReplaceText = DEPENDENCIA
                        })
                    },

                    new Request
                    {
                        ReplaceAllText = (new ReplaceAllTextRequest
                        {
                            ContainsText = (new SubstringMatchCriteria
                            {
                                Text = ("{{SECCION}}")
                                    ,
                                MatchCase = (true)
                            })
                                    ,
                            ReplaceText = SECCION
                        })
                    },

                    new Request
                    {
                        ReplaceAllText = (new ReplaceAllTextRequest
                        {
                            ContainsText = (new SubstringMatchCriteria
                            {
                                Text = ("{{OFICIO}}")
                                    ,
                                MatchCase = (true)
                            })
                                    ,
                            ReplaceText = OFICIO
                        })
                    },
                    new Request
                    {
                        ReplaceAllText = (new ReplaceAllTextRequest
                        {
                            ContainsText = (new SubstringMatchCriteria
                            {
                                Text = ("{{FECHA}}")
                                    ,
                                MatchCase = (true)
                            })
                                    ,
                            ReplaceText = FECHA
                        })
                    },

                    new Request
                    {
                        ReplaceAllText = (new ReplaceAllTextRequest
                        {
                            ContainsText = (new SubstringMatchCriteria
                            {
                                Text = ("{{ASUNTO}}")
                                    ,
                                MatchCase = (true)
                            })
                                    ,
                            ReplaceText = ASUNTO
                        })
                    },

                    new Request
                    {
                        ReplaceAllText = (new ReplaceAllTextRequest
                        {
                            ContainsText = (new SubstringMatchCriteria
                            {
                                Text = ("{{RESPONSABLE}}")
                                    ,
                                MatchCase = (true)
                            })
                                    ,
                            ReplaceText = RESPONSABLE
                        })
                    },

                    new Request
                    {
                        ReplaceAllText = (new ReplaceAllTextRequest
                        {
                            ContainsText = (new SubstringMatchCriteria
                            {
                                Text = ("{{SECCION}}")
                                    ,
                                MatchCase = (true)
                            })
                                    ,
                            ReplaceText = SECCION
                        })
                    },

                    new Request
                    {
                        ReplaceAllText = (new ReplaceAllTextRequest
                        {
                            ContainsText = (new SubstringMatchCriteria
                            {
                                Text = ("{{PUESTO}}")
                                    ,
                                MatchCase = (true)
                            })
                                    ,
                            ReplaceText = PUESTO
                        })
                    },

                };

                BatchUpdateDocumentRequest body = new BatchUpdateDocumentRequest();
                body.Requests = requests;
                service.Documents.BatchUpdate(body, fileId).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MemoryStream DriveExportWord(OficioDto _oficioDto)
        {
            try
            {
                GoogleCredential credential = GetGoogleCredential().CreateScoped(DriveService.Scope.Drive);
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Drive API Snippets"
                });
                var copReq = service.Files.Copy(FileMetadata(_oficioDto), fileId).Execute();
                Console.WriteLine(copReq.Id);
                DocumentUpdate(_oficioDto);
                var request = service.Files.Export(copReq.Id, mime);

                var stream = new MemoryStream();
                request.MediaDownloader.ProgressChanged +=
                progress =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                Console.WriteLine("Download complete.");
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                Console.WriteLine("Download failed.");
                                break;
                            }
                    }
                };
                request.Download(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Google.Apis.Drive.v3.Data.File FileMetadata(OficioDto _oficioDto)
        {
            return new Google.Apis.Drive.v3.Data.File()
            {
                Name = "Oficio_" + _oficioDto.Folio + "_" + DateTime.Now.ToString("dd/MM/yyyy"),
                Parents = new List<string> { "13oNtrIExmCN6Oy6pJQ9KpzEBDbwmMflZ" }
            };
        }

        public GoogleCredential GetGoogleCredential()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream);
            }
            return credential;
        }
    }
}
