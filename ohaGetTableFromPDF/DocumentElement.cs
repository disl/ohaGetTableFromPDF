using System.Collections.Generic;
using System.Xml.Serialization;

namespace TextOCR_PDF
{
    [XmlRoot(ElementName = "TextLocation")]
        public class TextLocation
        {
            [XmlElement(ElementName = "X")]
            public double X { get; set; }
            [XmlElement(ElementName = "Y")]
            public double Y { get; set; }
            [XmlElement(ElementName = "Text")]
            public string Text { get; set; }
            [XmlElement(ElementName = "Field")]
            public string Field { get; set; }
        }

        [XmlRoot(ElementName = "DocumentElement")]
        public class DocumentElement
        {
            [XmlElement(ElementName = "TextLocation")]
            public List<TextLocation> TextLocation { get; set; }
        }

}
