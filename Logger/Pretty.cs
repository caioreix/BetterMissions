using System;
using System.Collections.Generic;
using Unity.Collections;
using ProjectM;
using System.Text.RegularExpressions;

namespace Logger;

public static class PrettyLog {
    // NativeArray logs
    public static void NativeArray<T>(NativeArray<T> data) where T : new() {
        if (Config.traceLevel) {
            var lines = new List<string>();
            foreach (var d in data) {
                lines.Add($"\"{d}\"");
            }

            var msg = $"\"{data.GetType()}\": " + "[" + String.Join(", ", lines) + "]";
            Config.logFile(msg, "List:   ");
        }
    }

    // Struct logs
    public static void Struct<T>(T data) {
        if (Config.traceLevel) {
            var msg = structToString(data);
            Config.logger.LogDebug(msg);
            Config.logFile(msg, "Struct: ");
        }
    }

    private static string structToString<T>(T data) {
        var type = data.GetType();
        var fields = type.GetFields();
        var properties = type.GetProperties();

        var values = new Dictionary<string, object>();
        Array.ForEach(fields, (field) => {
            var value = getValue(field.GetValue(data));
            values.TryAdd(field.Name, value);
        });
        var lines = new List<string>();
        foreach (var value in values) {
            lines.Add($"\"{value.Key}\":\"{value.Value}\"");
        }

        return $"\"{type.ToString()}\": " + "{" + String.Join(",", lines) + "}";
    }

    private static string getValue(object value) {
        var valueStr = value.ToString();
        var type = value.GetType().ToString();

        if (type == "ProjectM.PrefabGUID") {
            var match = Regex.Match(valueStr, @"PrefabGuid\((.*)\)");
            if (match.Success) {
                var groupMatch = match.Groups[1].ToString();
                if (Int32.TryParse(groupMatch, out int j)) {
                    var prefab = new PrefabGUID(j);
                    valueStr = $"PrefabGuid({groupMatch}:{Hooks.PrefabCollectionSystemPatch.GetPrefabName(prefab)})";
                }
            }
        }

        return valueStr;
    }
}