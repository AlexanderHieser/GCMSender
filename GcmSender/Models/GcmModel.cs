using GcmSender.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace GcmSender.Models
{
    public class GcmModel
    {
        private string ApplicationPath { get; set; }
        private string Name = @"\configuration.xml";
        private WebRequest ValidityRequest;

        public GcmConfig Configuration { get; set; }

        public GcmModel()
        {
            ApplicationPath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            
            ApplicationPath += Name;
        }

        #region Configuration

        /// <summary>
        /// Serialize the given Configuration 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool serializeConfiguration(GcmConfig config) {
            XmlSerializer serializer = new XmlSerializer(typeof(GcmConfig));
            using(StreamWriter writer = new StreamWriter(ApplicationPath)) {
                serializer.Serialize(writer, config);
                return true;
            };
        }

        /// <summary>
        /// Deserialize standard configuration 
        /// </summary>
        /// <returns></returns>
        public GcmConfig deserializeConfiguration()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GcmConfig));
            if (File.Exists(ApplicationPath))
            {
                using (StreamReader reader = new StreamReader(ApplicationPath))
                {
                    GcmConfig config = (GcmConfig)serializer.Deserialize(reader);
                    return config;
                };
            }
            else
            {
                this.Configuration = new GcmConfig();
                serializeConfiguration(Configuration.StandardConfiguration());
                return Configuration;
            }
        }

        #endregion

        /// <summary>
        /// Loads the configuration on startup
        /// </summary>
        /// <returns></returns>
        public GcmConfig LoadConfigurationStartup()
        {
            this.Configuration = deserializeConfiguration();
            if (Configuration != null)
            {
                return Configuration;
            } return null;
        }


        public void CheckKeyValidity(string key, string tokenToTest)
        {
            String[] tokens = tokenToTest.Split(';');
            try
            {
                ValidityRequest = HttpWebRequest.Create(Configuration.GCMServerURL);
                ValidityRequest.Method = "post";
                ValidityRequest.Headers.Add(string.Format("Authorization: key={0}", key));
                ValidityRequest.ContentType = "application/json";
                JObject o = new JObject();
                o.Add("registration_ids", new JArray(tokens));
                ValidityRequest.ContentLength = o.ToString().Length;
                using(StreamWriter writer = new StreamWriter(ValidityRequest.GetRequestStream())) {
                       writer.Write(o.ToString());
                }

                Console.WriteLine(o.ToString());
                HttpWebResponse response = (HttpWebResponse)ValidityRequest.GetResponse();
                StringBuilder sb = new StringBuilder();
                using(StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    sb.Append(reader.ReadLine());
                };
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Your token is valid \n "+ sb.ToString(), "Token is valid", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (WebException e)
            {
                MessageBox.Show("Token not valid", "Sorry", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void SendMessage(string message, string deviceIds, string apikey)
        {
            try
            {
                WebRequest send = HttpWebRequest.Create(Configuration.GCMServerURL) ;
                send.Method = "POST";
                send.ContentType = "application/json";
                send.Headers.Add(string.Format("Authorization: key={0}", apikey));
          //      send.Headers.Add(string.Format("Sender : id= {0}", GcmSender IDataObject ???);
            }
            catch (WebException e)
            {
                MessageBox.Show("Error sending message", "Sorry",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
