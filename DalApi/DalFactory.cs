using System;
using System.Reflection;

namespace DalApi
{
    /// <summary>
    /// Static Factory class for creating Dal tier implementation object according to
    /// configuration in file config.xml
    /// </summary>
    public class DalFactory
    {
        /// <summary>
        /// The function creates Dal tier implementation object according to Dal type
        /// as appears in "dal" element in the configuration file config.xml.<br/>
        /// The configuration file also includes element "dal-packages" with list
        /// of available packages (.dll files) per Dal type.<br/>
        /// Each Dal package must use "Dal" namespace and it must include internal access
        /// singleton class with the same name as package's name.<br/>
        /// The singleton class must include public static property called "Instance"
        /// which must contain the single instance of the class.
        /// </summary>
        /// <returns>Dal tier implementation object</returns>
        public static IDal GetDal()
        {
            string dalType = DalConfig.DalName;
            string dalPackage = DalConfig.DalPackages["xml"];
            if (dalPackage == null)
                throw new DalConfigException($"Wrong DL type: {dalType}");

            try // Load concrete Dal implementation assembly
            {
                Assembly asm = Assembly.Load(dalPackage);
            }
            catch (Exception ex)
            {
                throw new DalConfigException($"Failed loading {dalPackage}.dll", ex);
            }

            // Get concrete Dal implementation's class metadata object
            Type type = Type.GetType($"Dal.{dalPackage}, {dalPackage}");
            if (type == null)
                throw new DalConfigException($"Class name is not the same as Assembly Name: {dalPackage}");

            // Get concrete Dal implementation's Instance
            IDal dal = (IDal)type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)
                .GetValue(null); // since it's a static property - no need for an object
            if (dal == null)
                throw new DalConfigException($"Class {dalPackage} is not a singleton");

            return dal;
        }
    }
}
