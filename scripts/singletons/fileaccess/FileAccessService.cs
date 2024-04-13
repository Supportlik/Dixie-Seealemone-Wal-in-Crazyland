using Godot;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using MasterofElements.scripts.singletons.fileaccess;

/// <summary>
/// Die FileAccessService Klasse stellt Methoden zum Lesen und Schreiben von Dateien, JSON-Daten und Objekten bereit.
/// </summary>
/// 
public partial class FileAccessService : Node
{
    /// <summary>
    /// Liest den Inhalt einer Datei.
    /// Wenn die Datei nicht existiert, wird null zurückgegeben.
    /// </summary>
    /// <param name="fileInfo">Die zu lesende Datei.</param>
    /// <returns>Der Inhalt der Datei oder null, wenn die Datei nicht existiert.</returns>
    public string ReadFile(FileInfo fileInfo)
    {
        if (!FileAccess.FileExists(fileInfo.Path))
            return null;

        var file = FileAccess.Open(fileInfo.Path, FileAccess.ModeFlags.Read);
        var content = file.GetAsText();
        file.Close();
        return content;
    }

    /// <summary>
    /// Schreibt Inhalt in eine Datei.
    /// </summary>
    /// <param name="fileInfo">Die Datei, in die geschrieben werden soll.</param>
    /// <param name="content">Der zu schreibende Inhalt.</param>
    public void WriteFile(FileInfo fileInfo, string content)
    {
        var file = FileAccess.Open(fileInfo.Path, FileAccess.ModeFlags.WriteRead);
        file.StoreString(content);
        file.Flush();
        file.Close();
    }


    /// <summary>
    /// Liest eine JSON-Datei und gibt ihren Inhalt als JsonNode zurück.
    /// Wenn die Datei nicht existiert, wird null zurückgegeben.
    /// </summary>
    /// <param name="fileInfo">Die zu lesende JSON-Datei.</param>
    /// <returns>Der Inhalt der JSON-Datei als JsonNode oder null, wenn die Datei nicht existiert.</returns>
    public JsonNode ReadJsonFile(FileInfo fileInfo)
    {
        var readTextFromFile = ReadFile(fileInfo);
        if (readTextFromFile == null)
            return null;

        var jsonNode = JsonSerializer.Deserialize<JsonNode>(readTextFromFile);
        return jsonNode;
    }


    /// <summary>
    /// Schreibt einen JsonNode in eine JSON-Datei.
    /// </summary>
    /// <param name="fileInfo">Die JSON-Datei, in die geschrieben werden soll.</param>
    /// <param name="jsonNode">Der zu schreibende JsonNode.</param>
    public void WriteJson(FileInfo fileInfo, JsonNode jsonNode)
    {
        var jsonString = jsonNode.ToJsonString();
        WriteFile(fileInfo, jsonString);
    }

    /// <summary>
    /// Liest eine JSON-Datei, deserialisiert ihren Inhalt zu einem Objekt des Typs TValue und gibt das Objekt zurück.
    /// Wenn die Datei nicht existiert, wird null zurückgegeben.
    /// </summary>
    /// <param name="fileInfo">Die zu lesende JSON-Datei.</param>
    /// <returns>Das deserialisierte Objekt oder null, wenn die Datei nicht existiert.</returns>
    public TValue ReadObject<TValue>(FileInfo fileInfo) where TValue : class
    {
        var jsonNode = ReadJsonFile(fileInfo);
        var customObject = jsonNode?.Deserialize<TValue>();
        return customObject;
    }

    /// <summary>
    /// Serialisiert ein Objekt des Typs TValue zu einem JsonNode und schreibt den JsonNode in eine JSON-Datei.
    /// </summary>
    /// <param name="fileInfo">Die JSON-Datei, in die geschrieben werden soll.</param>
    /// <param name="customObject">Das zu serialisierende und zu schreibende Objekt.</param>
    public void WriteObject<TValue>(FileInfo fileInfo, TValue customObject)
    {
        var jsonNode = JsonSerializer.SerializeToNode(customObject);
        WriteJson(fileInfo, jsonNode);
    }
}