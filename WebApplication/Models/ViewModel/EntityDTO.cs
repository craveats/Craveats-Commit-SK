using Generic.Obfuscation.TripleDES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication.Models.ViewModel
{
    public class EntityDTOHelper
    {
        public static U GetEntityDTO<T, U>(T t)
        {
            try
            {
                if (t != null)
                {
                    U uDTO = Activator.CreateInstance<U>();

                    PropertyInfo[] uProps = uDTO.GetType().GetProperties(),
                        tProps = t.GetType().GetProperties();

                    foreach (PropertyInfo propertyInfo in tProps)
                    {
                        if (propertyInfo.CanRead)
                        {
                            PropertyInfo uProp = uProps.FirstOrDefault(u => u.Name == propertyInfo.Name && u.CanWrite);
                            if (uProp != null)
                            {
                                if (!(propertyInfo.Name.ToLower().EndsWith("id") &&
                                    propertyInfo.PropertyType == typeof(System.Int32)))
                                {
                                    uProp.SetValue(uDTO, propertyInfo.GetValue(t, null));
                                }
                                else
                                {
                                    int? tPropVal = (int?)propertyInfo.GetValue(t, null);
                                    uProp.SetValue(
                                        uDTO,
                                        DataSecurityTripleDES.GetEncryptedText(tPropVal.Value));
                                }
                            }
                        }
                    }

                    return uDTO;
                }

                return default(U);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal static T2 MapToEntity<T1, T2>(T1 sourceDTO, T2 targetEntity, bool createInstance = false)
        {
            try
            {
                if (sourceDTO != null)
                {
                    if (targetEntity == null && createInstance)
                    {
                        targetEntity = Activator.CreateInstance<T2>();
                    }

                    PropertyInfo[] sourceProps = sourceDTO.GetType().GetProperties(),
                        targetProps = targetEntity?.GetType().GetProperties();

                    if (targetProps?.Length > 0)
                    {
                        foreach (PropertyInfo propertyInfo in targetProps)
                        {
                            if (propertyInfo.CanWrite)
                            {
                                PropertyInfo uProp = sourceProps.FirstOrDefault(u => u.Name == propertyInfo.Name && u.CanRead);
                                if (uProp != null)
                                {
                                    if (!(propertyInfo.Name.ToLower().EndsWith("id") &&
                                        propertyInfo.PropertyType == typeof(System.Int32)))
                                    {
                                        object objValue = uProp.GetValue(
                                            sourceDTO,
                                            null);

                                        if (objValue != null)
                                        {
                                            if (propertyInfo.PropertyType == uProp.PropertyType)
                                            {
                                                propertyInfo.SetValue(
                                                    targetEntity,
                                                    objValue);
                                            }
                                            else
                                            {
                                                propertyInfo.SetValue(targetEntity,
                                                    Convert.ChangeType(
                                                        objValue,
                                                        propertyInfo.PropertyType));
                                            }
                                        }
                                    }
                                    else 
                                    {
                                        object objValue = DataSecurityTripleDES.GetPlainText(
                                            uProp.GetValue(
                                                sourceDTO,
                                                null));

                                        if (objValue != null)
                                        {
                                            propertyInfo.SetValue(targetEntity, 
                                                Convert.ChangeType(objValue, 
                                                propertyInfo.PropertyType));
                                        }
                                    }
                                }
                            }
                        }

                        return targetEntity;
                    }
                }

                return default(T2);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    [Serializable]
    public class UserDTO
    {
        public string Id { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public Nullable<int> UserStatus { get; set; }
        public Nullable<int> UserTypeFlag { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string ResetCode { get; set; }
        public Nullable<System.DateTime> ResetCodeSentAt { get; set; }
        public Nullable<System.DateTime> ResetCodeExpiry { get; set; }
        public string AddressId { get; set; }
        public string ProfileAssetUrl { get; set; }
    }
}