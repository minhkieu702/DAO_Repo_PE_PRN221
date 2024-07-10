using System.Reflection;

namespace Eyeglasses_StudentName
{
    public static class Helper
    {
        public static void CopyValues<T>(T target, T source)
        {
            if (target == null || source == null)
            {
                throw new ArgumentNullException("Target or Source cannot be null");
            }

            foreach (PropertyInfo property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanWrite)
                {
                    var value = property.GetValue(source);
                    property.SetValue(target, value);
                }
            }
        }
    }
}
