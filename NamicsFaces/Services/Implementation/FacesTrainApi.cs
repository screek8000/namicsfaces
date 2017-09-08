using System;
using System.Collections.Generic;
using NamicsFaces.Models;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.Threading.Tasks;
using System.Linq;
using System.Web;

namespace NamicsFaces.Services.Implementation
{
    public class FacesTrainApi : IFacesTrainApi
    {
        private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient("511be8524d1a422eb9c21de5a5a54f12", "https://westcentralus.api.cognitive.microsoft.com/face/v1.0");

        private static string personGroupId = "lab-team";

        public async Task AddFaceAsync(HttpPostedFileBase file, string personId, string personName)
        {
            if (personId == null || personId == "")
            {
                Guid newPersonId = await AddPersonAsync(personName);
                if (newPersonId != Guid.Empty)
                {
                    UploadFace(file, newPersonId);
                }
            } else
            {
                UploadFace(file, Guid.Parse(personId));
            }   
        }

        public void TrainFaces()
        {
            StartTrain();
        }

        public async Task<TrainStatus> TrainStatusAsync()
        {
            TrainingStatus status = await GetTrainStatusAsync();
            if (status != null)
            {
                return new TrainStatus
                {
                    Status = status.Status,
                    Message = status.Message,
                    LastActionDateTime = status.LastActionDateTime
                };
            } else
            {
                return new TrainStatus();
            }
        }

        public async Task<IEnumerable<PersonMetaData>> GetPersonsMetaDataAsync()
        {
            Person[] persons = await GetPersonsAsync();
            return persons.Select((person) => new PersonMetaData
            {
                Name = person.Name,
                PersonId = person.PersonId.ToString(),
                UserData = person.UserData
            });
        }

        private async Task<TrainingStatus> GetTrainStatusAsync()
        {
            try
            {
                TrainingStatus status = await faceServiceClient.GetPersonGroupTrainingStatusAsync(personGroupId);
                return status;
            }
            // Catch and display Face API errors.
            catch (FaceAPIException f)
            {
                Log(f.ErrorMessage);
                return null;
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                Log(e.Message);
                return null;
            }
        }

        private async Task<Guid> AddPersonAsync(string personName)
        {
            try
            {
                Log("Add Person " + personName);
                CreatePersonResult personResult = await faceServiceClient.CreatePersonAsync(personGroupId, personName);
                Log("Person added with id " + personResult.PersonId);
                return personResult.PersonId;
            }
            // Catch and display Face API errors.
            catch (FaceAPIException f)
            {
                Log(f.ErrorMessage);
                return Guid.Empty;
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                Log(e.Message);
                return Guid.Empty;
            }
        }

        private void StartTrain()
        {
            try
            {
                faceServiceClient.TrainPersonGroupAsync(personGroupId);
            }
            // Catch and display Face API errors.
            catch (FaceAPIException f)
            {
                Log(f.ErrorMessage);
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                Log(e.Message);
            }
        }

        private void UploadFace(HttpPostedFileBase file, Guid personId)
        {
            try
            {
                Log("Add Face");
                faceServiceClient.AddPersonFaceAsync(personGroupId, personId, file.InputStream);
                Log("Added Face");
            }
            // Catch and display Face API errors.
            catch (FaceAPIException f)
            {
                Log(f.ErrorMessage);
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                Log(e.Message);
            }
        }

        private async Task<Person[]> GetPersonsAsync()
        {
            try
            {
                Log("Getting Persons");
                Person[] persons = await faceServiceClient.ListPersonsAsync(personGroupId);
                Log($"Got {persons.Length} Persons");
                return persons;
            }
            // Catch and display Face API errors.
            catch (FaceAPIException f)
            {
                Log(f.ErrorMessage);
                return new Person[0];
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                Log(e.Message);
                return new Person[0];
            }
        }

        private void Log(string log)
        {
            System.Diagnostics.Debug.WriteLine(log);
        }  
    }
}