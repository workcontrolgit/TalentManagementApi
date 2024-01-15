using System;
using System.Collections.Generic;
using System.Linq;
using TalentManagementApi.Application.Interfaces;

namespace TalentManagementApi.Application.Helpers
{
    public class ModelHelper : IModelHelper
    {
        /// <summary>
        /// Check field name in the model class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string ValidateModelFields<T>(string fields)
        {
            string retString = string.Empty;

            var bindingFlags = System.Reflection.BindingFlags.Instance |
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Public;
            var allowedFields = typeof(T).GetProperties(bindingFlags).Select(f => f.Name).ToList();

            // Convert the fields to array
            string[] inputFields = fields.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            // Convert the array to a list
            List<string> candidateFields = new List<string>(inputFields);

            // Create matchedFields list containing items from candidateFields that are also in allowedFields
            List<string> matchedFields = allowedFields.Intersect(candidateFields).ToList();

            foreach (var field in candidateFields)
            {
                if (!(allowedFields.Contains(field.Trim(), StringComparer.OrdinalIgnoreCase)))
                    matchedFields.Remove(field);
            }

            retString = string.Join(", ", matchedFields);

            return retString;
        }

        /// <summary>
        /// Get list of field names in the model class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetModelFields<T>()
        {
            string retString = string.Empty;

            var bindingFlags = System.Reflection.BindingFlags.Instance |
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Public;
            var allowedFields = typeof(T).GetProperties(bindingFlags).Select(f => f.Name).ToList();

            retString = string.Join(", ", allowedFields);

            return retString;
        }
    }
}