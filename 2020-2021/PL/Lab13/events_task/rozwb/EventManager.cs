using System.Xml.Serialization;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace lab_13pl
{
    public class Event : IEquatable<Event>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime EventOccurredAt { get; set; }
        public string Owner { get; set; }
        public string Details { get; set; }
        public bool Equals(Event other)
        {
            if(other == null)
                return false;
            
            return this.Id == other.Id
                && Name == other.Name
                && EventOccurredAt == other.EventOccurredAt
                && Owner == other.Owner
                && Details == other.Details;
        }
    }

    public class EventManager 
    {
        private readonly string _rootPath;

        public EventManager(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            _rootPath = rootPath;
        }

        public IEnumerable<Event> Import(string xmlFilePath)
        {
            if(!File.Exists(xmlFilePath))
            {
                throw new FileNotFoundException($"File {xmlFilePath} doesn't exist", Path.GetFileName(xmlFilePath));
            }

            using var fs = new FileStream(xmlFilePath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Event[]));
            var importedEvents = (Event[])serializer.Deserialize(fs);
            return importedEvents;
        }

        public void AddEvent(Event newEvent){
            var eventPath = Path.Combine(_rootPath, newEvent.Owner, newEvent.EventOccurredAt.Year.ToString("D4"), newEvent.EventOccurredAt.Month.ToString("D2"));
            if(!Directory.Exists(eventPath)){
                Directory.CreateDirectory(eventPath);
            }

            using var sw = new StreamWriter(Path.Combine(eventPath,  $"{newEvent.EventOccurredAt:yyyy-MM-dd}.json"), true);
            sw.WriteLine(JsonSerializer.Serialize(newEvent));
        }

        public IEnumerable<Event> GetOwnerEvents(string owner)
        {
            var result = new List<Event>();
            var ownerPath = Path.Combine(_rootPath, owner);
            if(!Directory.Exists(ownerPath)){
                return result;
            }

            foreach (var filePath in Directory.EnumerateFiles(ownerPath, "*.json", SearchOption.AllDirectories))
            {
                using var sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    result.Add(JsonSerializer.Deserialize<Event>(line));
                }
            }

            return result;
        }

        public bool RemoveOwnerEvents(string owner)
        {
            var ownerPath = Path.Combine(_rootPath, owner);
            if(!Directory.Exists(ownerPath)){
                return false;
            }

            Directory.Delete(ownerPath, true);
            return true;
        }

        public void Export(string exportFilePath)
        {
            var items = new List<Event>();
            foreach (var filePath in Directory.EnumerateFiles(_rootPath, "*.json", SearchOption.AllDirectories))
            {
                using var sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    items.Add(JsonSerializer.Deserialize<Event>(line));
                }
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Event[]));
            using var sw = new StreamWriter(exportFilePath);
            serializer.Serialize(sw, items.ToArray());
        }
    }
}