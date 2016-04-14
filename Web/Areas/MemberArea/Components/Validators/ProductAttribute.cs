using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Web.Areas.MemberArea.Components.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class VehicleAttribute : ValidationAttribute
    {
        private readonly string _yearAttribute;
        private readonly string _modelAttribute;

        public VehicleAttribute(string yearAttribute = "Year", string modelAttribute = "Model")
        {
            _yearAttribute = yearAttribute;
            _modelAttribute = modelAttribute;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
           
            string url = getUrl(value?.ToString(), GetAttribute(instance, _modelAttribute), GetAttribute(instance, _yearAttribute));
            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            if (dataStream != null)
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                JavaScriptSerializer js = new JavaScriptSerializer();
                var trims = js.Deserialize<dynamic>(responseFromServer.Remove(responseFromServer.Length - 3, 3).Substring(12));
                reader.Close();
                dataStream.Close();

                if (trims.Length != 0)
                {
                    return null;
                }
            }

            response.Close();

            // TODO :: transalate
            return new ValidationResult("Valitud sõiduk on väärate andmetega");
        }

        private string GetAttribute(object objectInstance, string attribute)
        {
            Type type = objectInstance.GetType();
            PropertyInfo property = type.GetProperty(attribute);
            object attributeInstance = property.GetValue(objectInstance);
            return attributeInstance?.ToString();
        }

        private string getUrl(String make, String model, String year)
        {
            return $"http://www.carqueryapi.com/api/0.3/?callback=?&cmd=getTrims&make={make}&model={model}&year={year}";
        }
    }
}