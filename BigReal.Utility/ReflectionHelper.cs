using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HPYA.BasicInfrastructure
{
    public class ReflectionHelper
    {
        public static object CreateInstance(Type type, params object[] args)
        {
            return null;
        }

        public static T CreateInstance<T>(Type type, params object[] args)
        {
            return default(T);
        }

        public static TAttribute GetCustomAttribute<TAttribute>(ICustomAttributeProvider source,
            bool inherit) where TAttribute : Attribute
        {
            object[] customAttributes = source.GetCustomAttributes(typeof(TAttribute), inherit);
            if (customAttributes.Length == 0) { return default(TAttribute); }
            return (TAttribute)customAttributes[0];
        }

        public void SetProperty()
        {

        }

        public static bool MethodHasOpenGenericParameters(MethodBase method)
        {
            return false;
        }

        public Type GetClosedParameterType(Type[] genericArguments)
        {
            return null;
        }

        public Type GetNamedGenericParameter(string parameterName)
        {
            return null;
        }

        public static void TypeIsAssignable(Type assignmentTargetType, Type assignmentValueType, string argumentName)
        {

        }
        public static void InstanceIsAssignable(Type assignmentTargetType, object assignmentInstance, string argumentName)
        {
        }

        private static string GetTypeName(object assignmentInstance)
        {
            return null;
        }

        /// <summary>     
        /// Pull out a <see cref="MethodInfo"/> object from an expression of the form     
        /// () => SomeClass.SomeMethod()     
        /// </summary>     
        /// <param name="expression">Expression describing the method to call.</param>     
        /// <returns>Corresponding <see cref="MethodInfo"/>.</returns>     
        /// [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",    
    }
}
