using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProNfe.App.Uteis
{
  

    public class ValidationHelper
    {                       

        public static List<ValidationResult> Validate(object instance, bool validateAllObjects)
        {
            var listOfErrors = new List<ValidationResult>();

            var T = instance.GetType();

            var metaDataType = T.GetCustomAttributes(typeof(MetadataTypeAttribute), true)
                .OfType<MetadataTypeAttribute>().FirstOrDefault();


            TypeDescriptor.AddProviderTransparent(
                new AssociatedMetadataTypeTypeDescriptionProvider( metaDataType.GetType()), T);

            var validationContext = new ValidationContext(instance, null, null);

            var isValid = Validator.TryValidateObject(instance, validationContext, listOfErrors, validateAllObjects);

            if (!isValid)
            {
                StringBuilder errorMsg = new StringBuilder();
                listOfErrors.ForEach((ValidationResult ex) =>
                    errorMsg.Append(ex.ErrorMessage)
                    );
               // throw new ValidationException(errorMsg.ToString());
            }

            return listOfErrors;
        }
      
    }
}
