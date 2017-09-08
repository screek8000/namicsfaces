using System;
using System.Collections.Generic;
using NamicsFaces.Models;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.IO;
using System.Threading.Tasks;
using NamicsFaces.Helpers;
using System.Linq;

namespace NamicsFaces.Services.Implementation
{
    public class FacesTrainApi : IFacesTrainApi
    {
        private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient("511be8524d1a422eb9c21de5a5a54f12", "https://westcentralus.api.cognitive.microsoft.com/face/v1.0");

        private static string personGroupId = "lab-team";


        private async Task<Face[]> UploadAndDetectFaces(string imageFilePath)
        {
            // The list of Face attributes to return.
            IEnumerable<FaceAttributeType> faceAttributes =
                new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Emotion, FaceAttributeType.Glasses, FaceAttributeType.Hair };

            // Call the Face API.
            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, returnFaceId: true, returnFaceLandmarks: false, returnFaceAttributes: faceAttributes);
                    return faces;
                }
            }
            // Catch and display Face API errors.
            catch (FaceAPIException f)
            {
                //MessageBox.Show(f.ErrorMessage, f.ErrorCode);
                return new Face[0];
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Error");
                return new Face[0];
            }
        }

        public void AddFace()
        {
            throw new NotImplementedException();
        }

        public void TrainFaces()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonMetaData> GetPersons()
        {
            Person[] persons = AsyncHelpers.RunSync<Person[]>(() => GetPersonsAsync());
            return persons.Select((person) => new PersonMetaData {
                Name = person.Name,
                PersonId = person.PersonId.ToString(),
                UserData = person.UserData
            });
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