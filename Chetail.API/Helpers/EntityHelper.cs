using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Chetail.API.Helpers
{
    public class EntityHelper<T>
    {
        public object CreateDataShapedObject(T entity, List<string> lstOfFields){
            
            //Return a list of fields to return for an entity
            //Based on a comma separated string passed in
            
            if (!lstOfFields.Any())
            {
                return entity;
            }
            else
            {
                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    var fieldValue = entity.GetType()
                        .GetProperty(field, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                        .GetValue(entity, null);

                    ((IDictionary<string, object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
    }
}