using System;
using System.Collections.Generic;
using NamicsFaces.Models;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.IO;
using System.Threading.Tasks;
using NamicsFaces.Helpers;

namespace NamicsFaces.Services.Implementation
{
    public class FacesApi : IFacesApi
    {
        private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient("511be8524d1a422eb9c21de5a5a54f12", "https://westcentralus.api.cognitive.microsoft.com/face/v1.0");

        public FaceMetaData GetMetaData(string pictureUrl)
        {
            Log("GetMetaDataAsync");
            Face[] faces = AsyncHelpers.RunSync<Face[]>(() => DetectFacesFromUrl(pictureUrl));
            if (faces.Length > 0)
            {
                Face face = faces[0];
                return new FaceMetaData()
                {
                    ImageUrl = pictureUrl,
                    Age = face.FaceAttributes.Age,
                    Gender = face.FaceAttributes.Gender,
                    Glasses = face.FaceAttributes.Glasses.ToString(),
                    Smile = face.FaceAttributes.Smile
                };  
            }
            else
            {
                return new FaceMetaData()
                {
                    ImageUrl = pictureUrl,
                    Age = 0,
                    Gender = "failed"
                };
            }
        }

        public PersonMetaData Identify(string pictureUrl)
        {
            // TODO
            return new PersonMetaData();
        }

        private async Task<Face[]> DetectFacesFromUrl(string pictureUrl)
        {
            // The list of Face attributes to return.
            IEnumerable<FaceAttributeType> faceAttributes =
                new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Emotion, FaceAttributeType.Glasses, FaceAttributeType.Hair };

            // Call the Face API.
            try
            {
                Log("Detecting Faces");
                Face[] faces = await faceServiceClient.DetectAsync(pictureUrl, returnFaceId: true, returnFaceLandmarks: false, returnFaceAttributes: faceAttributes);
                Log("Faces length: " + faces.Length);
                return faces;
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

        private void Log(string log)
        {
            System.Diagnostics.Debug.WriteLine(log);
        }
    }
}