using System;
using System.Xml.Serialization;

namespace pl_lab11
{
    public enum IrisSpecies
    {
        Setosa,
        Virginica,
        [XmlEnum(Name = "Versi-color")]
        Versicolor
    }

    [Serializable]
    public class Iris
    {
        [XmlElement("Sepal.Length")]
        public double SepalLength { get; set; }
        [XmlElement("Sepal.Width")]
        public double SepalWidth { get; set; }
        [XmlElement("Petal.Length")]
        public double PetalLength { get; set; }
        [XmlElement("Petal.Width")]
        public double PetalWidth { get; set; }
        public IrisSpecies Species { get; set; }
    }

}
