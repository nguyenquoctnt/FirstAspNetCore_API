using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FirstAspNetCore_Model
{
    public static class ModelExtension
    {
        public static int GetTotalRow(this Model model)
        {
            if (model == null)
                return 0;

            return model.TotalRow ?? 0;
        }

        public static int GetTotalRow(this IEnumerable<Model> models)
        {
            if (models == null || (models != null && !models.Any()))
                return 0;

            return models.FirstOrDefault().GetTotalRow();
        }

        public static T Trim<T>(this T model)
        {
            var stringProperties = model.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
            foreach (var stringProperty in stringProperties)
            {
                string value = (string)stringProperty.GetValue(model, null);
                if (value != null && stringProperty.SetMethod != null)
                    stringProperty.SetValue(model, value.Trim(), null);
            }
            return model;
        }

        public static T TrimSQL<T>(this T model)
        {
            var stringProperties = model.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
            foreach (var stringProperty in stringProperties)
            {
                string value = (string)stringProperty.GetValue(model, null);
                if (value != null && stringProperty.SetMethod != null)
                    stringProperty.SetValue(model, value.Replace("%", string.Empty).Trim(), null);
            }
            return model;
        }

        public static void IsNull(this Model model)
        {
            if (model == null)
                throw new RequiredArgumentBodyException("data");
        }

        public static void IsNull(this IEnumerable<Model> models)
        {
            if (models == null)
                throw new RequiredArgumentBodyException("data");
        }
    }
}
