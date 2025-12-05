using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Lab13PL
{
    public class Scene : IDisposable
    {
        public List<Model> Models { get; } = new();

        // TODO: Stage 3 (1.5pt)
        // Create directory given as a path.
        // If it has any files inside delete them.
        // Then iterate all models in Models list
        // and serialize each model into $"{model.Name}(0).bin" file (binary serialization).
        // If model with filename $"{model.Name}(0).bin" already exists save it with name: $"{model.Name}(1).bin".
        // And by analogy if model with filename $"{model.Name}(1).bin" already exists save it with name: $"{model.Name}(2).bin".
        // etc.
        // Don't serialize fields Mesh and Texture.
        // Add necessary implementation details to Model.cs file.
        public void Serialize(string path)
        {
            
        }

        // TODO: Stage 4 (1pt)
        // Deserialize all binary files at directory given as path.
        // Remember that fields Mesh and Texture were not serialized, so it's important to 
        // call LoadMesh() LoadTexture() on deserialized objects.
        // Add all deserialized objects to Models list.
        public void Deserialize(string path)
        {
            
        }

        public void Clear()
        {
            foreach (var model in Models)
            {
                model.Dispose();
            }
            Models.Clear();
        }

        public void Dispose()
        {
            Clear();
            GC.SuppressFinalize(this);
        }
    }
}