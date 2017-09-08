using System;
using System.Collections.Generic;
using NamicsFaces.Models;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NamicsFaces.Helpers;
using WebGrease.Css.Extensions;
using Microsoft.ProjectOxford.Common.Contract;

namespace NamicsFaces.Services.Implementation
{
    public class FacesApi : IFacesApi
    {
        private readonly IFaceServiceClient _faceServiceClient = new FaceServiceClient("511be8524d1a422eb9c21de5a5a54f12", "https://westcentralus.api.cognitive.microsoft.com/face/v1.0");

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
                    Smile = face.FaceAttributes.Smile,
                    Emotion = getEmotion(face.FaceAttributes.Emotion),
                    FacialHair = face.FaceAttributes.FacialHair
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

        private string getEmotion(EmotionScores emotion)
        {
            KeyValuePair<string, float> bestMatch = new KeyValuePair<string, float>("", 0);
            foreach (KeyValuePair<string,float> item in emotion.ToRankedList())
            {
                if(item.Value > bestMatch.Value)
                {
                    bestMatch = item;
                }
            }
            return bestMatch.Key + " (" + NumberHelpers.ToPercent(bestMatch.Value) + ")";

        }
        
		public async Task<PersonMetaData> Identify(string pictureUrl)        {
			// TODO
			IdentifyResult[] result = await IdentifyPersons(pictureUrl);
	        if (result.Length > 0)
	        {
		        if (result[0].Candidates.Length > 0)
		        {
			        string confidence = $"{result[0].Candidates[0].Confidence * 100}%";
					var candidateId = result[0].Candidates[0].PersonId;
			        Person person = await _faceServiceClient.GetPersonAsync("lab-team", candidateId);
			        return new PersonMetaData { Name = person.Name, ImageUrl = pictureUrl, Confidence = confidence };
				}
			}
	        return new PersonMetaData{Name = "Not found"};
        }

        private async Task<Face[]> DetectFacesFromUrl(string pictureUrl)
        {
            // The list of Face attributes to return.
            IEnumerable<FaceAttributeType> faceAttributes =
                new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Emotion, FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.FacialHair};

            // Call the Face API.
            try
            {
                Log("Detecting Faces");
                Face[] faces = await _faceServiceClient.DetectAsync(pictureUrl, returnFaceId: true, returnFaceLandmarks: false, returnFaceAttributes: faceAttributes);
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
        private void Log(string log)
        {
            System.Diagnostics.Debug.WriteLine(log);
        }
	    public async Task<IdentifyResult[]> IdentifyPersons(string pictureUrl)
	    {
		    string groupId = "lab-team";
		    Face[] tDetect = await DetectFacesFromUrl(pictureUrl);
		    if (tDetect.Length > 0)
		    {
				Guid[] ids = tDetect.Select(item => item.FaceId).ToArray();
				IdentifyResult[] tIdent = await _faceServiceClient.IdentifyAsync(groupId, ids);			
				return tIdent;
		    }
		    else
		    {
			   return new IdentifyResult[0];
		    }
	    }

	}
}