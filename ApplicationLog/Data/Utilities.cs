using ApplicationLog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ApplicationLog.Data
{
    public static class Utilities
    {
        public static List<tbllog> DataReaderMapToList<Tentity>(IDataReader reader)
        {
            var results = new List<tbllog>();

            var columnCount = reader.FieldCount;
            while (reader.Read())
            {
                var item = Activator.CreateInstance<tbllog>();
                try
                {
                    var rdrProperties = Enumerable.Range(0, columnCount).Select(i => reader.GetName(i)).ToArray();
                    foreach (var property in typeof(tbllog).GetProperties())
                    {
                        if ((typeof(tbllog).GetProperty(property.Name).GetGetMethod().IsVirtual) || (!rdrProperties.Contains(property.Name)))
                        {
                            continue;
                        }
                        else
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return results;
        }
    }
}
