using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;

namespace Uol.PagSeguro.Serialization
{
    internal static class SerializationHelper
    {
        internal static void SkipElement(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                reader.Read();
            }
            else
            {
                reader.Skip();
            }
        }

        internal static void SkipNode(XmlReader reader)
        {
            reader.Read();
            reader.MoveToContent();
        }

        internal static bool IsEndElement(XmlReader reader, string name)
        {
            return
                reader.NodeType == XmlNodeType.EndElement &&
                reader.Name.Equals(name);
        }

        internal static void WriteElementStringNotNull(XmlWriter writer, string localName, string value)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (localName == null)
                throw new ArgumentNullException("localName");
            if (value != null)
                writer.WriteElementString(localName, value);
        }

        internal static void WriteElementStringNotNull(XmlWriter writer, string localName, decimal? value, int decimals)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (localName == null)
                throw new ArgumentNullException("localName");
            if (value.HasValue)
                WriteElementString(writer, localName, value.Value, decimals);
        }

        internal static void WriteElementStringNotNull(XmlWriter writer, string localName, int? value)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (localName == null)
                throw new ArgumentNullException("localName");
            if (value.HasValue)
                writer.WriteElementString(localName, value.Value.ToString(CultureInfo.InvariantCulture));
        }

        internal static void WriteElementStringNotNull(XmlWriter writer, string localName, long? value)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (localName == null)
                throw new ArgumentNullException("localName");
            if (value.HasValue)
                writer.WriteElementString(localName, value.Value.ToString(CultureInfo.InvariantCulture));
        }

        internal static void WriteElementString(XmlWriter writer, string localName, decimal value, int decimals)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (localName == null)
                throw new ArgumentNullException("localName");

            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalDigits = decimals;
            nfi.NumberGroupSeparator = String.Empty;
            nfi.NumberDecimalSeparator = ".";
            string formattedValue = value.ToString("N", nfi);
            writer.WriteElementString(localName, formattedValue);
        }

        internal static void WriteElementString(XmlWriter writer, string localName, int value)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (localName == null)
                throw new ArgumentNullException("localName");
            writer.WriteElementString(localName, value.ToString(CultureInfo.InvariantCulture));
        }

        internal static void WriteElementString(XmlWriter writer, string localName, long value)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (localName == null)
                throw new ArgumentNullException("localName");
            writer.WriteElementString(localName, value.ToString(CultureInfo.InvariantCulture));
        }
    }
}
